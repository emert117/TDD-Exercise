using System;
using System.Collections.Generic;
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

            var delimiter = ",";
            var differentDelimeterSign = "//";
            if (!input.Contains(delimiter) && !input.StartsWith(differentDelimeterSign))
                return int.Parse(input);

            var newLine = "\n";
            var longDelimeterSign = "//[";
            List<string> numbers = new List<string>();

            if (input.StartsWith(longDelimeterSign))
            {
                var multipleDelimeterSign = "][";
                if (input.Contains(multipleDelimeterSign))
                {
                    var firstDelimeter = input.Substring(longDelimeterSign.Length, input.IndexOf(multipleDelimeterSign) - longDelimeterSign.Length);
                    var secondDelimiter = input.Substring(input.IndexOf(multipleDelimeterSign) + multipleDelimeterSign.Length, input.IndexOf(newLine) - input.IndexOf(multipleDelimeterSign) - multipleDelimeterSign.Length - 1);
                    input = input.Remove(0, input.IndexOf(newLine) + newLine.Length);
                    var numbersWithFirstDelimiter = input.Split(firstDelimeter).Where(x => int.TryParse(x, out _));
                    numbers.AddRange(numbersWithFirstDelimiter);
                    input = input.Remove(0, input.LastIndexOf(firstDelimeter) + firstDelimeter.Length);
                    var numbersWithSecondDelimiter = input.Split(secondDelimiter).Where(x => int.TryParse(x, out _));
                    numbers.AddRange(numbersWithSecondDelimiter);
                }
                else
                {
                    delimiter = input.Substring(longDelimeterSign.Length, input.IndexOf(newLine) - longDelimeterSign.Length - newLine.Length);
                    input = input.Remove(0, longDelimeterSign.Length + newLine.Length + delimiter.Length + 1);
                }
            }
            else if (input.StartsWith(differentDelimeterSign))
            {
                delimiter = input.Substring(differentDelimeterSign.Length, input.IndexOf(newLine) - differentDelimeterSign.Length);
                input = input.Remove(0, differentDelimeterSign.Length + newLine.Length + delimiter.Length);
            }

            if (!numbers.Any())
            {
                numbers = input.Split(delimiter).ToList();
            }

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
