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

	[SetUp]
	public void SetUp() {
		var input = new StringReader(SkillsYamlFile);

		yaml = new YamlStream();
		yaml.Load(input);
	}
		
    [Test]
    public void LoadSkillsYamlFile() {
		var yamlNode = new YamlNodeWrapper(yaml.Documents [0].RootNode);
		var skills = Skill.LoadFromYaml (yamlNode);

		Assert.AreEqual (3, skills.Count);
		Assert.IsTrue(skills.Any (x => x.Name == "Acrobatics"));
		Assert.IsTrue(skills.Any (x => x.Name == "Bluff"));
		Assert.IsTrue(skills.Any (x => x.Name == "Disable Device"));


		//validate each Skill
		var acro = skills.First(x => x.Name == "Acrobatics");
		Assert.AreEqual (AbilityScoreTypes.Dexterity, acro.Ability);
		Assert.IsFalse (acro.Trained);

		var device = skills.First(x => x.Name == "Disable Device");
		Assert.AreEqual (AbilityScoreTypes.Dexterity, device.Ability);
		Assert.IsTrue (device.Trained);



    }

	private const string SkillsYamlFile = @"--- 
- skill: 
  name: Acrobatics
  ability: dexterity
  trained: no
- skill: 
  name: Bluff
  ability: charisma
  trained: no
- skill: 
  name: Disable Device
  ability: dexterity
  trained: yes
...";
}
