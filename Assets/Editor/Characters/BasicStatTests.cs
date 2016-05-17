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
		stat.Modified += (object sender, System.EventArgs e) => {
			changeCalled = true;
		};

		stat.SetValue (21);
		Assert.True (changeCalled);
    }

	[Test]
	public void StatsRaiseModifiedEventWhenAdjustmentAdded() {
		var stat = new BasicStat (20);
		var changeCalled = false;
		stat.Modified += (object sender, System.EventArgs e) => {
			changeCalled = true;
		};
		stat.AddAdjustment (new BasicStatAdjustment ());

		Assert.True (changeCalled);
	}


}
