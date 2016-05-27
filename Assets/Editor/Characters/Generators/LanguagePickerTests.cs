using NUnit.Framework;
using ShortLegStudio.RPG.Characters;
using ShortLegStudio.RPG.Characters.Generators;
using System.Linq;
using System.Collections.Generic;


[TestFixture]
public class LanguagePickerTests {
	IList<Language> languages;

	[SetUp]
	public void SetUp() {
		languages = new List<Language> ();
		languages.Add (new Language ("Elvish", "Foo"));
		languages.Add (new Language ("Boo", "foo"));
		languages.Add (new Language ("Giant", "Rawr"));
		languages.Add (new Language ("Corgi", "woof"));
	}
	[Test]
	public void PickLanguagesThatAreKnownToTheRace() {
		var race = new Race ();
		race.AddKnownLanguage ("Elvish");
		race.AddKnownLanguage ("Giant");

		var res = LanguagePicker.PickLanguage (race, languages, 0);
		Assert.AreEqual (2, res.Count ());
		Assert.IsTrue (res.Any (x => x.Name == "Elvish"));
		Assert.IsTrue (res.Any (x => x.Name == "Giant"));
	}

	[Test]
	public void PickExtraLanguagesIfSmartEnough() {
		var race = new Race ();
		race.AddKnownLanguage ("Elvish");
		race.AddAvailableLanguage ("Corgi");
		race.AddAvailableLanguage ("Giant");

		//Pick two bonus Language -> This should always return all the above
		for (int i = 0; i < 1000; i++) {
			var res = LanguagePicker.PickLanguage (race, languages, 2);
			Assert.AreEqual (3, res.Count ());
			Assert.IsTrue (res.Any (x => x.Name == "Elvish"));
			Assert.IsTrue (res.Any (x => x.Name == "Giant"));
			Assert.IsTrue (res.Any (x => x.Name == "Corgi"));
		}
	}

	[Test]
	public void IfRunOutOfLanguagesItsOk() {
		var race = new Race ();
		race.AddKnownLanguage ("Elvish");
		race.AddAvailableLanguage ("Corgi");
		race.AddAvailableLanguage ("Giant");

		//Pick two bonus Language -> This should always return all the above
		for (int i = 0; i < 1000; i++) {
			var res = LanguagePicker.PickLanguage (race, languages, 6);
			Assert.AreEqual (3, res.Count ());
			Assert.IsTrue (res.Any (x => x.Name == "Elvish"));
			Assert.IsTrue (res.Any (x => x.Name == "Giant"));
			Assert.IsTrue (res.Any (x => x.Name == "Corgi"));
		}
	}
}
