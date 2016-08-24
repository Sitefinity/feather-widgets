(function ($) {
    $(document).ready(function () {
        $('[data-sf-role="scheduler-wrapper"][data-sf-loaded="false"]').each(function (i, element) {
            $(element).attr('data-sf-localtimezoneoffset', new Date().getTimezoneOffset());

            var schedulerHtmlTemplates = {
                eventAllDayEventTemplateHtml: $(element).find('[data-sf-role="event-alldayeventtemplate"]').html(),
                eventCalendarlistWrapperHtml: $(element).find('[data-sf-role="event-calendarlist-template-wrapper"]').html(),
                eventCalendarlistItemHtml: $(element).find('[data-sf-role="event-calendarlist-template-item"]').html(),
                eventEventTemplateHtml: $(element).find('[data-sf-role="event-eventtemplate"]').html()
            };
            var schedulerData = {
                model: $.parseJSON($(element).attr('data-sf-controller-model')),
                calendarIdList: [],
                calendarUrl: $(element).attr('data-sf-controller-calendars'),
                calendarListClassActive: $(element).attr('data-sf-calendarlist-class-active'),
                calendarList: $(element).find('[data-sf-role="calendarlist"]'),
                defaultview: $(element).attr('data-sf-defaultview'),
                eventsUrl: $(element).attr('data-sf-controller-events'),
                minCalendarLength: $(element).attr('data-sf-mincalendarlength'),
                scheduler: $(element).find('[data-sf-role="scheduler"]'),
                uiCulture: $(element).attr('data-sf-uiculture'),
                timezoneOffset: $(element).attr('data-sf-timezoneoffset'),
                timezone: function () {
                    var zones = $.grep(kendo.timezone.windows_zones, function (e) { return e.other_zone === $(element).attr('data-sf-timezoneid'); });
                    if (zones.length > 0) {
                        return zones[0].zone;
                    }
                    return "";
                }
            };
            // setup start day
            kendo.culture().calendar.firstDay = schedulerData.model.WeekStartDay === "Sunday" ? 0 : 1;
            // calendar list init
            if (schedulerData.model.AllowCalendarFilter) {
                $(schedulerHtmlTemplates.eventCalendarlistWrapperHtml).appendTo(schedulerData.calendarList);
                schedulerData.calendarlistWrapper = $(schedulerData.calendarList).find('[data-sf-role="calendarlist-wrapper"]');
            }
            else {
                schedulerData.calendarList.remove();
            }
            var kendoScheduler = schedulerData.scheduler.kendoScheduler({
                editable: false,
                batch: true,
                timezone: schedulerData.timezone(),
                views: [schedulerData.model.AllowChangeCalendarView ? { type: "day", selected: schedulerData.defaultview == 'Day' } : schedulerData.defaultview == 'Day' ? { type: "day" } : {},
                        schedulerData.model.AllowChangeCalendarView ? { type: "workWeek", selected: schedulerData.defaultview == 'WorkWeek' } : schedulerData.defaultview == 'WorkWeek' ? { type: "workWeek" } : {},
                        schedulerData.model.AllowChangeCalendarView ? { type: "week", selected: schedulerData.defaultview == 'Week' } : schedulerData.defaultview == 'Week' ? { type: "week" } : {},
                        schedulerData.model.AllowChangeCalendarView ? { type: "month", selected: schedulerData.defaultview == 'Month' } : schedulerData.defaultview == 'Month' ? { type: "month" } : {},
                        schedulerData.model.AllowChangeCalendarView ? { type: "agenda", selected: schedulerData.defaultview == 'Agenda' } : schedulerData.defaultview == 'Agenda' ? { type: "agenda" } : {},
                        schedulerData.model.AllowChangeCalendarView ? { type: "timeline", eventHeight: 50, selected: schedulerData.defaultview == 'Timeline' } : schedulerData.defaultview == 'Timeline' ? { type: "timeline", eventHeight: 50 } : {}
                ],
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
                                var model = schedulerData.model;
                                var startDate = scheduler.view().startDate();
                                var endDate = scheduler.view().endDate();
                                var localOffsetStartDate = startDate.getTimezoneOffset() * 60000;
                                var localOffsetEndDate = endDate.getTimezoneOffset() * 60000;
                                model.StartDate = new Date(startDate.getTime() - schedulerData.timezoneOffset - localOffsetStartDate).toISOString();
                                model.EndDate = new Date(endDate.getTime() - schedulerData.timezoneOffset - localOffsetEndDate).toISOString();
                                model.CalendarList = schedulerData.model.AllowCalendarFilter ? $.makeArray(schedulerData.calendarlistWrapper.find('[data-sf-role="calendarlist-item"].' + schedulerData.calendarListClassActive).attr("data-sf-id")) : [];
                                model.UiCulture = schedulerData.uiCulture;
                                model.EventSchedulerViewMode = scheduler.view().options.name.replace("View", "");
                                return model;
                            }
                        }
                    },
                    serverFiltering: true,
                    schema: {
                        model: {
                            id: "Id", // The "id" of the event is the "taskId" field
                            fields: {
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
                        },
                        parse: function (response) {
                            // https://github.com/telerik/kendo-ui-core/issues/1632 fix
                            var scheduler = kendoScheduler.data("kendoScheduler");
                            var events = [];
                            for (var i = 0; i < response.length; i++) {
                                var currentEvent = response[i];
                                if (scheduler.view().options.name === "agenda" && currentEvent.IsAllDay) {
                                    var endDate = new Date(parseInt(currentEvent.End.substr(6)))
                                    currentEvent.End = new Date(endDate.setDate(endDate.getDate() + 1)).toISOString();
                                }
                                events.push(currentEvent);
                            }
                            return events;
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
                                        var model = schedulerData.model;
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
            if (schedulerData.model.AllowCalendarFilter) {
                schedulerData.calendarlistWrapper.on('click', '[data-sf-role="calendarlist-item"]', function () {
                    if ($(this).hasClass(schedulerData.calendarListClassActive)) {
                        return;
                    }
                    var id = $(this).attr("data-sf-id");
                    $(this).siblings($(this).tagName).removeClass(schedulerData.calendarListClassActive);
                    $(this).addClass(schedulerData.calendarListClassActive);
                    kendoScheduler.data("kendoScheduler").dataSource.filter({
                        operator: function (task) {
                            return $.inArray(task.calendarId, id && id !== "" ? [id] : schedulerData.calendarIdList) >= 0;
                        }
                    });
                });
            }
            $(element).attr("data-sf-loaded", true);
        });
    });
}(jQuery));