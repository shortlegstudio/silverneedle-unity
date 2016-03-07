using UnityEngine;
using UnityEditor;
using System.Linq;
using System.IO;
using NUnit.Framework;
using ShortLegStudio;
using ShortLegStudio.RPG.Characters;
using YamlDotNet.RepresentationModel;
using System.Text;

[TestFixture]
public class RaceTests {
	YamlStream yaml;

	[SetUp]
	public void SetUp() {
		var input = new StringReader(RaceYamlFile);

		yaml = new YamlStream();
		yaml.Load(input);
	}
		
    [Test]
    public void LoadRaceYamlFile() {
		var yamlNode = new YamlNodeWrapper(yaml.Documents [0].RootNode);
		var races = Race.LoadFromYaml (yamlNode);

		Assert.AreEqual (3, races.Count);
		Assert.IsTrue(races.Any (x => x.Name == "Dwarf"));
		Assert.IsTrue(races.Any (x => x.Name == "Elf"));
		Assert.IsTrue(races.Any (x => x.Name == "Halfling"));

    }

	private const string RaceYamlFile = @"--- 
- race: 
  name: Dwarf
  abilities: 
    constitution: 2
    wisdom: 2
    charisma: -2
- race: 
  name: Elf
  abilities:
    constitution: 2
    wisdom: 2
    charisma: -2
- race: 
  name: Halfling
  abilities:
    constitution: 2
    wisdom: 2
    charisma: -2
...";
}
