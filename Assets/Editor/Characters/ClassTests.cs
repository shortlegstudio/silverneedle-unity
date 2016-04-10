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
public class ClassTests {
	YamlStream yaml;
	Class Fighter;
	Class Monk;
	Class Wizard;

	[SetUp]
	public void SetUp() {
		var input = new StringReader(ClassYamlFile);

		yaml = new YamlStream();
		yaml.Load(input);

		var yamlNode = new YamlNodeWrapper(yaml.Documents [0].RootNode);
		var classes = Class.LoadFromYaml (yamlNode);

		Fighter = classes.First (x => x.Name == "Fighter");
		Monk = classes.First (x => x.Name == "Monk");
		Wizard = classes.First (x => x.Name == "Wizard");
	}
		
    [Test]
    public void LoadClassYamlFile() {
		Assert.IsNotNull (Fighter);
		Assert.IsNotNull (Monk);
		Assert.IsNotNull (Wizard);
	}


	[Test]
	public void YamlLoadedClassesShouldHaveClassSkills() {
		//Validate that the class skills are tracked
		Assert.IsTrue (Fighter.IsClassSkill ("Climb"));
		Assert.IsTrue (Fighter.IsClassSkill ("Swim"));
		Assert.IsFalse (Fighter.IsClassSkill ("Spellcraft"));
    }

	[Test]
	public void YamlLoadedClassesShouldHaveSkillPoints() {
		Assert.AreEqual (2, Fighter.SkillPoints);
		Assert.AreEqual (4, Monk.SkillPoints);
	}

	private const string ClassYamlFile = @"--- 
- class: 
  name: Fighter
  skillpoints: 2
  skills: 
    - Climb
    - Swim
- class: 
  name: Monk
  skillpoints: 4
  skills:
    - Acrobatics
    - Climb
- class: 
  name: Wizard
  skillpoints: 4
  skills:
    - Knowledge Arcana
    - Spellcraft
...";
}
