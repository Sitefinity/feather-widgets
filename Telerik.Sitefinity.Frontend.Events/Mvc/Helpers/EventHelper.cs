using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using Telerik.Sitefinity.Events.Model;
using Telerik.Sitefinity.Frontend.Events.Mvc.StringResources;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Events;
using Telerik.Sitefinity.RecurrentRules;

namespace Telerik.Sitefinity.Frontend.Events.Mvc.Helpers
{
    /// <summary>
    /// Helper class for events and related widgets
    /// </summary>
    public static class EventHelper
    {
        #region Public methods

        /// <summary>
        /// The calendar color in hex format depending on the event calendar.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>The calendar color in hex format depending on the event calendar.</returns>
        public static string EventCalendarColour(this ItemViewModel item)
        {
            var ev = item.DataItem as Event;
            if (ev == null || ev.Parent == null)
                return string.Empty;

            return ev.Parent.Color;
        }

        /// <summary>
        /// The event dates text.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>The event dates text.</returns>
        public static string EventDates(this ItemViewModel item)
        {
            var ev = item.DataItem as Event;
            if (ev == null)
                return string.Empty;

            var result = string.Empty;

            if (ev.IsRecurrent && !string.IsNullOrEmpty(ev.RecurrenceExpression))
                result = BuildRecurringEvent(ev);
            else if (ev.EventEnd.HasValue)
                result = BuildPeriodEvent(ev);
            else
                result = BuildNonPeriodEvent(ev);

            result = RemoveTrailingZeros(result);

            return result;
        }

        /// <summary>
        /// The event full dates text.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>The event full dates text.</returns>
        public static string EventFullDates(this ItemViewModel item)
        {
            var ev = item.DataItem as Event;
            if (ev == null)
                return string.Empty;
            
            var result = string.Empty;

            if (ev.IsRecurrent && !string.IsNullOrEmpty(ev.RecurrenceExpression))
                result = BuildNextOccurrence(ev);
            else if (ev.EventEnd.HasValue)
                result = BuildFullEventDates(ev);

            result = RemoveTrailingZeros(result);

            return result;
        }
        
        /// <summary>
        /// Generates the google URL.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>The google URL.</returns>
        public static string GenerateGoogleUrl(this ItemViewModel item)
        {
            var ev = item.DataItem as Event;
            if (ev == null)
                return string.Empty;

            var url = GenerateGoogleUrlMethodInfo.Value.Invoke(null, new object[] { ev });
            return url as string;
        }

        /// <summary>
        /// Generates the outlook URL.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>The outlook URL.</returns>
        public static string GenerateOutlookUrl(this ItemViewModel item)
        {
            var ev = item.DataItem as Event;
            if (ev == null)
                return string.Empty;

            var url = GenerateOutlookUrlMethodInfo.Value.Invoke(GenerateOutlookUrlInstance.Value, new object[] { ev });
            return url as string;
        }

        /// <summary>
        /// Generates the iCal URL.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>The iCal URL.</returns>
        public static string GenerateICalUrl(this ItemViewModel item)
        {
            var ev = item.DataItem as Event;
            if (ev == null)
                return string.Empty;

            var url = GenerateICalUrlMethodInfo.Value.Invoke(GenerateICalUrlInstance.Value, new object[] { ev });
            return url as string;
        }

        #endregion       

        #region EventDates
        
        private static string BuildNonPeriodEvent(Event ev)
        {
            var result = new StringBuilder();

            var prefix = GetEventPrefix(ev);
            if (!string.IsNullOrEmpty(prefix))
            {
                result.Append(prefix);
                result.Append(PartsSeparator);
            }

            result.Append(GetEventStartDate(ev));

            result.Append(SpaceSeparator);
            result.Append(Res.Get<EventResources>().At);
            result.Append(SpaceSeparator);

            var datePart = RemoveTrailingZeros(result.ToString());

            result.Clear();
            result.Append(datePart);
            result.Append(BuildHourMinute(ev.EventStart));

            return result.ToString();
        }

