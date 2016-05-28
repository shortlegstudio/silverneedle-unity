using System;
using NUnit.Framework;
using ShortLegStudio.RPG.Characters;
using ShortLegStudio.RPG.Equipment;
using ShortLegStudio;
using System.Security.Policy;

[TestFixture]
[Category("Equipment")]
public class WeaponTests {
	[Test]
	public void DamageStatisticsCanBeConvertedBasedOnSize() {
		Assert.AreEqual ("1d4", Weapon.ConvertDamageBySize("1d6", CharacterSize.Small));
		Assert.AreEqual ("2d6", Weapon.ConvertDamageBySize ("1d8", CharacterSize.Large));
		Assert.AreEqual ("2d6", Weapon.ConvertDamageBySize ("2d10", CharacterSize.Tiny));
		Assert.AreEqual ("1d6", Weapon.ConvertDamageBySize ("1d8", CharacterSize.Small));
		Assert.AreEqual ("1d8", Weapon.ConvertDamageBySize ("1d8", CharacterSize.Medium));
	}

	[Test]
	[ExpectedException(typeof(NotImplementedException))]
	public void NotImplementedExceptionTriggeredForNotSupportedSizes() {
		Weapon.ConvertDamageBySize ("1d6", CharacterSize.Colossal);
	}
		
	[Test]
	public void DefaultCriticalValuesForWeaponsAreTwentyAndTimesTwo() {
		var wpn = new Weapon (
			"Test",
			0,
			"1d8",
			DamageTypes.Piercing,
			0,
			0,
			0,
			WeaponType.Light,
			WeaponGroup.Axes,
			WeaponTrainingLevel.Simple
		);
		Assert.AreEqual (20, wpn.CriticalThreat);
		Assert.AreEqual (2, wpn.CriticalModifier);
	}
}
