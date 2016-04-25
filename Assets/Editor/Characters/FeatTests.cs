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
public class FeatTests {
	YamlStream yaml;
	Feat Acrobatic;
	Feat CombatExpertise;
	Feat PowerAttack;


	[SetUp]
	public void SetUp() {
		var input = new StringReader(FeatYamlFile);

		yaml = new YamlStream();
		yaml.Load(input);
		var yamlNode = new YamlNodeWrapper(yaml.Documents [0].RootNode);
		var feats = Feat.LoadFromYaml (yamlNode);
		Acrobatic = feats.First (x => x.Name == "Acrobatic");
		CombatExpertise = feats.First (x => x.Name == "Combat Expertise");
		PowerAttack = feats.First (x => x.Name == "Power Attack");
	}
		
    [Test]
    public void LoadTraitYamlFile() {
		Assert.IsNotNull (Acrobatic);
		Assert.IsNotNull (CombatExpertise);
		Assert.IsNotNull (PowerAttack);
    }

	[Test]
	public void FeatsHaveADescription() {
		Assert.AreEqual ("Move good", Acrobatic.Description);
		Assert.AreEqual ("Hit Stuff Hard", PowerAttack.Description);
	}

	[Test]
	public void FeatsCanHaveSkillAdjustments() {
		var modifiers = Acrobatic.SkillModifiers;
		Assert.AreEqual (2, modifiers.Count);
		var skillAdj = modifiers.First ();
		Assert.AreEqual ("Acrobatics", skillAdj.SkillName);
		Assert.AreEqual ("Acrobatic (feat)", skillAdj.Reason);
		Assert.AreEqual (2, skillAdj.Amount);

		var flyAdj = modifiers.Last ();
		Assert.AreEqual ("Fly", flyAdj.SkillName);
		Assert.AreEqual ("Acrobatic (feat)", skillAdj.Reason);
		Assert.AreEqual (4, flyAdj.Amount);
	}

	[Test]
	public void FeatsCanHaveAbilityPrerequisites() {
		var prereq = CombatExpertise.Prerequisites;
		var abilityCheck = prereq.First () as AbilityPrerequisite;
		Assert.IsInstanceOf<AbilityPrerequisite> (abilityCheck);
		Assert.AreEqual (AbilityScoreTypes.Intelligence, abilityCheck.Ability);
		Assert.AreEqual (13, abilityCheck.Minimum);
	}

	private const string FeatYamlFile = @"--- 
- feat: 
  name: Acrobatic
  description: Move good
  skillmodifiers:
    - skill: Acrobatics
      amount: 2
    - skill: Fly
      amount: 4
- feat:
  name: Combat Expertise
  description: Dodge stuff better
  prerequisites:
    - ability: Intelligence 13

- feat:
  name: Power Attack
  description: Hit Stuff Hard
...";
}