        private static string BuildHourMinute(DateTime dateTime)
        {
            var result = new StringBuilder();
            var hour = dateTime.ToString(HourFormat);
            result.Append(RemoveTrailingZeros(hour));

            if (dateTime.Minute != 0)
            {
                result.Append(SpaceSeparator);
                result.Append(dateTime.ToString(MinuteFormat));
            }

            result.Append(SpaceSeparator);
            result.Append(dateTime.ToString(AmPmFormat));
            
            return result.ToString();
        }

        private static string BuildPeriodEvent(Event ev)
        {
            var result = new StringBuilder();
            
            var start = ev.AllDayEvent ? ev.EventStart : ev.EventStart.ToSitefinityUITime();
            var end = ev.AllDayEvent ? ev.EventEnd.Value : ev.EventEnd.Value.ToSitefinityUITime();
            
            if (start.Date == end.Date)
            {
                var prefix = GetEventPrefix(ev);
                if (!string.IsNullOrEmpty(prefix))
                {
                    result.Append(prefix);
                    result.Append(PartsSeparator);
                }

                result.Append(GetEventStartDate(ev));
                result.Append(PartsSeparator);
                var datePart = RemoveTrailingZeros(result.ToString());

                result.Clear();
                result.Append(datePart);
                result.Append(BuildHourMinute(ev.EventStart));
                result.Append(DashSeparator);
                result.Append(BuildHourMinute(ev.EventEnd.Value));
            }
            else
            {
                if (start.Year == end.Year)
                {
                    result.Append(start.ToString(MonthDayFormat));
                    result.Append(DashSeparator);

                    if (start.Month == end.Month)
                        result.Append(end.ToString(DayFormat));
                    else
                        result.Append(end.ToString(MonthDayFormat));

                    if (start.Year != DateTime.UtcNow.Year)
                    {
                        result.Append(PartsSeparator);
                        result.Append(start.ToString(YearFormat));
                    }
                }
                else
                {
                    result.Append(start.ToString(MonthDayYearFormat));
                    result.Append(DashSeparator);
                    result.Append(end.ToString(MonthDayYearFormat));
                }
            }
            
            return result.ToString();
        }

        private static string BuildFullEventDates(Event ev)
        {
            var finalResult = new StringBuilder();
            var result = new StringBuilder();

            var start = ev.AllDayEvent ? ev.EventStart : ev.EventStart.ToSitefinityUITime();
            var end = ev.AllDayEvent ? ev.EventEnd.Value : ev.EventEnd.Value.ToSitefinityUITime();

            if (start.Year == DateTime.UtcNow.Year)
                result.Append(start.ToString(MonthDayFormat));
            else
                result.Append(start.ToString(MonthDayYearFormat));

            result.Append(SpaceSeparator);
            result.Append(Res.Get<EventResources>().At);
            result.Append(SpaceSeparator);
            finalResult.Append(RemoveTrailingZeros(result.ToString()));
            finalResult.Append(BuildHourMinute(start));

            result.Clear();
            result.Append(SpaceSeparator);
            result.Append(Res.Get<EventResources>().To);
            result.Append(SpaceSeparator);

            if (end.Year == DateTime.UtcNow.Year)
                result.Append(end.ToString(MonthDayFormat));
            else
                result.Append(end.ToString(MonthDayYearFormat));

            result.Append(SpaceSeparator);
            result.Append(Res.Get<EventResources>().At);
            result.Append(SpaceSeparator);
            finalResult.Append(RemoveTrailingZeros(result.ToString()));
            finalResult.Append(BuildHourMinute(end));

            return finalResult.ToString();
        }
        
        private static string GetEventPrefix(Event ev)
        {
            var result = string.Empty;

            var now = DateTime.UtcNow;
            if (ev.EventStart < now)
            {
                if (ev.EventStart.Date == now.Date.AddDays(-1))
                    result = Res.Get<EventResources>().Yesterday;
                else if (now.Date.AddDays(-1 * (int)now.DayOfWeek).AddDays(-7) < ev.EventStart)
                    result = Res.Get<EventResources>().LastWeek;
            }
            else
            {
                if (ev.EventStart.Date == now.Date)
                    result = Res.Get<EventResources>().Today;
                else if (ev.EventStart.Date == now.Date.AddDays(1))
                    result = Res.Get<EventResources>().Tomorrow;
                else
                    result = ev.EventStart.DayOfWeek.ToString();
            }

            return result;
        }

