using NUnit.Framework;
using ShortLegStudio.RPG.Characters;
using System.IO;
using ShortLegStudio;
using System.Linq;
using System.Collections.Generic;
using ShortLegStudio.RPG.Characters.Skills;

namespace RPG.Characters {

	[TestFixture]
	public class PrerequisiteTests {
		[Test]
		public void ParseSomeYaml() {
			var yamlNode = PrerequisitesYaml.ParseYaml();
			var prereq = yamlNode.GetNode ("prerequisites");

			var prereqs = new Prerequisites (prereq);

			Assert.AreEqual (4, prereqs.Count);
			Assert.IsInstanceOf<AbilityPrerequisite> (prereqs.First ());
		}

		[Test]
		public void AlwaysQualifiedIfNoQualificationsNeeded() {
			var pre = new Prerequisites ();
			Assert.IsTrue(pre.Qualified(new CharacterSheet(new List<Skill>())));
		}

		[Test]
		public void AbilityIsQualifiedIfExceedingScore() {
			var pre = new AbilityPrerequisite ("Intelligence 13");
			var c = new CharacterSheet (new List<Skill>());
			c.Abilities.SetScore (AbilityScoreTypes.Intelligence, 15);
			Assert.IsTrue (pre.Qualified (c));
		}

		[Test]
		public void AbilityIsNotQualifiedIfNotExceedingScore() {
			var pre = new AbilityPrerequisite ("Intelligence 13");
			var c = new CharacterSheet (new List<Skill>());
			c.Abilities.SetScore (AbilityScoreTypes.Intelligence, 11);
			Assert.IsFalse (pre.Qualified (c));
		}




		private const string PrerequisitesYaml = @"--- 
prerequisites:
  - ability: Intelligence 13
  - race: Elf
  - feat: Weapon Finesse
  - skillranks: Acrobatics 4
";
	}
}

