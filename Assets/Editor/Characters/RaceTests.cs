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
	Race dwarf;
	Race elf;
	Race halfling;
	Race human;

	[SetUp]
	public void SetUp() {
		var input = new StringReader(SkillsYamlFile);

		yaml = new YamlStream();
		yaml.Load(input);
		var yamlNode = new YamlNodeWrapper(yaml.Documents [0].RootNode);
		var races = Race.LoadFromYaml (yamlNode);
		dwarf = races.First (x => x.Name == "Dwarf");
		elf = races.First (x => x.Name == "Elf");
		halfling = races.First (x => x.Name == "Halfling");
		human = races.First (x => x.Name == "Human");
	}
		
    [Test]
    public void LoadRaceYamlFile() {
		Assert.IsNotNull (dwarf);
		Assert.IsNotNull (elf);
		Assert.IsNotNull (halfling);
		Assert.IsNotNull (human);
    }

	[Test]
	public void HumansCanChooseAbilityModifier() {
		var mod = human.AbilityModifiers.First ();
		Assert.IsTrue (mod.RacialChose);
		Assert.AreEqual (2, mod.value);
	}

	[Test]
	public void DwarvesHaveSpecificAbilitiesToModifier() {
		var cons = dwarf.AbilityModifiers.First (x => x.ability == AbilityScoreTypes.Constitution);
		Assert.AreEqual (2, cons.value);

		var wis = dwarf.AbilityModifiers.First (x => x.ability == AbilityScoreTypes.Wisdom);
		Assert.AreEqual (2, wis.value);

		var cha = dwarf.AbilityModifiers.First (x => x.ability == AbilityScoreTypes.Charisma);
		Assert.AreEqual (-2, cha.value);
	}

	private const string SkillsYamlFile = @"--- 
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
  name: Human
  abilities:
    choose: 2
- race: 
  name: Halfling
  abilities:
    constitution: 2
    wisdom: 2
    charisma: -2
...";
}