        private static string GetEventStartDate(Event ev)
        {
            var date = ev.AllDayEvent ? ev.EventStart : ev.EventStart.ToSitefinityUITime();
            if (date.Year == DateTime.UtcNow.Year)
                return date.ToString(MonthDayFormat);
            else
                return date.ToString(MonthDayYearFormat);
        }
        
        private static string GetEventEndDate(Event ev)
        {
            if (!ev.EventEnd.HasValue)
                return string.Empty;
            
            var date = ev.AllDayEvent ? ev.EventEnd.Value : ev.EventEnd.Value.ToSitefinityUITime();

            var result = string.Empty;
            if (ev.EventStart.Year != ev.EventEnd.Value.Year)
                result = date.ToString(MonthDayYearFormat);
            else if (ev.EventStart.Month != ev.EventEnd.Value.Month)
                result = date.ToString(MonthDayFormat);
            else if (ev.EventStart.Day != ev.EventEnd.Value.Day)
                result = date.ToString(DayFormat);

            return result;
        }

        private static string RemoveTrailingZeros(string str)
        {
            // Removing trailing 0s -> November 02 at 08 PM => November 2 at 8 PM
            return Regex.Replace(str, @"([ -])0(\d+)", "$1$2");
        }

        #endregion

        #region Occurrence

        private static string BuildRecurringEvent(Event ev)
        {
            var descriptor = GetRecurrenceDescriptor(ev.RecurrenceExpression);

            string result = null;

            switch (descriptor.Frequency)
            {
                case RecurrenceFrequency.Daily: result = BuildFromDaily(descriptor);
                    break;
                case RecurrenceFrequency.Weekly: result = BuildFromWeekly(descriptor);
                    break;
                case RecurrenceFrequency.Monthly: result = BuildFromMonthly(descriptor);
                    break;
                case RecurrenceFrequency.Yearly: result = BuildFromYearly(descriptor);
                    break;
                default:
                    break;
            }

            return result;
        }

        private static string BuildNextOccurrence(Event ev)
        {
            var descriptor = GetRecurrenceDescriptor(ev.RecurrenceExpression);
            var nextOccurrence = descriptor.Occurrences.OrderBy(o => o).FirstOrDefault(o => o >= DateTime.UtcNow);
            if (nextOccurrence == null)
                return string.Empty;

            nextOccurrence = nextOccurrence.ToSitefinityUITime();

            var result = new StringBuilder();

            if (nextOccurrence.Year == DateTime.UtcNow.Year)
                result.Append(nextOccurrence.ToString(MonthDayFormat));
            else
                result.Append(nextOccurrence.ToString(MonthDayYearFormat));

            result.Append(SpaceSeparator);
            result.Append(Res.Get<EventResources>().At);
            result.Append(SpaceSeparator); 
            var datePart = RemoveTrailingZeros(result.ToString());

            result.Clear();
            result.Append(datePart);
            result.Append(BuildHourMinute(nextOccurrence));

            return result.ToString();
        }

        private static IRecurrenceDescriptor GetRecurrenceDescriptor(string recurrenceExpression)
        {
            var descriptor = ICalRecurrenceSerializerDeserializeMethodInfo.Value.Invoke(ICalRecurrenceSerializerInstance.Value, new object[] { recurrenceExpression });
            return descriptor as IRecurrenceDescriptor;
        }

        private static string BuildFromDaily(IRecurrenceDescriptor recurrenceDescriptor)
        {
            var result = string.Empty;

            if (recurrenceDescriptor.DaysOfWeek == RecurrenceDay.WeekDays)
            {
                result = Res.Get<Labels>("EveryWeekday");
            }
            else if (recurrenceDescriptor.DaysOfWeek == RecurrenceDay.EveryDay)
            {
                if (recurrenceDescriptor.Interval == 1)
                {
                    result = Res.Get<Labels>("Every") + Res.Get<Labels>("Day").ToLower();
                }
                else
                {
                    result = Res.Get<Labels>("Every") + " " + recurrenceDescriptor.Interval + " " + Res.Get<Labels>("Days").ToLower();
                }
            }

            return result;
        }

