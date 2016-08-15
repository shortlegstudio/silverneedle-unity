using NUnit.Framework;
using System.Linq;
using ShortLegStudio.RPG;
using ShortLegStudio;
using ShortLegStudio.RPG.Names.Gateways;

namespace RPG.Names.Gateways {
	[TestFixture]
	public class CharacterNamesYamlGatewayTests
    {
        [Test]
        public void CanLoadABunchOfNames()
        {
            var gateway = new CharacterNamesYamlGateway(CharacterNamesYamlFile.ParseYaml());
            var names = gateway.GetFirstNames();
            Assert.Greater(names.Count(), 0);
            Assert.IsTrue(names.Contains("Steve"));
            Assert.IsTrue(names.Contains("Neo"));
        }

        [Test]
        public void PrunesOutAnyEmptyStrings() 
        {
            var gateway = new CharacterNamesYamlGateway(CharacterNamesYamlFile.ParseYaml());
            var names = gateway.GetFirstNames();
            Assert.IsFalse(names.Any(x => string.IsNullOrEmpty(x)));
        }

        [Test]
        public void CanLoadSomeLastNames()
        {
            var gateway = new CharacterNamesYamlGateway(CharacterNamesYamlFile.ParseYaml());
            var names = gateway.GetLastNames();
            Assert.Greater(names.Count(), 0);
            Assert.IsTrue(names.Contains("Hookum"));
            Assert.IsTrue(names.Contains("Fondu"));
        }

        const string CharacterNamesYamlFile = @"
- gender: male
  race: human
  category: first
  names: | 
    Steve, John, Charles
    Neo,,,Foobar
- gender: any
  race: human
  category: last
  names: |
    Smith, Johnson, Fondu
    ,,Hookum,Stookum,
";
	}
}