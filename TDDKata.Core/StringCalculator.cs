using System;
using System.Linq;

namespace TDDKata.Core
{
    public class StringCalculator
    {
        private static int _numberOfAddCalls;
        public event Action<string, int> AddOccured;

        public StringCalculator()
        {
            _numberOfAddCalls = 0;
        }

        public int Add(string input)
        {
            _numberOfAddCalls++;

            if (String.IsNullOrEmpty(input))
                return 0;

            var delimeter = ",";
            var differentDelimeterSign = "//";
            if (!input.Contains(delimeter) && !input.StartsWith(differentDelimeterSign))
                return int.Parse(input);

            if (input.StartsWith(differentDelimeterSign))
            {
                var newLine = "\n";
                delimeter = input.Substring(differentDelimeterSign.Length, input.IndexOf(newLine) - differentDelimeterSign.Length);
                input = input.Remove(0, differentDelimeterSign.Length + newLine.Length + delimeter.Length);
            }

            var numbers = input.Split(delimeter);
            var integers = numbers.Select(x => int.Parse(x));
            var negatives = integers.Where(x => x < 0);
            if (negatives.Any())
            {
                var negativesString = string.Join(",", negatives.ToList());
                throw new ArgumentException($"negatives not allowed: {negativesString}");
            }

            var sum = integers.Where(x => x < 1000).Sum();

            if (AddOccured != null)
                AddOccured(input, sum);
            
            return sum;
        }

        public int GetCalledCount()
        {
            return _numberOfAddCalls;
        }
    }
}
