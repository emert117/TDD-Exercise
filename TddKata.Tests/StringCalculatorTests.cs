using System;
using NUnit.Framework;
using TDDKata.Core;

namespace TddKata.Tests
{
    public class StringCalculatorTests
    {
        private StringCalculator _stringCalculator;

        [SetUp]
        public void Setup()
        {
            _stringCalculator = new StringCalculator();
        }

        [Test]
        public void Empty_input_should_return_zero()
        {
            var expected = 0;
            Assert.AreEqual(expected, _stringCalculator.Add(""));
        }

        [Test]
        public void One_number_should_return_itself()
        {
            var expected = 5;
            Assert.AreEqual(expected, _stringCalculator.Add("5"));
        }

        [Test]
        public void Two_numbers_should_return_the_sum_of_two_numbers()
        {
            var expected = 11;
            Assert.AreEqual(expected, _stringCalculator.Add("5,6"));
        }

        [Test]
        public void Unknown_amount_of_numbers_should_return_the_sum_of_all_numbers()
        {
            var expected = 17;
            Assert.AreEqual(expected, _stringCalculator.Add("5,6,2,4"));
        }

        [Test]
        public void Can_handle_new_lines_in_input()
        {
            var expected = 11;
            Assert.AreEqual(expected, _stringCalculator.Add("5\n,6"));
        }

        [Test]
        public void Can_support_different_delimeters_in_input()
        {
            var expected = 7;
            Assert.AreEqual(expected, _stringCalculator.Add("//;\n3;4"));
        }

        [Test]
        public void Throw_exception_if_get_any_negative_input()
        {
            Assert.Throws(typeof(ArgumentException), () => _stringCalculator.Add("5,-1"));
        }

        [Test]
        public void Throw_exception_if_get_any_negative_input_and_show_them_in_exception_message()
        {
            Assert.Throws(typeof(ArgumentException), () => _stringCalculator.Add("5,-1,2,-3"));
        }

        [Test]
        public void Should_return_number_of_add_method_calls()
        {
            _stringCalculator = new StringCalculator();
            var expected = 2;
            _stringCalculator.Add("1");
            _stringCalculator.Add("1,2");

            Assert.AreEqual(expected, _stringCalculator.GetCalledCount());
        }

        [Test]
        public void AddOccured_called_after_add_method()
        {
            string expected = null;
            _stringCalculator.AddOccured += delegate (string input, int result)
            {
                expected = input;
            };

            var actualInput = "2,3";
            _stringCalculator.Add(actualInput);
            Assert.AreEqual(expected, actualInput);
        }

        [Test]
        public void Numbers_bigger_than_1000_should_be_ignored()
        {
            var expected = 2;
            Assert.AreEqual(expected, _stringCalculator.Add("2,1002"));
        }

        [Test]
        public void Can_handle_delimeters_wtih_any_lenght()
        {
            var expected = 6;
            Assert.AreEqual(expected, _stringCalculator.Add("//[***]\n1***2***3"));
        }
    }
}