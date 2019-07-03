using System.Globalization;        
using System.Net.Sockets;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Text.RegularExpressions;
using System.Linq;
using System.Text;

namespace System
{
    public static class DateTimeExtensions
    {
        //public static TimeSpan GetSpanFromNowToTimespan(string timestamp)
        //{
        //    DateTime now = DateTime.Now;

        //    if (now.IsLaterThan(timestamp))
        //        throw new ArgumentException("Provided timestamp is associated with a date in the past.");

        //    if (!int.TryParse(timestamp.Substring(11, 2), out int hour))
        //        throw new ArgumentException($"Invalid timestamp format. Value of the hour cannot be obtained from: '{timestamp.Substring(11, 2)}'", nameof(timestamp));

        //    if (!int.TryParse(timestamp.Substring(14, 2), out int minute))
        //        throw new ArgumentException($"Invalid timestamp format. Value of the minute cannot be obtained from: '{timestamp.Substring(14, 2)}'", nameof(timestamp));

        //    if (!int.TryParse(timestamp.Substring(17, 2), out int second))
        //        throw new ArgumentException($"Invalid timestamp format. Value of the second cannot be obtained from: '{timestamp.Substring(17, 2)}'", nameof(timestamp));

        //    int hoursFromStamp = hour - now.Hour;
        //    if (hoursFromStamp < 0)
        //        hoursFromStamp = 0;

        //    int minutesFromStamp = minute - now.Minute;
        //    if (minutesFromStamp < 0)
        //        minutesFromStamp = 0;

        //    int secondsFromStamp = second - now.Second;
        //    if (secondsFromStamp < 0)
        //        secondsFromStamp = 0;

        //    return new TimeSpan(hoursFromStamp, minutesFromStamp, secondsFromStamp);
        //}
        //// 0 sec+
        //public static TimeSpan GetSpanFromTimespanToNow(string timestamp)
        //{
        //    DateTime now = DateTime.Now;

        //    if (now.IsEarlierThan(timestamp))
        //        throw new ArgumentException("Provided timestamp is associated with a date in a future.");

        //    if (!int.TryParse(timestamp.Substring(11, 2), out int hour))
        //        throw new ArgumentException($"Invalid timestamp format. Value of the hour cannot be obtained from: '{timestamp.Substring(11, 2)}'", nameof(timestamp));

        //    if (!int.TryParse(timestamp.Substring(14, 2), out int minute))
        //        throw new ArgumentException($"Invalid timestamp format. Value of the minute cannot be obtained from: '{timestamp.Substring(14, 2)}'", nameof(timestamp));

        //    if (!int.TryParse(timestamp.Substring(17, 2), out int second))
        //        throw new ArgumentException($"Invalid timestamp format. Value of the second cannot be obtained from: '{timestamp.Substring(17, 2)}'", nameof(timestamp));

        //    int hoursFromStamp = now.Hour - hour;
        //    if (hoursFromStamp < 0)
        //        hoursFromStamp = 0;

        //    int minutesFromStamp = now.Minute - minute;
        //    if (minutesFromStamp < 0)
        //        minutesFromStamp = 0;

        //    int secondsFromStamp = now.Second - second;
        //    if (secondsFromStamp < 0)
        //        secondsFromStamp = 0;

        //    return new TimeSpan(hoursFromStamp, minutesFromStamp, secondsFromStamp);
        //}

        private static bool _internetSynchronised = false;
        private static TimeSpan _internetDiffrenceTimestamp;

        public static DateTime GetInternetNow()
        {
            if (!_internetSynchronised)
            {
                int internetHour, internetMinute, internetSecond;
                DateTime now = DateTime.Now;
                DateTime internetDateTime = DateTime.Now;

                //string urlAddress = "https://time.nist.gov";
                string urlAddress = "https://www.timeanddate.com/worldclock/uk";
                //string urlAddress = "http://nist.time.gov/actualtime.cgi?lzbc=siqm9b";
                //string urlAddress = "https://www.google.com/search?q=what+time+is+in+uk+right+now";

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlAddress);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    Stream receiveStream = response.GetResponseStream();
                    StreamReader readStream = null;

                    if (response.CharacterSet == null)
                    {
                        readStream = new StreamReader(receiveStream);
                    }
                    else
                    {
                        readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                    }

                    string data = readStream.ReadToEnd();

                    int startIndex = data.IndexOf("<span id=ct class=h1>") + "<span id=ct class=h1>".Length; ;

                    string dateTimeStamp = data.Substring(startIndex, 8);

                    internetHour = int.Parse(dateTimeStamp.Substring(0,2));
                    internetMinute = int.Parse(dateTimeStamp.Substring(3,2));
                    internetSecond = int.Parse(dateTimeStamp.Substring(6,2));

                    internetDateTime = new DateTime(now.Year, now.Month, now.Day, internetHour, internetMinute, internetSecond);

                    response.Close();
                    readStream.Close();
                }

