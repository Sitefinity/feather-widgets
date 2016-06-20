(function ($) {
    $(document).ready(function () {
        $('[data-sf-role="scheduler-wrapper"][data-sf-loaded="false"]').each(function (i, element) {
            var schedulerHtmlTemplates = {
                eventAllDayEventTemplateHtml: $(element).find('[data-sf-role="event-alldayeventtemplate"]').html(),
                eventEventTemplateHtml: $(element).find('[data-sf-role="event-eventtemplate"]').html(),
                eventCalendarlistWrapperHtml: $(element).find('[data-sf-role="event-calendarlist-template-wrapper"]').html(),
                eventCalendarlistItemHtml: $(element).find('[data-sf-role="event-calendarlist-template-item"]').html()
            }
            var schedulerData = {
                allCalendars: [],
                minCalendarLength: 2,
                calendarIdList: [],
                calendarUrl: $(element).attr('data-sf-controller-calendars'),
                eventsUrl: $(element).attr('data-sf-controller-events'),
                model: $(element).attr('data-sf-controller-model'),
                scheduler: $(element).find('[data-sf-role="scheduler"]'),
                calendarListClassActive: $(element).attr('data-sf-calendarlist-class-active'),
                calendarList: $(element).find('[data-sf-role="calendarlist"]')
            };

            $(schedulerHtmlTemplates.eventCalendarlistWrapperHtml).appendTo(schedulerData.calendarList);
            schedulerData.calendarlistWrapper = $(schedulerData.calendarList).find('[data-sf-role="calendarlist-wrapper"]');

            var kendoScheduler = schedulerData.scheduler.kendoScheduler({
                editable: false,
                views: ["day", "workWeek", "week", { type: "month", selected: true }, "agenda", { type: "timeline", eventHeight: 50 }],
                allDayEventTemplate: schedulerHtmlTemplates.eventAllDayEventTemplateHtml,
                eventTemplate: schedulerHtmlTemplates.eventEventTemplateHtml,
                dataSource: {
                    transport: {
                        read: {
                            url: schedulerData.eventsUrl,
                            dataType: "json",
                            complete: function (jqXHR, textStatus) {
                                if (jqXHR && jqXHR.responseJSON) {
                                    var data = jqXHR.responseJSON;
                                    var calendars = [];
                                    $.each(data, function (i, ev) {
                                        $.each(schedulerData.allCalendars, function (j, cal) {
                                            if (cal.calendarId == ev.calendarId && $.grep(calendars, function (obj) { return obj == cal; }).length == 0) { //check if calendar exist in event calendars
                                                calendars.push(cal);
                                                return;
                                            }
                                        });
                                    });
                                    if (calendars.length >= schedulerData.minCalendarLength) {
                                        $.each(calendars, function (i, el) {
                                            var template = kendo.template(schedulerHtmlTemplates.eventCalendarlistItemHtml);
                                            var templateData = { calendarId: el.calendarId, color: el.color, title: el.Title };
                                            schedulerData.calendarlistWrapper.append(template(templateData));
                                            schedulerData.calendarIdList.push(el.calendarId);
                                        });
                                        schedulerData.calendarList.show();
                                    }
                                }
                            }
                        },
                        parameterMap: function (options, operation) {
                            if (operation === "read") {
                                return $.parseJSON(schedulerData.model);
                            }
                        }
                    },
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
                                            var data = jqXHR.responseJSON;
                                            $.each(data, function (i, el) {
                                                schedulerData.allCalendars.push(el);
                                            });
                                        }
                                    }
                                },
                                parameterMap: function (options, operation) {
                                    if (operation === "read") {
                                        return $.parseJSON(schedulerData.model);
                                    }
                                }
                            },
                            schema: {
                                model: {
                                    id: "CalendarId", // The "id" of the calendar is the "CalendarId" field
                                    fields: {
                                        calendarId: { from: "CalendarId" },
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