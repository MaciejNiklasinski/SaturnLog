using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaturnLog.Core
{
    public enum Saturn5MovementType
    {
        Received = 0,
        Sent = 1
    }

    public static class Saturn5MovementTypeExtensions
    {
        public static Saturn5MovementType GetFromString(string movementTypeString)
        {
            movementTypeString = movementTypeString.Trim().ToUpper();

            if (movementTypeString == Saturn5MovementType.Received.ToString().Trim().ToUpper())
                return Saturn5MovementType.Received;
            else if (movementTypeString == Saturn5MovementType.Sent.ToString().Trim().ToUpper())
                return Saturn5MovementType.Sent;
            else
                throw new ArgumentException($"Provided movement type string value: {movementTypeString} doesn't represent any known Saturn5MovementType enum value.", nameof(movementTypeString));
        }
    }

}
