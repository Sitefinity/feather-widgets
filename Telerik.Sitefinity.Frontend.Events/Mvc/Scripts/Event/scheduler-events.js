(function ($) {
    $(document).ready(function () {
        $('[data-sf-role="scheduler-wrapper"][data-sf-loaded="false"]').each(function (i, element) {
            var schedulerHtmlTemplates = {
                eventAllDayEventTemplateHtml: $(element).find('[data-sf-role="event-alldayeventtemplate"]').html(),
                eventCalendarlistWrapperHtml: $(element).find('[data-sf-role="event-calendarlist-template-wrapper"]').html(),
                eventCalendarlistItemHtml: $(element).find('[data-sf-role="event-calendarlist-template-item"]').html(),
                eventEventTemplateHtml: $(element).find('[data-sf-role="event-eventtemplate"]').html()
            };
            var schedulerData = {
                calendarIdList: [],
                calendarUrl: $(element).attr('data-sf-controller-calendars'),
                calendarListClassActive: $(element).attr('data-sf-calendarlist-class-active'),
                calendarList: $(element).find('[data-sf-role="calendarlist"]'),
                eventsUrl: $(element).attr('data-sf-controller-events'),
                minCalendarLength: $(element).attr('data-sf-mincalendarlength'),
                model: $(element).attr('data-sf-controller-model'),
                scheduler: $(element).find('[data-sf-role="scheduler"]'),
                uiCulture: $(element).attr('data-sf-uiculture')
            };

            $(schedulerHtmlTemplates.eventCalendarlistWrapperHtml).appendTo(schedulerData.calendarList);
            schedulerData.calendarlistWrapper = $(schedulerData.calendarList).find('[data-sf-role="calendarlist-wrapper"]');

            var kendoScheduler = schedulerData.scheduler.kendoScheduler({
                editable: false,
                batch: true,
                views: ["day", "workWeek", "week", { type: "month", selected: true }, "agenda", { type: "timeline", eventHeight: 50 }],
                allDayEventTemplate: schedulerHtmlTemplates.eventAllDayEventTemplateHtml,
                eventTemplate: schedulerHtmlTemplates.eventEventTemplateHtml,
                dataSource: {
                    transport: {
                        read: {
                            url: schedulerData.eventsUrl,
                            dataType: "json",
                            traditional: true
                        },
                        parameterMap: function (options, operation) {
                            if (operation === "read") {
                                var scheduler = kendoScheduler.data("kendoScheduler");
                                var model = $.parseJSON(schedulerData.model);
                                model.StartDate = kendo.toString(scheduler.view().startDate(), "s");
                                model.EndDate = kendo.toString(scheduler.view().endDate(), "s");
                                model.CalendarList = $.makeArray(schedulerData.calendarlistWrapper.find('[data-sf-role="calendarlist-item"].' + schedulerData.calendarListClassActive).attr("data-sf-id"));
                                model.UiCulture = schedulerData.uiCulture;
                                return model;
                            }
                        }
                    },
                    serverFiltering: true,
                    schema: {
                        model: {
                            id: "Id", // The "id" of the event is the "taskId" field
                            fields: {
                                // Describe the scheduler event fields and map them to the fields returned by the remote service
                                taskId: {
                                    from: "Id"
                                },
                                title: { from: "Title", defaultValue: "No title", validation: { required: true } },
                                start: { type: "date", from: "Start" },
                                end: { type: "date", from: "End" },
                                description: { from: "Description" },
                                recurrenceId: { from: "RecurrenceID" },
                                recurrenceRule: { from: "RecurrenceRule" },
                                recurrenceException: { from: "RecurrenceException" },
                                isAllDay: { type: "boolean", from: "IsAllDay" },
                                calendarId: { from: "CalendarId", defaultValue: '045b2da5-a247-6ea2-811c-ff0000a3df5c' },
                                eventUrl: { from: "EventUrl" },
                                city: { from: "City" },
                                country: { from: "Country" }
                            }
                        }
                    }
                },
                resources: [
                    {
                        field: "calendarId",
                        dataValueField: "calendarId",
                        dataSource: {
                            transport: {
                                read: {
                                    url: schedulerData.calendarUrl,
                                    dataType: "json",
                                    complete: function (jqXHR, textStatus) {
                                        if (jqXHR && jqXHR.responseJSON) {
                                            var calendarData = jqXHR.responseJSON;
                                            if (calendarData.length >= schedulerData.minCalendarLength) {
                                                $.each(calendarData, function (i, calendar) {
                                                    var template = kendo.template(schedulerHtmlTemplates.eventCalendarlistItemHtml);
                                                    schedulerData.calendarlistWrapper.append(template(calendar));
                                                    schedulerData.calendarIdList.push(calendar.calendarId);
                                                });
                                                schedulerData.calendarList.show();
                                            }
                                        }
                                    }
                                },
                                parameterMap: function (options, operation) {
                                    if (operation === "read") {
                                        var model = $.parseJSON(schedulerData.model);
                                        model.UiCulture = schedulerData.uiCulture;
                                        return model;
                                    }
                                }
                            },
                            schema: {
                                model: {
                                    id: "CalendarId", // The "id" of the calendar is the "CalendarId" field
                                    fields: {
                                        calendarId: { from: "CalendarId" },
                                        title: { from: "Title" },
                                        color: { from: "Color" }
                                    }
                                }
                            }
                        }
                    }
                ]
            });
            schedulerData.calendarlistWrapper.on('click', '[data-sf-role="calendarlist-item"]', function () {
                if ($(this).hasClass(schedulerData.calendarListClassActive)) {
                    return;
                }
                var id = $(this).attr("data-sf-id");
                $(this).siblings($(this).tagName).removeClass(schedulerData.calendarListClassActive);
                $(this).addClass(schedulerData.calendarListClassActive);
                kendoScheduler.data("kendoScheduler").dataSource.filter({
                    operator: function (task) {
                        return $.inArray(task.calendarId, id && id != "" ? [id] : schedulerData.calendarIdList) >= 0;
                    }
                });
            });
            $(element).attr("data-sf-loaded", true);
        });
    });
}(jQuery));