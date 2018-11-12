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
                allowCalendarFilter: JSON.parse($(element).attr('data-sf-allowcalendarfilter')),
                allowChangeCalendarView: JSON.parse($(element).attr('data-sf-allowchangecalendarview')),
                calendarUrl: $(element).attr('data-sf-controller-calendars'),
                calendarListClassActive: $(element).attr('data-sf-calendarlist-class-active'),
                calendarList: $(element).find('[data-sf-role="calendarlist"]'),
                defaultview: $(element).attr('data-sf-defaultview'),
                eventsUrl: $(element).attr('data-sf-controller-events'),
                minCalendarLength: parseInt($(element).attr('data-sf-mincalendarlength')),
                uiCulture: $(element).attr('data-sf-uiculture'),
                scheduler: $(element).find('[data-sf-role="scheduler"]'),
                timezoneOffset: $(element).attr('data-sf-timezoneoffset'),
                timezone: function () {
                    var zones = $.grep(kendo.timezone.windows_zones, function (e) { return e.other_zone === $(element).attr('data-sf-timezoneid'); });
                    if (zones.length > 0) {
                        return zones[0].zone;
                    }
                    return "";
                },
                siteid: $(element).attr('data-sf-siteid'),
                localTimezoneOffset: function () {
                    var localtimezoneoffset = new Date().getTimezoneOffset();
                    $(element).attr('data-sf-localtimezoneoffset', localtimezoneoffset);
                    return localtimezoneoffset;
                },
                weekStartDay: $(element).attr('data-sf-weekstartday'),
                widgetId: $(element).attr('data-sf-widget-id'),
                isRtl: $(element).attr('data-sf-rtl')
            };
            // check if is Rtl
            if (schedulerData.isRtl === "True") {
                $('[data-sf-role="scheduler-wrapper"]').addClass("k-rtl"); 
            }
            // setup start day
            kendo.culture().calendar.firstDay = schedulerData.weekStartDay === "Sunday" ? 0 : 1;
            // calendar list init
            if (schedulerData.allowCalendarFilter) {
                $(schedulerHtmlTemplates.eventCalendarlistWrapperHtml).appendTo(schedulerData.calendarList);
                schedulerData.calendarlistWrapper = $(schedulerData.calendarList).find('[data-sf-role="calendarlist-wrapper"]');
            }
            else {
                schedulerData.calendarList.remove();
            }

            var allowChangeCalendarView = schedulerData.allowChangeCalendarView,
				defaultview = schedulerData.defaultview;

            var kendoSchedulerViewInit = function (typeCamel, typePascal) {
                var isTimeline = typeCamel === 'timeline',
					kendoSchedulerView;

                if (allowChangeCalendarView) {
                    kendoSchedulerView = { type: typeCamel, selected: defaultview == typePascal };
                } else if (defaultview == typePascal) {
                    kendoSchedulerView = { type: typeCamel };
                } else {
                    kendoSchedulerView = {};
                }

                if (isTimeline && (allowChangeCalendarView || defaultview == typePascal)) {
                    kendoSchedulerView.eventHeight = 50;
                }

                return kendoSchedulerView;
            };

            var kendoScheduler = schedulerData.scheduler.kendoScheduler({
                editable: false,
                batch: true,
                timezone: schedulerData.timezone(),
                views: [kendoSchedulerViewInit('day', 'Day'),
						kendoSchedulerViewInit('workWeek', 'WorkWeek'),
						kendoSchedulerViewInit('week', 'Week'),
						kendoSchedulerViewInit('month', 'Month'),
						kendoSchedulerViewInit('agenda', 'Agenda'),
						kendoSchedulerViewInit('timeline', 'Timeline')
                ],
                allDayEventTemplate: schedulerHtmlTemplates.eventAllDayEventTemplateHtml,
                eventTemplate: schedulerHtmlTemplates.eventEventTemplateHtml,
                dataSource: {
                    transport: {
                        read: {
                            url: schedulerData.eventsUrl,
                            dataType: "json",
                            traditional: true,
                            type: "GET"
                        },
                        parameterMap: function (options, operation) {
                            if (operation === "read") {
                                var scheduler = kendoScheduler.data("kendoScheduler");
                                var startDate = scheduler.view().startDate();
                                var endDate = scheduler.view().endDate();
                                var localOffset = schedulerData.localTimezoneOffset() * 60000;
                                var filter = {};
                                filter.StartDate = new Date(startDate.getTime() - schedulerData.timezoneOffset - localOffset).toISOString();
                                filter.EndDate = new Date(endDate.getTime() - schedulerData.timezoneOffset - localOffset).toISOString();
                                filter.CalendarList = schedulerData.allowCalendarFilter ? $.makeArray(schedulerData.calendarlistWrapper.find('[data-sf-role="calendarlist-item"].' + schedulerData.calendarListClassActive).attr("data-sf-id")) : [];
                                filter.EventSchedulerViewMode = scheduler.view().options.name.replace("View", "");
                                filter.UICulture = schedulerData.uiCulture;
                                filter.Id = schedulerData.widgetId;
                                filter.sf_site = schedulerData.siteid;
                                return filter;
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
                                eventUrl: { from: "EventUrl" }
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
                                    traditional: true,
                                    type: "GET",
                                    complete: function (jqXHR, textStatus) {
                                        if (schedulerData.allowCalendarFilter && jqXHR && jqXHR.responseJSON) {
                                            var calendarData = jqXHR.responseJSON;
                                            calendarData = $.grep(calendarData, function (e) { return e.title !== null; });
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
                                        var filter = {};
                                        filter.UICulture = schedulerData.uiCulture;
                                        filter.Id = schedulerData.widgetId;
                                        filter.sf_site = schedulerData.siteid;
                                        return filter;
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
            if (schedulerData.allowCalendarFilter) {
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