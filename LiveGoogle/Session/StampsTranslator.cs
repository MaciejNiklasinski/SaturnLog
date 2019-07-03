using LiveGoogle.Sheets;
using LiveGoogle.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LiveGoogle.Session
{
    internal partial class SessionsRepository
    {
        private static class StampsTranslator
        {
            // Construct stamp string based on the provided date time and current sessionId
            public static string GetStamp(DateTime dateTime)
            {
                return SessionsRepository.GetSessionStamp(dateTime);
            }

            #region Stamp String(s) Separation Helpers
            // Null for disconnected session stamp
            public static string GetTimestampStampSection(string stamp)
            {
                // If provided stamp represents disconnected session..
                if (StampsTranslator.IsRepresentingNonActiveLastStamp(stamp))
                    // ... return null.
                    return null;
                // .. otherwise split stamp
                else
                    // .. and return a timestamp from it.
                    return stamp.Substring(0, SessionsRepository.TimestampLenght);
            }

            public static string GetSessionIdStampSection(string stamp)
            {
                // If provided stamp represents disconnected session..
                if (StampsTranslator.IsRepresentingNonActiveLastStamp(stamp))
                    // return provided stamp as its consist from sessionId only.
                    return stamp;
                // otherwise split string...
                else
                    // .. and return a sessionId from it.
                    return stamp.Substring(SessionsRepository.TimestampLenght);
            }
            #endregion

            #region Stamp String(s) Analyze Helpers
            // Answer the question whether the currently cached session stamp indicates that some other instance 
            public static bool IsOtherInstanceStartingSession(string connectedIn15, string connectedIn30, string connectedIn45, string connectedIn60, string connectedIn90, string connectedIn120)
            {
                // If ANY of the 'ConnectedIn' stamps is not empty, and doesn't represent unresponsive timestamp, and doesn't have equal sessionId return true..
                if (!string.IsNullOrWhiteSpace(connectedIn120) && !StampsTranslator.IsRepresentingUnresponsiveStamp(connectedIn120) && !StampsTranslator.IsRepresentingThisSession(connectedIn120)) return true;
                else if (!string.IsNullOrWhiteSpace(connectedIn90) && !StampsTranslator.IsRepresentingUnresponsiveStamp(connectedIn90) && !StampsTranslator.IsRepresentingThisSession(connectedIn90)) return true;
                else if (!string.IsNullOrWhiteSpace(connectedIn60) && !StampsTranslator.IsRepresentingUnresponsiveStamp(connectedIn60) && !StampsTranslator.IsRepresentingThisSession(connectedIn60)) return true;
                else if (!string.IsNullOrWhiteSpace(connectedIn45) && !StampsTranslator.IsRepresentingUnresponsiveStamp(connectedIn45) && !StampsTranslator.IsRepresentingThisSession(connectedIn45)) return true;
                else if (!string.IsNullOrWhiteSpace(connectedIn30) && !StampsTranslator.IsRepresentingUnresponsiveStamp(connectedIn30) && !StampsTranslator.IsRepresentingThisSession(connectedIn30)) return true;
                else if (!string.IsNullOrWhiteSpace(connectedIn15) && !StampsTranslator.IsRepresentingUnresponsiveStamp(connectedIn15) && !StampsTranslator.IsRepresentingThisSession(connectedIn15)) return true;
                // .. otherwise return false.
                else return false;
            }

            public static bool IsRepresentingNonActiveLastStamp(string lastStamp)
            {
                // If provided last stamp length is equal with the length of the current sessionId,
                // assume that the provided stamp does not contain timestamp session
                // and represent inactive session.
                return (lastStamp.Length == _sessionId.Length);
            }

            public static bool IsRepresentingDisconnectedStamp(string disconnectedStamp, string lastStamp)
            {
                // If provided last stamp length is equal with the length of the current sessionId,
                // and its associated with the same sessionId as disconnectedStamp,
                // assume that the provided stamp does not contain timestamp session, as well as
                // and represent disconnected session.
                return (lastStamp.Length == _sessionId.Length) && (StampsTranslator.GetSessionIdStampSection(disconnectedStamp) == StampsTranslator.GetSessionIdStampSection(lastStamp));
            }

            public static bool IsRepresentingForcedToDisconnectStamp(string forcedToDisconnectStamp, string disconnectedStamp, string lastStamp)
            {
                // If provided last stamp length is equal with the length of the current sessionId,
                // and its associated with the same sessionId as forcedToDisconnectStamp and disconnectedStamp,
                // assume that the provided stamp does not contain timestamp session, as well as
                // and represent session that has been forced to deactivate.
                return (lastStamp.Length == _sessionId.Length)
                    && (StampsTranslator.GetSessionIdStampSection(forcedToDisconnectStamp) == StampsTranslator.GetSessionIdStampSection(disconnectedStamp)
                    && (StampsTranslator.GetSessionIdStampSection(disconnectedStamp) == StampsTranslator.GetSessionIdStampSection(lastStamp)));
            }

            public static bool IsRepresentingUnresponsiveStamp(string stamp)
            {
                // If provided stamp represents disconnected session..
                if (string.IsNullOrWhiteSpace(stamp) || StampsTranslator.IsRepresentingNonActiveLastStamp(stamp))
                    return false;

                // Otherwise...

                // Get timestamp of section from the provided stamp..
                string timestamp = GetTimestampStampSection(stamp);
                // .. and convert it into the DateTime.
                DateTime stampDateTime = DateTimeExtensions.FromTimestamp(timestamp);
                DateTime now = DateTimeExtensions.GetInternetNow();

                // Calculate number of seconds elapsed from the point when the stamp
                // has been created, till 'relativeNow'.
                int stampAgeSec = (int)(now - stampDateTime).TotalSeconds;

                // Is provided stamp describing the time in past 
                // by the number of seconds sufficient to consider it by unresponsive.
                return stampAgeSec > _unresponsiveSecondsLimit;
            }

            public static bool IsRepresentingLongTimeUnresponsiveStamp(string stamp)
            {
                // If provided stamp represents disconnected session..
                if (string.IsNullOrWhiteSpace(stamp) || StampsTranslator.IsRepresentingNonActiveLastStamp(stamp))
                    return false;

                // Otherwise...

                // Get timestamp of section from the provided stamp..
                string timestamp = GetTimestampStampSection(stamp);
                // .. and convert it into the DateTime.
                DateTime stampDateTime = DateTimeExtensions.FromTimestamp(timestamp);
                DateTime now = DateTimeExtensions.GetInternetNow();

                // Calculate number of seconds elapsed from the point when the stamp
                // has been created, till 'relativeNow'.
                int stampAgeSec = (int)(now - stampDateTime).TotalSeconds;

                // Is provided stamp describing the time in past 
                // by the number of seconds sufficient to consider it by unresponsive.
                return stampAgeSec > _longTimeUnresponsiveSecondsLimit;
            }

            // Answers the question whether the provided stamp is representing current session.
            public static bool IsRepresentingThisSession(string stamp)
            {
                if (string.IsNullOrWhiteSpace(stamp))
                    return false;

                // if sessionId section of provided stamp is equal this sessionId return true, otherwise false.
                return StampsTranslator.GetSessionIdStampSection(stamp) == SessionsRepository._sessionId;
            }

            // Answers the question whether two provided stamps represent the same session
            public static bool IsRepresentingSameSession(string stamp1, string stamp2)
            {
                // If both stamp when used as parameters for this.GetSessionIdStampSection(string stamp)
                // method return the same value, assume that the represent the same session.
                return StampsTranslator.GetSessionIdStampSection(stamp1) == StampsTranslator.GetSessionIdStampSection(stamp2);
            }

            // Answers the question whether two provided laterStamp represents stamp located
            // later in thime then other provided stamp, earlier stamp.
            public static bool IsRepresentingLaterSession(string lasterStamp, string earlierStamp)
            {
                if (StampsTranslator.IsRepresentingNonActiveLastStamp(lasterStamp)
                    || StampsTranslator.IsRepresentingNonActiveLastStamp(earlierStamp))
                    throw new ArgumentException("Unable to compare timestamp of non-active last stamp.");

                //
                return (DateTimeExtensions.FromTimestamp(lasterStamp) > DateTimeExtensions.FromTimestamp(earlierStamp));
            }
            #endregion
        }
    }
}
