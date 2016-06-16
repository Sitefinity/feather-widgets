(function ($) {
    $(document).ready(function () {
        $('[data-sf-role="scheduler-wrapper"][data-sf-loaded="false"]').each(function (index, element) {
            var calendar = [], minCalendarLength = 1, wrapper = $(element), calendarIdList = [],
			schedulerListArea = wrapper.find('[data-sf-role="scheduler-list"]'),
			schedulerList = wrapper.find('[data-sf-role="scheduler-list"] ul'),
            calendarUrl = wrapper.find('[data-sf-role="scheduler-controller-calendars"]').val(),
            eventsUrl = wrapper.find('[data-sf-role="scheduler-controller-events"]').val(),
            model = wrapper.find('[data-sf-role="scheduler-controller-model"]').val(),
            eventTemplateHtml = wrapper.find('[data-sf-role="event-template"]').html(),
            scheduler = wrapper.find('[data-sf-role="scheduler"]'),
            kendoScheduler = scheduler.kendoScheduler({
                editable: false,
                views: [
                    "day",
                    "workWeek",
                    "week",
                    { type: "month", selected: true },
                    "agenda",
                    { type: "timeline", eventHeight: 50 }
                ],
                allDayEventTemplate: eventTemplateHtml,
                eventTemplate: eventTemplateHtml,
                dataSource: {
                    transport: {
                        read: {
                            url: eventsUrl,
                            dataType: "json",
                            complete: function (jqXHR, textStatus) {
                                if (jqXHR && jqXHR.responseJSON) {
                                    var data = jqXHR.responseJSON;
                                    var calendars = [];
                                    $.each(data, function (i, ev) {
                                        $.each(calendar, function (j, cal) {
                                            if (cal.calendarId == ev.calendarId && $.grep(calendars, function (obj) { return obj == cal; }).length == 0) { //check if calendar exist in event calendars
                                                calendars.push(cal);
                                                return;
                                            }
                                        });
                                    });
                                    if (calendars.length > minCalendarLength && schedulerList) {
                                        $.each(calendars, function (i, el) {
                                            schedulerList.append('<li class="sf-calendarList-item" data-sf-id="' + el.calendarId + '"><span class="sf-event-type" style="background-color: ' + el.color + '""></span>' + el.Title + '</li>');
                                            calendarIdList.push(el.calendarId);
                                        });
                                        schedulerListArea.show();
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
                                        if (jqXHR && jqXHR.responseJSON) {
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
            schedulerList.on('click', 'li', function () {
                var id = $(this).attr("data-sf-id"),
                dataKendoScheduler = kendoScheduler.data("kendoScheduler");
                schedulerList.find("li").each(function (i) {
                    if (id == $(this).attr("data-sf-id")) {
                        $(this).addClass("active");
                    }
                    else {
                        $(this).removeClass("active");
                    }
                });
                dataKendoScheduler.dataSource.filter({
                    operator: function (task) {
                        return $.inArray(task.calendarId, id == "-1" ? calendarIdList : [id]) >= 0;
                    }
                });
            });
            wrapper.attr("data-sf-loaded", true);
        });
    });
}(jQuery));