using NUnit.Framework;
using System.Linq;
using ShortLegStudio.RPG;

namespace RPG {
	[TestFixture]
	public class BasicStatTests {

	    [Test]
	    public void StatsRaiseModifiedEventWhenValueSet() {
			var stat = new BasicStat (20);
			var changeCalled = false;
			stat.Modified += (object sender, BasicStatModifiedEventArgs e) => {
				changeCalled = true;
			};

			stat.SetValue (21);
			Assert.True (changeCalled);
	    }

		[Test]
		public void StatsRaiseModifiedEventWhenAdjustmentAdded() {
			var stat = new BasicStat (20);
			var changeCalled = false;
			stat.Modified += (object sender, BasicStatModifiedEventArgs e) => {
				changeCalled = true;
			};
			stat.AddModifier (new BasicStatModifier ());

			Assert.True (changeCalled);
		}

		[Test]
		public void StatsRaiseTheOldValueAndNewValue() {
			var stat = new BasicStat (10);
			bool mod = false;
			stat.Modified += (object sender, BasicStatModifiedEventArgs e) => {
				Assert.AreEqual(10, e.OldBaseValue);
				Assert.AreEqual(14, e.NewBaseValue);
				mod = true;
			};
			stat.SetValue (14);

			Assert.IsTrue (mod);
		}

		[Test]
		public void StatsTotalUpAdjustments() {
			var stat = new BasicStat (10);
			stat.AddModifier (new BasicStatModifier (5, "Foo"));
			Assert.AreEqual (15, stat.TotalValue);
		}

		[Test]
		public void StatModifiersCanHaveConditionalModifiers() {
			var stat = new BasicStat(10);
			var mod = new ConditionalStatModifier("vs. Giants", "Skill", 5, "bonus", "Feat");
			stat.AddModifier(mod);
			Assert.AreEqual(10, stat.TotalValue);
			Assert.AreEqual(15, stat.GetConditionalScore("vs. Giants"));
			Assert.AreEqual(1, stat.GetConditions().Count());
			Assert.AreEqual("vs. Giants", stat.GetConditions().First());
		}
	}
}