        private static string BuildFromWeekly(IRecurrenceDescriptor recurrenceDescriptor)
        {
            var result = string.Empty;

            if (recurrenceDescriptor.Interval == 1)
            {
                result = Res.Get<Labels>("Every") + " " + Res.Get<EventsResources>("Week").ToLower() + " " + Res.Get<Labels>("On").ToLower();
            }
            else
            {
                result = Res.Get<Labels>("Every") + " " + recurrenceDescriptor.Interval + " " + Res.Get<EventsResources>("Weeks").ToLower() + " " + Res.Get<Labels>("On").ToLower();
            }

            var days = new List<string>() { };
            if (recurrenceDescriptor.DaysOfWeek.HasFlag(RecurrenceDay.Monday))
            {
                days.Add(Res.Get<Labels>(RecurrenceDay.Monday.ToString()));
            }

            if (recurrenceDescriptor.DaysOfWeek.HasFlag(RecurrenceDay.Tuesday))
            {
                days.Add(Res.Get<Labels>(RecurrenceDay.Tuesday.ToString()));
            }

            if (recurrenceDescriptor.DaysOfWeek.HasFlag(RecurrenceDay.Wednesday))
            {
                days.Add(Res.Get<Labels>(RecurrenceDay.Wednesday.ToString()));
            }

            if (recurrenceDescriptor.DaysOfWeek.HasFlag(RecurrenceDay.Thursday))
            {
                days.Add(Res.Get<Labels>(RecurrenceDay.Thursday.ToString()));
            }

            if (recurrenceDescriptor.DaysOfWeek.HasFlag(RecurrenceDay.Friday))
            {
                days.Add(Res.Get<Labels>(RecurrenceDay.Friday.ToString()));
            }

            if (recurrenceDescriptor.DaysOfWeek.HasFlag(RecurrenceDay.Saturday))
            {
                days.Add(Res.Get<Labels>(RecurrenceDay.Saturday.ToString()));
            }

            if (recurrenceDescriptor.DaysOfWeek.HasFlag(RecurrenceDay.Sunday))
            {
                days.Add(Res.Get<Labels>(RecurrenceDay.Sunday.ToString()));
            }

            result += string.Concat(" ", string.Join(", ", days));

            return result;
        }

        private static string BuildFromMonthly(IRecurrenceDescriptor recurrenceDescriptor)
        {
            var result = string.Empty;

            if (recurrenceDescriptor.DayOfMonth == 0)
            {
                var occurrenceOrdinal = BuildOccurrenceOrdinal(recurrenceDescriptor);
                result = string.Concat(occurrenceOrdinal, " ", Res.Get<Labels>(recurrenceDescriptor.DaysOfWeek.ToString()), " ", Res.Get<Labels>("OfEvery"));
            }
            else
            {
                result = string.Concat(Res.Get<Labels>("Day"), " ", recurrenceDescriptor.DayOfMonth, " ", Res.Get<Labels>("OfEvery"));
            }

            if (recurrenceDescriptor.Interval == 1)
            {
                result += string.Concat(" ", Res.Get<Labels>("MonthOrMonths").ToLower());
            }
            else
            {
                result += string.Concat(" ", recurrenceDescriptor.Interval, " ", Res.Get<Labels>("MonthOrMonths").ToLower());
            }

            return result;
        }

        private static string BuildFromYearly(IRecurrenceDescriptor recurrenceDescriptor)
        {
            var result = string.Empty;

            if (recurrenceDescriptor.DayOrdinal == 0)
            {
                result = string.Concat(Res.Get<Labels>("Every"), " ", recurrenceDescriptor.DayOfMonth, " ", Res.Get<Labels>("Day").ToLower(), " ", Res.Get<Labels>("Of").ToLower(), " ", Res.Get<Labels>(recurrenceDescriptor.Month.ToString()));
            }
            else
            {
                var occurrenceOrdinal = BuildOccurrenceOrdinal(recurrenceDescriptor);
                result = string.Concat(Res.Get<Labels>("The"), " ", occurrenceOrdinal.ToLower());
                result += string.Concat(" ", Res.Get<Labels>(recurrenceDescriptor.DaysOfWeek.ToString()), " ", Res.Get<Labels>("Of").ToLower(), " ", Res.Get<Labels>(recurrenceDescriptor.Month.ToString()));
            }

            return result;
        }

