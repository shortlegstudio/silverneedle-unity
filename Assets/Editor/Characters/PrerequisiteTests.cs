using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using ShortLegStudio.RPG.Characters;
using YamlDotNet.RepresentationModel;
using System.IO;
using ShortLegStudio;
using System.Linq;

[TestFixture]
public class PrerequisiteTests {
	[Test]
	public void ParseSomeYaml() {
		var input = new StringReader(PrerequisitesYaml);

		var yaml = new YamlStream();
		yaml.Load(input);
		var yamlNode = new YamlNodeWrapper(yaml.Documents [0].RootNode);

		var prereq = yamlNode.GetNode ("prerequisites");

		var prereqs = new Prerequisites (prereq);

		Assert.AreEqual (3, prereqs.Count);
		Assert.IsInstanceOf<AbilityPrerequisite> (prereqs.First ());
	}

	[Test]
	public void AlwaysQualifiedIfNoQualificationsNeeded() {
		var pre = new Prerequisites ();
		Assert.IsTrue(pre.Qualified(new CharacterSheet()));
	}

	[Test]
	public void AbilityIsQualifiedIfExceedingScore() {
		var pre = new AbilityPrerequisite ("Intelligence 13");
		var c = new CharacterSheet ();
		c.SetAbility (AbilityScoreTypes.Intelligence, 15);
		Assert.IsTrue (pre.Qualified (c));
	}

	[Test]
	public void AbilityIsNotQualifiedIfNotExceedingScore() {
		var pre = new AbilityPrerequisite ("Intelligence 13");
		var c = new CharacterSheet ();
		c.SetAbility (AbilityScoreTypes.Intelligence, 11);
		Assert.IsFalse (pre.Qualified (c));
	}



	private const string PrerequisitesYaml = @"--- 
prerequisites:
  - ability: Intelligence 13
  - race: Elf
  - feat: Weapon Finesse
";
}

