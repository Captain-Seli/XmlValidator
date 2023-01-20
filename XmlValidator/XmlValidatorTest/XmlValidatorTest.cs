
using Newtonsoft.Json.Linq;
using System.ComponentModel;

namespace XmlValidator
{
    [TestFixture]
    public class XmlValidatorTests
    {
        private XmlValidator _validator { get; set; } = null!;
        

        [SetUp]
        public void Setup()
        {
             _validator = new XmlValidator();
        }

        [Test]
        public void Tokenize_Equal_Test()
        {
            //ARRANGE
            var xml = "<book><title>Jaws</title><author>Peter Benchley</author></book>";
            var expectedTokens = new List<string> { "<book>", "<title>", "</title>", "<author>", "</author>", "</book>" };

            //ACT
            var actualTokens = _validator.Tokenize(xml);

            //ASSERT
            Assert.That(expectedTokens, Is.EqualTo(actualTokens));
        }

        [Test]
        public void Tokenize_MissingOpenBracket_Test()
        {
            //ARRANGE
            var xml = "book><title>Jaws</title><author>Peter Benchley</author></book>";
            
            //ACT
            var actualTokens = _validator.Tokenize(xml);

            //ASSERT
            Assert.That(actualTokens, Is.EqualTo(null));
        }

        [Test]
        public void Tokenize_MissingClosedBracket_Test()
        {
            //ARRANGE
            var xml = "<book><title>The Cat in the Hat</title><author>Dr. Seuss</author></book";

            //ACT
            var actualTokens = _validator.Tokenize(xml);

            //ASSERT
            Assert.That(actualTokens, Is.EqualTo(null));
        }

        [Test]
        public void Tokenize_EmptyTag_Test()
        {
            //ARRANGE
            var xml = "<></>";

            //ACT
            var actualTokens = _validator.Tokenize(xml);

            //ASSERT
            Assert.That(actualTokens, Is.EqualTo(null));
        }

        [Test]
        public void Tokenize_MissingClosedTag_Test()
        {
            //ARRANGE
            var xml = "<book><title>The Cat in the Hat</title><author>Dr. Seuss</author></book";

            //ACT
            var actualTokens = _validator.Tokenize(xml);

            //ASSERT
            Assert.That(actualTokens, Is.EqualTo(null));
        }

        [Test]
        public void Tokenize_NonXml_Test()
        {
            //ARRANGE
            var xml = "<wdiud<>dfi<< iurir>> !!@4";

            //ACT
            var actualTokens = _validator.Tokenize(xml);

            //ASSERT
            Assert.That(actualTokens, Is.EqualTo(null));
        }

        [Test]
        public void Tokenize_DoubleClosedBracket_Test()
        {
            //ARRANGE
            var xml = "<book><title>The Cat in the Hat</title><author>Dr. Seuss</author></book>>";

            //ACT
            var actualTokens = _validator.Tokenize(xml);

            //ASSERT
            Assert.That(actualTokens, Is.EqualTo(null));
        }

        [Test]
        public void DetermineNestOrder_Basic_Test()
        {
            //ARRANGE
            var tokens = new List<string> { "<book>", "<title>", "</title>", "<author>", "</author>", "</book>" };

            //ACT
            var actualValidity = _validator.DetermineNestOrder(tokens);

            //ASSERT
            Assert.IsTrue(actualValidity);
        }

        [Test]
        public void DetermineNestOrder_SelfClosingTag_Test()
        {
            //ARRANGE
            var tokens = new List<string> { "<book>", "<title>", "</title>", "<author>", "</author>", "<img/>", "</book>" };

            //ACT
            var actualValidity = _validator.DetermineNestOrder(tokens);

            //ASSERT
            Assert.IsTrue(actualValidity);
        }

        [Test]
        public void DeterminNestOrder_DoubleOpenBracket_Test()
        {
            //ARRANGE
            var tokens = new List<string> { "<<book>", "<title>", "</title>", "<author>", "</author>", "<img/>", "</book>" };

            //ACT
            var actualValidity = _validator.DetermineNestOrder(tokens);

            //ASSERT
            Assert.IsFalse(actualValidity);
        }

        [Test]
        public void DetermineNestOrder_MissingOpenTag_Test()
        {
            //ARRANGE
            var tokens = new List<string> { "<title>", "</title>", "<author>", "</author>", "<img/>", "</book>" };

            //ACT
            var actualValidity = _validator.DetermineNestOrder(tokens);

            //ASSERT
            Assert.IsFalse(actualValidity);
        }

        [Test]
        public void DetermineNestOrder_MissingClosedTag_Test()
        {
            //ARRANGE
            var tokens = new List<string> { "<title>", "</title>", "<author>", "</author>", "<img/>" };

            //ACT
            var actualValidity = _validator.DetermineNestOrder(tokens);

            //ASSERT
            Assert.IsFalse(actualValidity);
        }

        [Test]
        public void DetermineNestOrder_OutofOrder_Test()
        {
            //ARRANGE
            var tokens = new List<string> { "<People>", "<Design>", "<Code>", "</People>", "</Code>", "</Design>"};

            //ACT
            var actualValidity = _validator.DetermineNestOrder(tokens);

            //ASSERT
            Assert.IsFalse(actualValidity);
        }

        [Test]
        public void DetermineNestOrder_OddNumberTags_Test()
        {
            //ARRANGE
            var tokens = new List<string> { "<Design>", "<Code>", "</Code>", "</Design>", "<People>" };

            //ACT
            var actualValidity = _validator.DetermineNestOrder(tokens);

            //ASSERT
            Assert.IsFalse(actualValidity);
        }

        [Test]
        public void DetermineNestOrder_ImproperOpenTag_Test()
        {
            //ARRANGE
            var tokens = new List<string> { "<People age='1'", "</People>" };

            //ACT
            var actualValidity = _validator.DetermineNestOrder(tokens);

            //ASSERT
            Assert.IsFalse(actualValidity);
        }

        [Test]
        public void SelfClosingCheckTest()
        {
            //ARRANGE
            List<string> tokens = new List<string> { "<img/>" };

            //ACT
            var isSelfClosing = _validator.DetermineNestOrder(tokens);

            //ASSERT
            Assert.True(isSelfClosing);
        }
    }
}
