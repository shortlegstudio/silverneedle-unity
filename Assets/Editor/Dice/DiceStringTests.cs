using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using ShortLegStudio.Dice;
using System;
using System.Linq;

[TestFixture]
public class DiceStringTests {
	[Test]
	public void ParseSingleDiceStrings() {
		var d = DiceStrings.ParseSides ("d6");
		Assert.AreEqual (DiceSides.d6, d);

		d = DiceStrings.ParseSides ("d8");
		Assert.AreEqual (DiceSides.d8, d);
	}
}
