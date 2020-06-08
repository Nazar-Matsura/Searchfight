using NUnit.Framework;

namespace Searchfight.Application.Tests
{
    [TestFixture]
    public class MergeQuotedTests
    {
        private MergeQuotedService _mergeQuotedService;

        [SetUp]
        public void Setup()
        {
            _mergeQuotedService = new MergeQuotedService();
        }

        [Test]
        [TestCase("java")]
        [TestCase("java", ".net")]
        [TestCase("java", ".net", "c#")]
        [TestCase("java", "c", "c++", "c#", "goLang")]
        public void Valid_Arguments_Without_Quotes_Are_Not_Affected(params string[] args)
        {
            var result = _mergeQuotedService.Merge(args, "\"");

            Assert.That(result, Is.EqualTo(args));
        }

        [Test]
        [TestCase("\"java\"", ExpectedResult = new []{"java"})]
        [TestCase("\"java\"", "\".net\"", ExpectedResult = new[]{"java", ".net"})]
        [TestCase("java", "\".net\"", ExpectedResult = new[]{"java", ".net"})]
        [TestCase("\"java\"", ".net", ExpectedResult = new[]{"java", ".net"})]
        [TestCase("java", "\".net\"", "c#", ExpectedResult = new[]{"java", ".net", "c#"})]
        [TestCase("\"java\"", "c", "c++", "c#", "\"goLang\"",  ExpectedResult = new[]{"java", "c", "c++", "c#", "goLang"})]
        public string[] Valid_One_Word_Arguments_With_Quotes_Are_Stripped_Of_Quotes(params string[] args)
        {
            return _mergeQuotedService.Merge(args, "\"").ToArray();
        }

        [Test]
        [TestCase("\"java", "script\"",
            ExpectedResult = new []{"java script"})]
        [TestCase("\"java", "script\"", 
            "\"go", "lang\"", 
            ExpectedResult = new []{"java script", "go lang"})]
        [TestCase("\"java", "script\"", 
            ".net", 
            "\"go", "lang\"", 
            ExpectedResult = new[]{"java script", ".net", "go lang"})]
        public string[] Valid_Two_Word_Arguments_Are_Merged(params string[] args)
        {
            return _mergeQuotedService.Merge(args, "\"").ToArray();
        }

        [Test]
        [TestCase("\"java", "super", "script\"",
            ExpectedResult = new []{"java super script"})]
        [TestCase("\"java", "super", "script\"", 
            "\"go", "long", "lang\"", 
            ExpectedResult = new []{"java super script", "go long lang"})]
        [TestCase("\"java", "super", "script\"", 
            ".net", 
            "\"go", "long", "lang\"", 
            ExpectedResult = new[]{"java super script", ".net", "go long lang"})]
        [TestCase("\"java", "super", "script\"", 
            ".net", 
            "\"go", "very", "ultra", "long", "lang\"", 
            ExpectedResult = new[]{"java super script", ".net", "go very ultra long lang"})]
        public string[] Valid_Multiple_Word_Arguments_Are_Merged(params string[] args)
        {
            return _mergeQuotedService.Merge(args, "\"").ToArray();
        }

        [Test]
        [TestCase("ja\"va")]
        [TestCase("")]
        [TestCase("java", "")]
        [TestCase("java", "\"")]
        [TestCase("ja\"va")]
        [TestCase("\'java\"")]
        [TestCase("\"ja\"va\"")]
        [TestCase("ja\"va", ".n\'et")]
        [TestCase("\"java", ".net", "c#")]
        [TestCase("java", "c", "c++\"", "c#", "goLang")]
        [TestCase("java", "c", "c++\"", "c#", "goLang\"")]
        public void Invalid_Arguments_Do_Not_Throw(params string[] args)
        {
            Assert.That(() => _mergeQuotedService.Merge(args, "\""), Throws.Nothing);
        }
    }
}