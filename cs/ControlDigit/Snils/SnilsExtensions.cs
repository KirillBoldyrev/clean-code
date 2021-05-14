using System;
using System.Collections.Generic;
using System.Linq;

namespace ControlDigit
{
    public static class SnilsExtensions
    {
        public static int CalculateSnils(this long number)
        {
            int result = 0;

            var digitsByPosition = GetDigitsByPositionFromEnd(number);

            result = digitsByPosition
                .Select(kv => kv.Key * kv.Value)
                .Sum();

            result = ProcessBelow101Cases(result);

            if (result > 101)
            {
                result = ProcessBelow101Cases(result % 101);
            }

            return result;
        }

        private static Dictionary<int, int> GetDigitsByPositionFromEnd(long number)
        {
            var n = number.ToString().Length;
            var result = new Dictionary<int, int>();

            var parsedNumber = number;

            for (var positionFromEnd = 1; positionFromEnd <= n; positionFromEnd++)
            {
                var numberOnPosition = parsedNumber % 10;
                result.Add(positionFromEnd, (int) numberOnPosition);
                parsedNumber = parsedNumber / 10;
            }

            return result;
        }

        private static int ProcessBelow101Cases(int number)
        {
            if (number < 100)
                return number;

            if (number == 100 || number == 101)
                return 0;

            return number;
        }

    }
}
