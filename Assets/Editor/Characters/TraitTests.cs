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
public class TraitTests {
	YamlStream yaml;
	Trait darkvision;
	Trait hardy;
	Trait halflingLuck;


	[SetUp]
	public void SetUp() {
		var input = new StringReader(TraitYamlFile);

		yaml = new YamlStream();
		yaml.Load(input);
		var yamlNode = new YamlNodeWrapper(yaml.Documents [0].RootNode);
		var traits = Trait.LoadFromYaml (yamlNode);
		darkvision = traits.First (x => x.Name == "Darkvision");
		hardy = traits.First (x => x.Name == "Hardy");
		halflingLuck = traits.First (x => x.Name == "Halfling Luck");
	}
		
    [Test]
    public void LoadTraitYamlFile() {
		Assert.IsNotNull (darkvision);
		Assert.IsNotNull (hardy);
		Assert.IsNotNull (halflingLuck);
    }

	[Test]
	public void TraitsHaveADescription() {
		Assert.AreEqual ("See in the dark.", darkvision.Description);
		Assert.AreEqual ("Really tough", hardy.Description);
	}

	[Test]
	public void TraitsCanHaveSkillAdjustments() {
		var modifiers = hardy.SkillModifiers;
		Assert.AreEqual (2, modifiers.Count);
		var skillAdj = modifiers.First ();
		Assert.AreEqual ("Heal", skillAdj.SkillName);
		Assert.AreEqual ("Hardy (trait)", skillAdj.Reason);
		Assert.AreEqual (2, skillAdj.Amount);

		var flyAdj = modifiers.Last ();
		Assert.AreEqual ("Fly", flyAdj.SkillName);
		Assert.AreEqual (4, flyAdj.Amount);
	}

	private const string TraitYamlFile = @"--- 
- trait: 
  name: Darkvision
  description: See in the dark.
- trait:
  name: Hardy
  description: Really tough
  skillmodifiers:
    - skill: Heal
      amount: 2 
    - skill: Fly
      amount: 4
- trait:
  name: Halfling Luck
  description: Savings throw bonus
...";
}
