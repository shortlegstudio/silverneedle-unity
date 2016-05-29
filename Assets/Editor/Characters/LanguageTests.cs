using System;
using System.Linq;
using NUnit.Framework;
using ShortLegStudio;
using ShortLegStudio.RPG.Characters;
using ShortLegStudio.RPG.Repositories;

[TestFixture]
public class LanguageTests
{
	[Test]
	public void ParseTheYamlFile() {
		var gateway = new LanguageYamlRepository (LanguageYamlFile.ParseYaml ());
		var french = gateway.All().First (x => x.Name == "French");
		Assert.AreEqual ("C'est la vie", french.Description);
		var english = gateway.All().First (x => x.Name == "English");
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


