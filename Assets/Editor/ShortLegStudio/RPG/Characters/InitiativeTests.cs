using System.Linq;
using System.IO;
using NUnit.Framework;
using ShortLegStudio;
using ShortLegStudio.RPG.Characters;
using System.Text;

namespace RPG.Characters {

	[TestFixture]
	public class InitiativeTests {
		[Test]
		public void InitiativeIsBasedOnDexterity() {
			var abilities = new AbilityScores ();
			abilities.SetScore (AbilityScoreTypes.Dexterity, 18);
			var init = new Initiative (abilities);
			Assert.AreEqual (4, init.TotalValue);
		}
	}
}