using System;

namespace ControlDigit
{
    public static class UpcExtensions
    {
        public static int CalculateUpc(this long number)
        {
            var n = number.ToString().Length;
            long result = 0;

            var parsedNumber = number;

            for (var positionFromEnd = 1; positionFromEnd <= n; positionFromEnd++)
            {
                var numberOnPosition = parsedNumber % 10;

                if (positionFromEnd % 2 == 1)
                {
                    result += numberOnPosition * 3;
                }
                else
                {
                    result += numberOnPosition;
                }

                parsedNumber = parsedNumber / 10;
            }

            result = result % 10;

            if (result != 0)
                result = 10 - result;

            return (int) result;
        }
    }
}
