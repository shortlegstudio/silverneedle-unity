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
public class SkillTests {
	YamlStream yaml;
	Skill Acrobatics;
	Skill Bluff;
	Skill DisableDevice;

	[SetUp]
	public void SetUp() {
		var input = new StringReader(SkillsYamlFile);

		yaml = new YamlStream();
		yaml.Load(input);

		var yamlNode = new YamlNodeWrapper(yaml.Documents [0].RootNode);
		var skills = Skill.LoadFromYaml (yamlNode);

		Acrobatics = skills.First (x => x.Name == "Acrobatics");
		Bluff = skills.First (x => x.Name == "Bluff");
		DisableDevice = skills.First (x => x.Name == "Disable Device");
	}
		
    [Test]
    public void LoadSkillsYamlFile() {
		Assert.IsNotNull (Acrobatics);
		Assert.IsNotNull (Bluff);
		Assert.IsNotNull (DisableDevice);
    }

	[Test]
	public void SkillsHaveAnAbilityToBaseScoresFrom() {
		Assert.AreEqual (AbilityScoreTypes.Dexterity, Acrobatics.Ability);
		Assert.AreEqual (AbilityScoreTypes.Charisma, Bluff.Ability);
		Assert.AreEqual (AbilityScoreTypes.Dexterity, DisableDevice.Ability);

	}

	[Test]
	public void SomeSkillsRequireTraining() {
		Assert.IsFalse (Acrobatics.TrainingRequired);
		Assert.IsTrue (DisableDevice.TrainingRequired);
	}

	[Test]
	public void SkillsCanHaveALongDescription() {
		Assert.AreEqual ("A really long description.\n", Acrobatics.Description);
	}

	private const string SkillsYamlFile = @"--- 
- skill: 
  name: Acrobatics
  ability: dexterity
  trained: no
  description: >
    A really long
    description.
- skill: 
  name: Bluff
  ability: charisma
  trained: no
  description: >
    A really long description.
- skill: 
  name: Disable Device
  ability: dexterity
  trained: yes
  description: >
    A really long description.
...";
}
