//-----------------------------------------------------------------------
// <copyright file="StringExtensionsTests.cs" company="Felsökning">
//     Copyright (c) Felsökning. All rights reserved.
// </copyright>
// <author>John Bailey</author>
//-----------------------------------------------------------------------
namespace Felsökning.Extensions.Common.Tests
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class StringExtensionsTests
    {
        [DataTestMethod]
        [DataRow("")]
        [DataRow(" ")]
        [DataRow(null)]
        public void GetPostnummerDetails_ShouldFailForNullOrEmpty(string sut)
        {
            var exception = Assert.ThrowsException<ArgumentNullException>(() => sut.GetPostnummerDetails());

            exception.Should().BeOfType<ArgumentNullException>();
            exception.Message.Should().Be("Value cannot be null.");
        }

        [TestMethod]
        public void GetPostnummerDetails_ShouldFailForTooShort()
        {
            var sut = "1234";

            var exception = Assert.ThrowsException<ArgumentException>(() => sut.GetPostnummerDetails());

            exception.Should().BeOfType<ArgumentException>();
            exception.Message.Should().Be("The parameter supplied was too short to be a valid Swedish postnummer.");
        }

        [TestMethod]
        public void GetPostnummerDetails_ShouldFailForTooLong()
        {
            var sut = "1234567890";

            var exception = Assert.ThrowsException<ArgumentException>(() => sut.GetPostnummerDetails());

            exception.Should().BeOfType<ArgumentException>();
            exception.Message.Should().Be("The parameter supplied was too long to be a valid Swedish postnummer.");
        }

        [TestMethod]
        public void GetPostnummerDetails_ShouldFailForNonNumberCharacters()
        {
            var sut = "1b345";

            var exception = Assert.ThrowsException<ArgumentException>(() => sut.GetPostnummerDetails());

            exception.Should().BeOfType<ArgumentException>();
            exception.Message.Should().Be("The parameter supplied had non-numeric characters, which postnummers do not.");
        }

        [TestMethod]
        public void GetPostnummerDetails_ShouldSucceedForLinköping()
        {
            var sut = "582 22";

            var result = sut.GetPostnummerDetails();

            result.Should().NotBeNullOrWhiteSpace();
            result.Should().Be("Linköping, Brevbäring, Boxpost");
        }

        [TestMethod]
        public void GetPostnummerDetails_ShouldSucceedForCentralStationStockholm()
        {
            var sut = "111 20";

            var result = sut.GetPostnummerDetails();

            result.Should().NotBeNullOrWhiteSpace();
            result.Should().Be("Stockholm, Brevbäring, Svarspost");
        }

        [DataTestMethod]
        [DataRow("")]
        [DataRow(" ")]
        [DataRow(null)]
        public void IsValidPersonNummer_ShouldFailForNullOrEmpty(string sut)
        {
            var exception = Assert.ThrowsException<ArgumentNullException>(() => sut.IsValidPersonNummer());

            exception.Should().BeOfType<ArgumentNullException>();
            exception.Message.Should().Be("Value cannot be null.");
        }

        [TestMethod]
        public void IsValidPersonNummer_ShouldFailForIncorrectSize()
        {
            var sut = "This should fail";

            var exception = Assert.ThrowsException<ArgumentException>(() => sut.IsValidPersonNummer());

            exception.Should().BeOfType<ArgumentException>();
            exception.Message.Should().NotBeNullOrWhiteSpace();
            exception.Message.Should().Be("String is incorrect size: 16");
        }

        [TestMethod]
        public void IsValidPersonNummer_ShouldThrow_ForNoNumbers()
        {
            var sut = "aaaaaaaaaa";

            var exception = Assert.ThrowsException<ArgumentException>(() => sut.IsValidPersonNummer());

            exception.Should().BeOfType<ArgumentException>();
            exception.Message.Should().NotBeNullOrWhiteSpace();
            exception.Message.Should().Be("Unable to parse 'aaaaaaaaaa' to long.");
        }

        [TestMethod]
        public void IsValidPersonNummer_ShouldFail_ForTenDigits()
        {
            var sut = "867530-9999";

            var result = sut.IsValidPersonNummer();

            result.Should().BeFalse();
        }

        [TestMethod]
        public void IsValidPersonNummer_ShouldSucceed_ForTenDigits()
        {
            var sut = "811228-9874";

            var result = sut.IsValidPersonNummer();

            result.Should().BeTrue();
        }

        [TestMethod]
        public void IsValidPersonNummer_ShouldFail_ForTwelveDigits()
        {
            var sut = "19867530-9999";

            var result = sut.IsValidPersonNummer();

            result.Should().BeFalse();
        }

        [TestMethod]
        public void IsValidPersonNummer_ShouldSucceed_ForTwelveDigits()
        {
            var sut = "19811228-9874";

            var result = sut.IsValidPersonNummer();

            result.Should().BeTrue();
        }

        [DataTestMethod]
        [DataRow(new string[] { })]
        [DataRow(null)]
        public void ToArrayString_ShouldFailForNullOrEmpty(string[] sut)
        {
            var exception = Assert.ThrowsException<ArgumentNullException>(() => sut.ToArrayString());

            exception.Should().BeOfType<ArgumentNullException>();
            exception.Message.Should().Be("Value cannot be null.");
        }

        [TestMethod]
        public void ToArrayString_ShouldSucceed()
        {
            var sut = new string[] { "This", "is", "a", "test." };

            var result = sut.ToArrayString();

            result.Should().NotBeNullOrWhiteSpace();
            result.Should().Be("This, is, a, test.");
        }

        /// <summary>
        ///     Validates that the given string is a valid personnummer.
        /// </summary>
        [TestMethod]
        public void ValidateSwedishPersonNummerCheck()
        {
            string personnummer = "811228-9874";
            Assert.IsTrue(personnummer.IsValidPersonNummer());
            personnummer = "190609-1606";
            Assert.IsTrue(personnummer.IsValidPersonNummer());
            personnummer = "811228+9874";
            Assert.IsTrue(personnummer.IsValidPersonNummer());
            personnummer = "19811228-9874";
            Assert.IsTrue(personnummer.IsValidPersonNummer());
            personnummer = "670919-9530";
            Assert.IsTrue(personnummer.IsValidPersonNummer());
            personnummer = "000101-0001";
            Assert.IsFalse(personnummer.IsValidPersonNummer());
            personnummer = "19670919-9530";
            Assert.IsTrue(personnummer.IsValidPersonNummer());
            personnummer = "191212121212";
            Assert.IsTrue(personnummer.IsValidPersonNummer());
            personnummer = "1212121212";
            Assert.IsTrue(personnummer.IsValidPersonNummer());
            personnummer = "5909204017";
            Assert.IsTrue(personnummer.IsValidPersonNummer());
            personnummer = "191027-0543";
            Assert.IsTrue(personnummer.IsValidPersonNummer());
            personnummer = "200318-0417";
            Assert.IsTrue(personnummer.IsValidPersonNummer());
            personnummer = "451103-2668";
            Assert.IsTrue(personnummer.IsValidPersonNummer());
            personnummer = "440715-2752";
            Assert.IsTrue(personnummer.IsValidPersonNummer());
            personnummer = "300112-2443";
            Assert.IsTrue(personnummer.IsValidPersonNummer());
            personnummer = "19230721-6537";
            Assert.IsTrue(personnummer.IsValidPersonNummer());
            personnummer = "19030721+6531";
            Assert.IsTrue(personnummer.IsValidPersonNummer());
            personnummer = "20180721-6534";
            Assert.IsTrue(personnummer.IsValidPersonNummer());
        }

        [TestMethod]
        public async Task StringToJsonHttpContent()
        {
            string testString = "testContent";
            HttpContent testContent = testString.ToJsonHttpContent();
            Assert.IsTrue(testContent?.Headers?.ContentType?.MediaType?.Equals("application/json"));
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var content = await testContent?.ReadAsStringAsync();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            Assert.IsTrue(content.Equals(testString));
        }


        [TestMethod]
        public async Task StringToHttpContent()
        {
            string testString = "testContent";
            HttpContent testContent = testString.ToHttpContent("application/xml");
            Assert.IsTrue(testContent?.Headers?.ContentType?.MediaType?.Equals("application/xml"));
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var content = await testContent?.ReadAsStringAsync();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            Assert.IsTrue(content.Equals(testString));
        }

        [TestMethod]
        public void ValidateGoodString()
        {
            string testString = "something";
            testString.Validate();
        }

        [TestMethod]
        public void ValidateBadString()
        {
            string testString = string.Empty;

            var exception = Assert.ThrowsException<ArgumentException>(() => testString.Validate());

            exception.Should().BeOfType<ArgumentException>();
            exception.Message.Should().Contain("The given string was either null, empty, or whitespace");
        }

        [TestMethod]
        public void ValidateGoodLength()
        {
            string testString = "something";
            testString.Validate(9);
        }

        [TestMethod]
        public void ValidateNullLength()
        {
            string testString = string.Empty;

            var exception = Assert.ThrowsException<ArgumentException>(() => testString.Validate(9));

            exception.Should().BeOfType<ArgumentException>();
            exception.Message.Should().Contain("The given string was either null, empty, or whitespace");
        }

        [TestMethod]
        public void ValidateBadLength()
        {
            string testString = "something";

            var exception = Assert.ThrowsException<ArgumentException>(() => testString.Validate(8));

            exception.Should().BeOfType<ArgumentException>();
            exception.Message.Should().Contain("The given string was not the expected length of 8");
        }
    }
}