using System.Globalization;
using System.Net.Sockets;
using System.IO;

namespace System
{
    public static class DateTimeExtensions
    {
        // This presumes that weeks start with Monday.
        // Week 1 is the 1st week of the year with a Thursday in it.

        private static bool _nistSynchronised = false;
        private static TimeSpan _nistDiffrenceTimestamp;

        public static DateTime GetNISTNow()
        {
            if (!_nistSynchronised)
            {
                DateTime nistDateTime;
                DateTime now = DateTime.Now;

                TcpClient client = new TcpClient("time.nist.gov", 13);
                using (var streamReader = new StreamReader(client.GetStream()))
                {
                    string response = streamReader.ReadToEnd();
                    string utcDateTimeString = response.Substring(7, 17);
                    nistDateTime = DateTime.ParseExact(utcDateTimeString, "yy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal);
                }

                _nistDiffrenceTimestamp = nistDateTime - now;
                _nistSynchronised = true;
            }

            return DateTime.Now.Add(_nistDiffrenceTimestamp);
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
