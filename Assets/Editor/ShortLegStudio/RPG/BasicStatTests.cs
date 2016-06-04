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
		public void StatsCanHaveSituationalAdjustments() {
			var stat = new BasicStat (10);
			stat.AddModifier (new ConditionalStatModifier (5, "Foo", "vs. Dancing"));
			stat.AddModifier (new ConditionalStatModifier (5, "Foo", "vs. Comedy"));
			Assert.AreEqual (10, stat.TotalValue);
			Assert.AreEqual("vs. Dancing", stat.ConditionalModifiers.First().Condition);
			Assert.AreEqual (15, stat.ConditionalScore("vs. Dancing"));
		}

		[Test]
		public void ConditionalModifiersAreBasedOnAllStats() {
			var stat = new BasicStat (10);
			stat.AddModifier(new BasicStatModifier(10, "Bar"));
			stat.AddModifier (new ConditionalStatModifier (5, "Foo", "vs. Dancing"));
			Assert.AreEqual (20, stat.TotalValue);
			Assert.AreEqual("vs. Dancing", stat.ConditionalModifiers.First().Condition);
			Assert.AreEqual (25, stat.ConditionalScore("vs. Dancing"));
		}
	}
}