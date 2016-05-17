using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using ShortLegStudio.RPG.Characters;

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
		stat.AddAdjustment (new BasicStatAdjustment ());

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
}
