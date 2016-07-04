using System.Linq;
using System.IO;
using NUnit.Framework;
using ShortLegStudio;
using ShortLegStudio.RPG.Characters;
using ShortLegStudio.RPG.Equipment;

namespace RPG.Characters {

	[TestFixture]
	public class WeaponProficiencyTests {
		[Test]
		public void SimpleWeaponsAreProficientForSimple() {
			var prof = new WeaponProficiency("simple");
			var wpn = new Weapon();
			wpn.Level = WeaponTrainingLevel.Simple;
			Assert.IsTrue(prof.IsProficient(wpn));
			wpn.Level = WeaponTrainingLevel.Martial;
			Assert.IsFalse(prof.IsProficient(wpn));
		}

		[Test]
		public void MartialWeaponsAreProficientIfMartiallyTrained() {
			var prof = new WeaponProficiency("martial");
			var wpn = new Weapon();
			wpn.Level = WeaponTrainingLevel.Martial;
			Assert.IsTrue(prof.IsProficient(wpn));
		}

		[Test]
		public void MatchesBasedOnNameIfNotTrainingLevel() {
			var prof = new WeaponProficiency("Shortbow");
			var wpn = new Weapon();
			wpn.Name = "Longsword";
			Assert.IsFalse(prof.IsProficient(wpn));
			wpn.Name = "Shortbow";
			Assert.IsTrue(prof.IsProficient(wpn));
		}

	}
}