using System;
using System.Collections.Generic;
using System.Text;

namespace VideoCollection.Model
{
    class ColorGenerator
    {
        private readonly char[] ArrayOfChars = new char[] { 'A', 'B', 'C', 'D', 'E', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
        public string GenerateColor()
        {
            string result = "#";
            for (int i = 0; i < 6; i++)
            {
                result += ArrayOfChars[GenerateValue(0, ArrayOfChars.Length)];
            }
            return result;
        }
        private int GenerateValue(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
    }
}
