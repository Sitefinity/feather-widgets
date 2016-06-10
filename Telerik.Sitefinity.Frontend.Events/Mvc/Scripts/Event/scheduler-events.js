(function ($) {
    $(function () {
        $('[data-sf-role="scheduler-wrapper"]').each(function (index, element) {
            var calendar = [];
            var minCalendarLenght = 1;
            var wrapper = $(element);
            var calendarUrl = wrapper.find('[data-sf-role="scheduler-controller-calendars"]').val();
            var eventsUrl = wrapper.find('[data-sf-role="scheduler-controller-events"]').val();
            var model = wrapper.find('[data-sf-role="scheduler-controller-model"]').val();
            var schedulerList = wrapper.find('[data-sf-role="scheduler-list"]');
            var eventTemplate = wrapper.find('[data-sf-role="event-template"]');
            var scheduler = wrapper.find('[data-sf-role="scheduler"]');
            var kendoScheduler = scheduler.kendoScheduler({
                editable: false,
                views: [
                    "day",
                    "workWeek",
                    "week",
                    { type: "month", selected: true },
                    "agenda",
                    { type: "timeline", eventHeight: 50 }
                ],
                allDayEventTemplate: eventTemplate.html(),
                eventTemplate: eventTemplate.html(),
                dataSource: {
                    batch: true, // Enable batch updates
                    transport: {
                        read: {
                            url: eventsUrl,
                            dataType: "json",
                            complete: function (jqXHR, textStatus) {
                                if (jqXHR && jqXHR.responseText) {
                                    var data = jqXHR.responseJSON;
                                    var calendars = [];
                                    $.each(data, function (i, ev) {
                                        $.each(calendar, function (j, cal) {
                                            if (cal.calendarId == ev.calendarId && $.grep(calendars, function (obj) { return obj == cal; }).length == 0) { //check if calendar exist in calendar list
                                                calendars.push(cal);
                                                return;
                                            }
                                        });
                                    });
                                    if (calendars.length > minCalendarLenght && schedulerList) {
                                        $.each(calendars, function (i, el) {
                                            schedulerList.append('<li value="' + el.calendarId + '" style="padding: 2px; background-color: ' + el.color + '">' + el.Title + '</li>');
                                        });
                                        schedulerList.show();
                                    }
                                }
                            }
                        },
                        parameterMap: function (options, operation) {
                            if (operation === "read") {
                                return $.parseJSON(model);
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
                                    url: calendarUrl,
                                    dataType: "json",
                                    complete: function (jqXHR, textStatus) {
                                        if (jqXHR && jqXHR.responseText) {
                                            var data = jqXHR.responseJSON;
                                            $.each(data, function (i, el) {
                                                calendar.push(el);
                                            });
                                        }
                                    }
                                },
                                parameterMap: function (options, operation) {
                                    if (operation === "read") {
                                        return $.parseJSON(model);
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
        });
    });
}(jQuery));