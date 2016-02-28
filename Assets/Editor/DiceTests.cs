using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using ShortLegStudio.Dice;
using System;
using System.Linq;

[TestFixture]
public class DiceTests {

	private void ValidateAllSides(Die die) {
		var results = new bool[die.SideCount()];
		for (var counter = 0; counter < 1000 * die.SideCount(); counter++) {
			var roll = die.Roll ();
			results [roll - 1] = true;
			if (results.All (x => x)) {
				return;
			}
		}
		Assert.Fail ("Not all sides were returned: " + string.Join(",", Array.ConvertAll(results, z => z.ToString())) );
	}

    [Test]
    public void D4ReturnsAllSideValues()
    {
		ValidateAllSides (Die.d4());
		Assert.Pass ();
    }

	[Test]
	public void D6ReturnsAllSideValues()
	{
		ValidateAllSides (Die.d6());
		Assert.Pass ();
	}

	[Test]
	public void D8ReturnsAllSideValues()
	{
		ValidateAllSides (Die.d8());
		Assert.Pass ();
	}

	[Test]
	public void D10ReturnsAllSideValues()
	{
		ValidateAllSides (Die.d10());
		Assert.Pass ();
	}

	[Test]
	public void D12ReturnsAllSideValues()
	{
		ValidateAllSides (Die.d12());
		Assert.Pass ();
	}

	[Test]
	public void D20ReturnsAllSideValues()
	{
		ValidateAllSides (Die.d20());
		Assert.Pass ();
	}

	[Test]
	public void D100ReturnsAllSideValues()
	{
		ValidateAllSides (Die.d100());
		Assert.Pass ();
	}

	[Test]
	public void GetProperSidesOfDefaultDie() {
		Assert.AreEqual (DiceSides.d4, Die.d4 ().Sides);
		Assert.AreEqual (DiceSides.d6, Die.d6 ().Sides);
		Assert.AreEqual (DiceSides.d8, Die.d8 ().Sides);
		Assert.AreEqual (DiceSides.d10, Die.d10 ().Sides);
		Assert.AreEqual (DiceSides.d12, Die.d12 ().Sides);
		Assert.AreEqual (DiceSides.d20, Die.d20 ().Sides);
		Assert.AreEqual (DiceSides.d100, Die.d100 ().Sides);
	}

	[Test]
	public void ToStringReturnsALogicalVersionOfDie() {
		var d4 = Die.d4 ();
		var result = d4.Roll ();
		var expectedString = string.Format ("[Die: Sides={0}, LastRoll={1}]", d4.Sides, result);
		Assert.AreEqual (expectedString, d4.ToString ());
	}

	[Test]
	public void TwoDifferentDiceWithTheSameSidesAndRollAreEqual() {
		var dieOne = Die.d8 ();
		var dieTwo = Die.d8 ();

		Assert.AreEqual (dieOne, dieTwo);
	}

	[Test]
	public void CreateAnArrayOfDiceByPassingANumberIntoTheHelperMethod() {
		var diceArray = Die.GetDice (DiceSides.d12, 4);
		Assert.AreEqual (new Die[] { Die.d12 (), Die.d12 (), Die.d12 (), Die.d12 () }, diceArray);
	}
}
