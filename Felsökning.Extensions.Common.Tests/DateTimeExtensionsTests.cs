using System.Text.RegularExpressions;

namespace Felsökning.Extensions.Common.Tests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class DateTimeExtensionsTests
    {
        [TestMethod]
        public void IsWeekDay_ShouldReturn_True_Monday()
        {
            using (var dateTimeProvider = new TestingDateTimeProvider(new DateTime(1954, 11, 08)))
            {
                var sut = dateTimeProvider.Now;

                var result = sut.IsWeekDay();

                result.Should().BeTrue();
            }
        }

        [TestMethod]
        public void IsWeekDay_ShouldReturn_True_Tuesday()
        {
            using (var dateTimeProvider = new TestingDateTimeProvider(new DateTime(2001, 09, 11)))
            {
                var sut = dateTimeProvider.Now;

                var result = sut.IsWeekDay();

                result.Should().BeTrue();
            }
        }

        [TestMethod]
        public void IsWeekDay_ShouldReturn_True_Wednesday()
        {
            using (var dateTimeProvider = new TestingDateTimeProvider(new DateTime(1358, 12, 13)))
            {
                var sut = dateTimeProvider.Now;

                var result = sut.IsWeekDay();

                result.Should().BeTrue();
            }
        }

        [TestMethod]
        public void IsWeekDay_ShouldReturn_True_Thursday()
        {
            using (var dateTimeProvider = new TestingDateTimeProvider(new DateTime(1776, 07, 04)))
            {
                var sut = dateTimeProvider.Now;

                var result = sut.IsWeekDay();

                result.Should().BeTrue();
            }
        }

        [TestMethod]
        public void IsWeekDay_ShouldReturn_False_Saturday()
        {
            using (var dateTimeProvider = new TestingDateTimeProvider(new DateTime(1523, 06, 16)))
            {
                var sut = dateTimeProvider.Now;

                var result = sut.IsWeekDay();

                result.Should().BeFalse();
            }
        }

        [TestMethod]
        public void ToCulturedString_ShouldReturn_ExpectedValue()
        {
            using (var dateTimeProvider = new TestingDateTimeProvider(new DateTime(2023, 01, 01)))
            {
                var sut = dateTimeProvider.Now;

                var result = sut.ToCulturedString("sv-se");

                result.Should().NotBeNullOrWhiteSpace();
                result.Should().Be("2023-01-01 00:00:00");
            }
        }

        [TestMethod]
        public void ToIso8601UtcString_ShouldReturn_ExpectedValue()
        {
            using (var dateTimeProvider = new TestingDateTimeProvider(new DateTime(1986, 01, 28, 16, 39, 13)))
            {
                var sut = dateTimeProvider.Now;

                var result = sut.ToIso8601UtcString();

                result.Should().NotBeNullOrWhiteSpace();
                result.Should().Be("1986-01-28T16:39:13.0000000Z");
            }
        }

        [TestMethod]
        public void ToPosixTime_ShouldReturn_ExpectedValue()
        {
            using (var dateTimeProvider = new TestingDateTimeProvider(new DateTime(1975, 01, 28, 16, 39, 13)))
            {
                var sut = dateTimeProvider.Now;

                var result = sut.ToPosixTime();

                result.Should().BeGreaterThan(0);
                result.Should().Be(160159153);
            }
        }

        [TestMethod]
        public void ToRfc1123String_ShouldReturn_ExpectedValue()
        {
            using (var dateTimeProvider = new TestingDateTimeProvider(new DateTime(2022, 08, 29, 10, 01, 13)))
            {
                var sut = dateTimeProvider.Now;

                var result = sut.ToRfc1123String();

                result.Should().NotBeNullOrWhiteSpace();
                result.Should().Be("Mon, 29 Aug 2022 10:01:13 GMT");
            }
        }

        [TestMethod]
        public void ToSwedishString_ShouldReturn_ExpectedValue()
        {
            using (var dateTimeProvider = new TestingDateTimeProvider(new DateTime(1995, 12, 24, 23, 59, 59)))
            {
                var sut = dateTimeProvider.Now;

                var result = sut.ToSwedishString();

                result.Should().NotBeNullOrWhiteSpace();
                result.Should().Be("1995-12-24 23:59:59");
            }
        }

        [TestMethod]
        public void ToUnixEpochTime_ShouldReturn_ExpectedValue()
        {
            using (var dateTimeProvider = new TestingDateTimeProvider(new DateTime(2026, 02, 17, 10, 00, 00)))
            {
                var sut = dateTimeProvider.Now;

                var result = sut.ToUnixEpochTime();

                result.Should().BeGreaterThan(0);
                result.Should().Be(1771322400);
            }
        }

        [TestMethod]
        public void ToWeekNumber_ShouldReturn_ExpectedValue()
        {
            using (var dateTimeProvider = new TestingDateTimeProvider(new DateTime(2005, 06, 06)))
            {
                var sut = dateTimeProvider.Now;

                var result = sut.ToWeekNumber();

                result.Should().BeGreaterThan(0);
                result.Should().Be(23);
            }
        }

        [TestMethod]
        public void ValidateVeckan1990()
        {
            DateTime dateTime = new DateTime(1990, 10, 10);
            int weekNumber = dateTime.ToWeekNumber();
            Assert.IsNotNull(weekNumber);
            Assert.IsFalse(weekNumber == 0);
            Assert.AreEqual(weekNumber, 41);
        }

        [TestMethod]
        public void ValidateSwedishStringIsLocalised()
        {
            DateTime dateTime = new DateTime(2019, 01, 01);
            string dateTimeString = dateTime.ToSwedishString();
            Assert.IsTrue(Regex.IsMatch(dateTimeString, @"^[0-9][0-9][0-9][0-9]-[0-1][0-9]-[0-3][0-9] [0-2][0-9]:[0-6][0-9]:[0-5][0-9]$"));
        }

        /// <summary>
        ///     Validates that the given datetime string is valid per the culture standard.
        /// </summary>
        [TestMethod]
        public void ValidateFirstCulturedDateString()
        {
            DateTime now = new DateTime(1990, 12, 25, 14, 30, 55);
            string culturedString = now.ToCulturedString("cs-CZ");
            Assert.IsTrue(Regex.IsMatch(culturedString, @"^[0-9][0-9].[0-1][0-9].[1-2][0-9][0-9][0-9] [0-2][0-9]:[0-6][0-9]:[0-5][0-9]$"));
        }

        /// <summary>
        ///     Validates that the given datetime string is valid per the culture standard.
        /// </summary>
        [TestMethod]
        public void ValidateThirdCulturedDateString()
        {
            DateTime now = new DateTime(1990, 12, 25, 14, 30, 55);
            string culturedString = now.ToCulturedString("pt-BR");
            Assert.IsTrue(Regex.IsMatch(culturedString, @"^[0-9][0-9]/[0-1][0-9]/[1-2][0-9][0-9][0-9] [0-2][0-9]:[0-6][0-9]:[0-5][0-9]$"));
        }
    }
}
