using NUnit.Framework;
using ShortLegStudio.RPG.Characters;
using ShortLegStudio.RPG.Mechanics.CharacterGenerator;
using System.Linq;
using System.Collections.Generic;
using ShortLegStudio.Enchilada;


[TestFixture]
public class LanguageSelectorTests {
	[Test]
	public void PickLanguagesThatAreKnownToTheRace() {
		var race = new Race ();
		race.KnownLanguages.Add ("Elvish");
		race.KnownLanguages.Add ("Giant");
		var subject = new LanguageSelector (new LanguageTestRepo());
		var res = subject.PickLanguage (race, 0);
		Assert.AreEqual (2, res.Count ());
		Assert.IsTrue (res.Any (x => x.Name == "Elvish"));
		Assert.IsTrue (res.Any (x => x.Name == "Giant"));
	}

	[Test]
	public void PickExtraLanguagesIfSmartEnough() {
		var race = new Race ();
		race.KnownLanguages.Add ("Elvish");
		race.AvailableLanguages.Add ("Corgi");
		race.AvailableLanguages.Add ("Giant");
		var subject = new LanguageSelector (new LanguageTestRepo());

		//Pick two bonus Language -> This should always return all the above
		for (int i = 0; i < 1000; i++) {
			var res = subject.PickLanguage (race, 2);
			Assert.AreEqual (3, res.Count ());
			Assert.IsTrue (res.Any (x => x.Name == "Elvish"));
			Assert.IsTrue (res.Any (x => x.Name == "Giant"));
			Assert.IsTrue (res.Any (x => x.Name == "Corgi"));
		}
	}

	[Test]
	public void IfRunOutOfLanguagesItsOk() {
		var race = new Race ();
		race.KnownLanguages.Add ("Elvish");
		race.AvailableLanguages.Add ("Corgi");
		race.AvailableLanguages.Add ("Giant");
		var subject = new LanguageSelector (new LanguageTestRepo());

		//Pick two bonus Language -> This should always return all the above
		for (int i = 0; i < 1000; i++) {
			var res = subject.PickLanguage (race, 6);
			Assert.AreEqual (3, res.Count ());
			Assert.IsTrue (res.Any (x => x.Name == "Elvish"));
			Assert.IsTrue (res.Any (x => x.Name == "Giant"));
			Assert.IsTrue (res.Any (x => x.Name == "Corgi"));
		}
	}

	private class LanguageTestRepo : EntityGateway<Language> {
		public IEnumerable<Language> All() {
			var languages = new List<Language> ();
			languages.Add (new Language ("Elvish", "Foo"));
			languages.Add (new Language ("Boo", "foo"));
			languages.Add (new Language ("Giant", "Rawr"));
			languages.Add (new Language ("Corgi", "woof"));
			return languages;
		}

	}
}
