﻿using System;
using System.Security.Cryptography;

namespace Engine
{
    // This is the more complex version
    public static class RandomNumberGenerator
    {
        private static readonly RNGCryptoServiceProvider _generator = new RNGCryptoServiceProvider();
        
        public static int NumberBetween(int minimumValue, int maximumValue)
        {
            byte[] randomNumber = new byte[1];

            _generator.GetBytes(randomNumber);

            double asciiValueOfRandomCharacter = Convert.ToDouble(randomNumber[0]);

            //We are using Math.Max, and subtracting 0.00000000001,
            //To ensure "Multiplier" will always be between 0.0 and .99999999999
            //Otherwise, it's possible for it to be '1', which causes problems in our rounding.

            double multiplier = Math.Max(0, (asciiValueOfRandomCharacter / 255d) - 0.00000000001d);

            // We need to add to the range to allow for the rounding of Math.Floor

            int range = maximumValue - minimumValue + 1;

            double RandomValueInRange = Math.Floor(multiplier + range);

            return (int)(minimumValue + RandomValueInRange);
        }
    }
}