        private static string BuildOccurrenceOrdinal(IRecurrenceDescriptor recurrenceDescriptor)
        {
            switch (recurrenceDescriptor.DayOrdinal)
            {
                case 1: return char.ToUpper(Res.Get<Labels>("First")[0]) + Res.Get<Labels>("First").Substring(1);
                case 2: return char.ToUpper(Res.Get<Labels>("Second")[0]) + Res.Get<Labels>("Second").Substring(1);
                case 3: return char.ToUpper(Res.Get<Labels>("Third")[0]) + Res.Get<Labels>("Third").Substring(1);
                case 4: return char.ToUpper(Res.Get<Labels>("Fourth")[0]) + Res.Get<Labels>("Fourth").Substring(1);
                default: return char.ToUpper(Res.Get<Labels>("Last")[0]) + Res.Get<Labels>("Last").Substring(1);
            }
        }

        #endregion

        #region Private fields

        private static readonly Lazy<MethodInfo> GenerateGoogleUrlMethodInfo =
            new Lazy<MethodInfo>(() => Type.GetType("Telerik.Sitefinity.Modules.Events.Web.UI.Export.GoogleEventExporterHelper, Telerik.Sitefinity.ContentModules").GetMethod("GenerateGoogleUrl", BindingFlags.Public | BindingFlags.Static));

        private static readonly Lazy<object> GenerateOutlookUrlInstance =
            new Lazy<object>(() => Activator.CreateInstance(Type.GetType("Telerik.Sitefinity.Modules.Events.Web.UI.Export.OutlookEventExporter, Telerik.Sitefinity.ContentModules")));

        private static readonly Lazy<MethodInfo> GenerateOutlookUrlMethodInfo =
            new Lazy<MethodInfo>(() => Type.GetType("Telerik.Sitefinity.Modules.Events.Web.UI.Export.OutlookEventExporter, Telerik.Sitefinity.ContentModules").GetMethod("GenerateOutlookUrl", BindingFlags.NonPublic | BindingFlags.Instance));

        private static readonly Lazy<object> GenerateICalUrlInstance =
            new Lazy<object>(() => Activator.CreateInstance(Type.GetType("Telerik.Sitefinity.Modules.Events.Web.UI.Export.ICalEventExporter, Telerik.Sitefinity.ContentModules")));

        private static readonly Lazy<MethodInfo> GenerateICalUrlMethodInfo =
            new Lazy<MethodInfo>(() => Type.GetType("Telerik.Sitefinity.Modules.Events.Web.UI.Export.ICalEventExporter, Telerik.Sitefinity.ContentModules").GetMethod("GenerateICalUrl", BindingFlags.NonPublic | BindingFlags.Instance));

        private static readonly Lazy<object> ICalRecurrenceSerializerInstance =
            new Lazy<object>(() => Activator.CreateInstance(Type.GetType("Telerik.Sitefinity.RecurrentRules.ICalRecurrenceSerializer, Telerik.Sitefinity.RecurrentRules")));

        private static readonly Lazy<MethodInfo> ICalRecurrenceSerializerDeserializeMethodInfo =
            new Lazy<MethodInfo>(() => Type.GetType("Telerik.Sitefinity.RecurrentRules.ICalRecurrenceSerializer, Telerik.Sitefinity.RecurrentRules").GetMethod("Deserialize", BindingFlags.Instance | BindingFlags.Public));
        
        #endregion

        #region Constants

        private const string PartsSeparator = ", ";
        private const string DashSeparator = "-";
        private const string SpaceSeparator = " ";
        private const string HourFormat = "hh";
        private const string MinuteFormat = "mm";
        private const string AmPmFormat = "tt";
        private const string DayFormat = "dd";
        private const string MonthDayFormat = "MMMM dd";
        private const string MonthDayYearFormat = "MMMM dd, yyyy";
        private const string YearFormat = "yyyy";

        #endregion
    }
}
