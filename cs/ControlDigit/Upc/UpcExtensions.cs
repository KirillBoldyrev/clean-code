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

            for (var i = 1; i <= n; i++)
            {
                var position = n - (n - i + 1) + 1;
                var numberOnPosition = parsedNumber % 10;

                if (position % 2 == 1)
                {
                    result = result + numberOnPosition * 3;
                }
                else
                {
                    result = result + numberOnPosition;
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
