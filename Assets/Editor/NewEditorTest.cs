using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using ShortLegStudio.Dice;
using System;
using System.Linq;

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
}
