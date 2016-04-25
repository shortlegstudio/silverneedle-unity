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

	private const string PrerequisitesYaml = @"--- 
prerequisites:
  - ability: Intelligence 13
  - race: Elf
  - feat: Weapon Finesse
";
}

