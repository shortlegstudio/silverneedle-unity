using System;
using System.Linq;
using NUnit.Framework;
using ShortLegStudio;
using ShortLegStudio.RPG.Characters;

[TestFixture]
public class LanguageTests
{
	[Test]
	public void ParseTheYamlFile() {
		var f = Language.LoadFromYaml (LanguageYamlFile.ParseYaml());
		var french = f.First (x => x.Name == "French");
		Assert.AreEqual ("C'est la vie", french.Description);
		var english = f.First (x => x.Name == "English");
		Assert.AreEqual ("Good day old boy", english.Description);
	}

	private const string LanguageYamlFile = @"--- 
- language: 
  name: French
  description: C'est la vie
- language:
  name: English
  description: Good day old boy
";
}