                _internetDiffrenceTimestamp = internetDateTime - now;
                _internetSynchronised = true;
            }

            return DateTime.Now.Add(_internetDiffrenceTimestamp);
        }

        public static int GetIso8601WeekOfYear(this DateTime time)
        {
            // Seriously cheat.  If its Monday, Tuesday or Wednesday, then it'll 
            // be the same week# as whatever Thursday, Friday or Saturday are,
            // and we always get those right
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                time = time.AddDays(3);
            }

            // Return the week of our adjusted day
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

        public static string ToTimestamp(this DateTime dateTime)
        {
            // Lenght.23
            return dateTime.ToString("yyyy/MM/dd HH:mm:ss:fff");
        }

        public static DateTime FromTimestamp(string timestamp)
        {
            if (!int.TryParse(timestamp.Substring(0, 4), out int year))
                throw new ArgumentException($"Invalid timestamp format. Value of the year cannot be obtained from: '{timestamp.Substring(0, 4)}'", nameof(timestamp));

            if (!int.TryParse(timestamp.Substring(5, 2), out int month))
                throw new ArgumentException($"Invalid timestamp format. Value of the month cannot be obtained from: '{timestamp.Substring(5, 2)}'", nameof(timestamp));

            if (!int.TryParse(timestamp.Substring(8, 2), out int day))
                throw new ArgumentException($"Invalid timestamp format. Value of the day cannot be obtained from: '{timestamp.Substring(8, 2)}'", nameof(timestamp));

            if (!int.TryParse(timestamp.Substring(11, 2), out int hour))
                throw new ArgumentException($"Invalid timestamp format. Value of the hour cannot be obtained from: '{timestamp.Substring(11, 2)}'", nameof(timestamp));

            if (!int.TryParse(timestamp.Substring(14, 2), out int minute))
                throw new ArgumentException($"Invalid timestamp format. Value of the minute cannot be obtained from: '{timestamp.Substring(14, 2)}'", nameof(timestamp));

            if (!int.TryParse(timestamp.Substring(17, 2), out int second))
                throw new ArgumentException($"Invalid timestamp format. Value of the second cannot be obtained from: '{timestamp.Substring(17, 2)}'", nameof(timestamp));

            if (!int.TryParse(timestamp.Substring(20, 3), out int millisecond))
                throw new ArgumentException($"Invalid timestamp format. Value of the millisecond cannot be obtained from: '{timestamp.Substring(20, 3)}'", nameof(timestamp));

            return new DateTime(year, month, day, hour, minute, second, millisecond);
        }

        public static void DisassembleTimestamp(string timestamp, out int year, out int month, out int day, out int hour, out int minute, out int second, out int millisecond)
        {
            if (!int.TryParse(timestamp.Substring(0, 4), out year))
                throw new ArgumentException($"Invalid timestamp format. Value of the year cannot be obtained from: '{timestamp.Substring(0, 4)}'", nameof(timestamp));

            if (!int.TryParse(timestamp.Substring(5, 2), out month))
                throw new ArgumentException($"Invalid timestamp format. Value of the month cannot be obtained from: '{timestamp.Substring(5, 2)}'", nameof(timestamp));

            if (!int.TryParse(timestamp.Substring(8, 2), out day))
                throw new ArgumentException($"Invalid timestamp format. Value of the day cannot be obtained from: '{timestamp.Substring(8, 2)}'", nameof(timestamp));

            if (!int.TryParse(timestamp.Substring(11, 2), out hour))
                throw new ArgumentException($"Invalid timestamp format. Value of the hour cannot be obtained from: '{timestamp.Substring(11, 2)}'", nameof(timestamp));

            if (!int.TryParse(timestamp.Substring(14, 2), out minute))
                throw new ArgumentException($"Invalid timestamp format. Value of the minute cannot be obtained from: '{timestamp.Substring(14, 2)}'", nameof(timestamp));

            if (!int.TryParse(timestamp.Substring(17, 2), out second))
                throw new ArgumentException($"Invalid timestamp format. Value of the second cannot be obtained from: '{timestamp.Substring(17, 2)}'", nameof(timestamp));

            if (!int.TryParse(timestamp.Substring(20, 3), out millisecond))
                throw new ArgumentException($"Invalid timestamp format. Value of the millisecond cannot be obtained from: '{timestamp.Substring(20, 3)}'", nameof(timestamp));
        }
        
        public static bool IsEqualWithTimestamp(this DateTime dateTime, string timestamp)
        {
            return dateTime.ToTimestamp() == timestamp;
        }

        // Is DateTime later than timestamp
        public static bool IsLaterThan(this DateTime dateTime, string timestamp)
        {
            if (!int.TryParse(timestamp.Substring(0, 4), out int year))
                throw new ArgumentException($"Invalid timestamp format. Value of the year cannot be obtained from: '{timestamp.Substring(0, 4)}'", nameof(timestamp));

            if (dateTime.Year > year) return true;
            else if (dateTime.Year < year) return false;


            if (!int.TryParse(timestamp.Substring(5, 2), out int month))
                throw new ArgumentException($"Invalid timestamp format. Value of the month cannot be obtained from: '{timestamp.Substring(5, 2)}'", nameof(timestamp));

            if (dateTime.Month > month) return true;
            else if (dateTime.Month < month) return false;


            if (!int.TryParse(timestamp.Substring(8, 2), out int day))
                throw new ArgumentException($"Invalid timestamp format. Value of the day cannot be obtained from: '{timestamp.Substring(8, 2)}'", nameof(timestamp));

            if (dateTime.Day > day) return true;
            else if (dateTime.Day < day) return false;


            if (!int.TryParse(timestamp.Substring(11, 2), out int hour))
                throw new ArgumentException($"Invalid timestamp format. Value of the hour cannot be obtained from: '{timestamp.Substring(11, 2)}'", nameof(timestamp));

            if (dateTime.Hour > hour) return true;
            else if (dateTime.Hour < hour) return false;


            if (!int.TryParse(timestamp.Substring(14, 2), out int minute))
                throw new ArgumentException($"Invalid timestamp format. Value of the minute cannot be obtained from: '{timestamp.Substring(14, 2)}'", nameof(timestamp));

            if (dateTime.Minute > minute) return true;
            else if (dateTime.Minute < minute) return false;


            if (!int.TryParse(timestamp.Substring(17, 2), out int second))
                throw new ArgumentException($"Invalid timestamp format. Value of the second cannot be obtained from: '{timestamp.Substring(17, 2)}'", nameof(timestamp));

            if (dateTime.Second > second) return true;
            else if (dateTime.Second < second) return false;

            if (!int.TryParse(timestamp.Substring(20, 3), out int millisecond))
                throw new ArgumentException($"Invalid timestamp format. Value of the millisecond cannot be obtained from: '{timestamp.Substring(20, 3)}'", nameof(timestamp));

            if (dateTime.Millisecond > millisecond) return true;
            else return false;
        }

        // Is DateTime eariel than timestamp
        public static bool IsEarlierThan(this DateTime dateTime, string timestamp)
        {
            if (!int.TryParse(timestamp.Substring(0, 4), out int year))
                throw new ArgumentException($"Invalid timestamp format. Value of the year cannot be obtained from: '{timestamp.Substring(0, 4)}'", nameof(timestamp));

            if (dateTime.Year < year) return true;
            else if (dateTime.Year > year) return false;


            if (!int.TryParse(timestamp.Substring(5, 2), out int month))
                throw new ArgumentException($"Invalid timestamp format. Value of the month cannot be obtained from: '{timestamp.Substring(5, 2)}'", nameof(timestamp));

            if (dateTime.Month < month) return true;
            else if (dateTime.Month > month) return false;


            if (!int.TryParse(timestamp.Substring(8, 2), out int day))
                throw new ArgumentException($"Invalid timestamp format. Value of the day cannot be obtained from: '{timestamp.Substring(8, 2)}'", nameof(timestamp));

            if (dateTime.Day < day) return true;
            else if (dateTime.Day > day) return false;


            if (!int.TryParse(timestamp.Substring(11, 2), out int hour))
                throw new ArgumentException($"Invalid timestamp format. Value of the hour cannot be obtained from: '{timestamp.Substring(11, 2)}'", nameof(timestamp));

            if (dateTime.Hour < hour) return true;
            else if (dateTime.Hour > hour) return false;


            if (!int.TryParse(timestamp.Substring(14, 2), out int minute))
                throw new ArgumentException($"Invalid timestamp format. Value of the minute cannot be obtained from: '{timestamp.Substring(14, 2)}'", nameof(timestamp));

            if (dateTime.Minute < minute) return true;
            else if (dateTime.Minute > minute) return false;


            if (!int.TryParse(timestamp.Substring(17, 2), out int second))
                throw new ArgumentException($"Invalid timestamp format. Value of the second cannot be obtained from: '{timestamp.Substring(17, 2)}'", nameof(timestamp));

            if (dateTime.Second < second) return true;
            else if (dateTime.Second > second) return false;


            if (!int.TryParse(timestamp.Substring(20, 3), out int millisecond))
                throw new ArgumentException($"Invalid timestamp format. Value of the millisecond cannot be obtained from: '{timestamp.Substring(20, 3)}'", nameof(timestamp));

            if (dateTime.Millisecond < millisecond) return true;
            else return false;
        }
        
        public static void GetTimestampParams(out DateTime now, out string dateString, out string timeString)
        {
            // Get current date time
            now = DateTime.Now;

            // Build dateString string based on current date.
            dateString = $"{now.Day:D2}/{now.Month:D2}/{now.Year:D4}";

            //Build lastSeenTime string based on the current time.
            timeString = $"{now.Hour:D2}:{now.Minute:D2}:{now.Second:D2}";
        }
    }
}
