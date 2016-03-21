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

	[SetUp]
	public void SetUp() {
		var input = new StringReader(ClassYamlFile);

		yaml = new YamlStream();
		yaml.Load(input);
	}
		
    [Test]
    public void LoadRaceYamlFile() {
		var yamlNode = new YamlNodeWrapper(yaml.Documents [0].RootNode);
		var classes = Class.LoadFromYaml (yamlNode);

		Assert.AreEqual (3, classes.Count);
		Assert.IsTrue(classes.Any (x => x.Name == "Fighter"));
		Assert.IsTrue(classes.Any (x => x.Name == "Monk"));
		Assert.IsTrue(classes.Any (x => x.Name == "Wizard"));

    }

	private const string ClassYamlFile = @"--- 
- class: 
  name: Fighter
- class: 
  name: Monk
- class: 
  name: Wizard
...";
}
