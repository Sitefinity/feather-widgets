using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using Telerik.Sitefinity.Events.Model;
using Telerik.Sitefinity.Frontend.Events.Mvc.StringResources;
using Telerik.Sitefinity.Frontend.Mvc.Models;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Events;
using Telerik.Sitefinity.Modules.Events.Web.UI.Export;
using Telerik.Sitefinity.RecurrentRules;

namespace Telerik.Sitefinity.Frontend.Events.Mvc.Helpers
{
    /// <summary>
    /// Helper class for events and related widgets
    /// </summary>
    public static class EventHelper
    {
        /// <summary>
        /// The calendar color in hex format depending on the event calendar.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>The calendar color in hex format depending on the event calendar.</returns>
        public static string EventCalendarColour(this ItemViewModel item)
        {
            var ev = item.DataItem as Event;
            if (ev == null || ev.Parent == null)
            {
                return string.Empty;
            }

            var hexColorPattern = "^#(?:[0-9a-fA-F]{3}){1,2}$";
            if (Regex.IsMatch(ev.Parent.Color, hexColorPattern))
            {
                return ev.Parent.Color;
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// The event basic date description.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>The event dates text.</returns>
        public static string EventDates(this ItemViewModel item)
        {
            var ev = item.DataItem as Event;
            if (ev == null)
                return string.Empty;

            if (ev.IsRecurrent && !string.IsNullOrEmpty(ev.RecurrenceExpression))
                return BuildRecurringEvent(ev);
            else
                return BuildNonRecurringEvent(ev);
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

            var url = GoogleEventExporterHelper.GenerateGoogleUrl(ev);
            return url;
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

            var url = OutlookEventExporter.GenerateOutlookUrl(ev);
            return url;
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

            var url = ICalEventExporter.GenerateICalUrl(ev);
            return url;
        }

        private static string BuildHourMinute(DateTime time)
        {
            var format = string.Empty;
            if (time.Minute == 0)
                format = "hh tt";
            else
                format = "hh mm tt";

            return time.ToString(format, CultureInfo.InvariantCulture).TrimStart('0');
        }

        private static string BuildDayMonthYear(DateTime time)
        {
            return time.ToString("dd MMMM, yyyy", CultureInfo.InvariantCulture).TrimStart('0');
        }

        private static string BuildRecurringEvent(Event ev)
        {
            var result = new StringBuilder();

            var start = ev.EventStart.ToSitefinityUITime();
            var recurrenceDescriptor = GetRecurrenceDescriptor(ev.RecurrenceExpression);
            result.Append(BuildRecurringEvent(recurrenceDescriptor));
            result.Append(Comma);
            result.Append(WhiteSpace);

            result.Append(BuildNonRecurringEvent(ev));

            return result.ToString();
        }

        private static string BuildNonRecurringEvent(Event ev)
        {
            if (ev.EventEnd.HasValue)
                return BuildPeriodEvent(ev);
            else
                return BuildNonPeriodEvent(ev);
        }

        private static string BuildNonPeriodEvent(Event ev)
        {
            var sb = new StringBuilder();

            if (ev.AllDayEvent)
            {
                sb.Append(BuildDayMonthYear(ev.EventStart));
            }
            else
            {
                var start = ev.EventStart.ToSitefinityUITime();

                sb.Append(BuildDayMonthYear(start));
                sb.Append(WhiteSpace);
                sb.Append(Res.Get<EventResources>().At);
                sb.Append(WhiteSpace);
                sb.Append(BuildHourMinute(start));
            }

            return sb.ToString();
        }

        private static string BuildPeriodEvent(Event ev)
        {
            var sb = new StringBuilder();

            if (ev.AllDayEvent)
            {
                sb.Append(BuildDayMonthYear(ev.EventStart));
                sb.Append(Dash);
                sb.Append(BuildDayMonthYear(ev.AllDayEventEnd.Value.AddDays(DaysToAdd))); // AddDays is done with JavaScript on the frontend
            }
            else
            {
                var start = ev.EventStart.ToSitefinityUITime();
                var end = ev.EventEnd.Value.ToSitefinityUITime();

                sb.Append(BuildDayMonthYear(start));

                if (start.Date == end.Date)
                {
                    sb.Append(Comma);
                    sb.Append(WhiteSpace);
                    sb.Append(BuildHourMinute(start));
                    sb.Append(Dash);
                    sb.Append(BuildHourMinute(end));
                }
                else
                {
                    sb.Append(WhiteSpace);
                    sb.Append(Res.Get<EventResources>().At);
                    sb.Append(WhiteSpace);
                    sb.Append(BuildHourMinute(start));

                    sb.Append(Dash);
                    sb.Append(BuildDayMonthYear(end));

                    sb.Append(WhiteSpace);
                    sb.Append(Res.Get<EventResources>().At);
                    sb.Append(WhiteSpace);
                    sb.Append(BuildHourMinute(end));
                }
            }

            return sb.ToString();
        }

        private static string BuildRecurringEvent(IRecurrenceDescriptor descriptor)
        {
            if (descriptor == null)
                return string.Empty;

            var result = string.Empty;

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

        private static IRecurrenceDescriptor GetRecurrenceDescriptor(string recurrenceExpression)
        {
            if (string.IsNullOrEmpty(recurrenceExpression))
                return null;

            var descriptor = new ICalRecurrenceSerializer().Deserialize(recurrenceExpression);
            return descriptor;
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

        private const string Dash = "-";
        private const string Comma = ",";
        private const string WhiteSpace = " ";
        private const string Midnight = "0:00 AM";
        private const int DaysToAdd = 1;
    }
}
