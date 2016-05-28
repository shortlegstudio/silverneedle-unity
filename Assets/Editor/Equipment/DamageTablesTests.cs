using System;
using NUnit.Framework;
using ShortLegStudio.RPG.Characters;
using ShortLegStudio.RPG.Equipment;
using ShortLegStudio;
using System.Security.Policy;

[TestFixture]
[Category("Equipment")]
public class DamageTablesTests {
	[Test]
	public void DamageStatisticsCanBeConvertedBasedOnSize() {
		Assert.AreEqual ("1d4", DamageTables.ConvertDamageBySize("1d6", CharacterSize.Small));
		Assert.AreEqual ("2d6", DamageTables.ConvertDamageBySize ("1d8", CharacterSize.Large));
		Assert.AreEqual ("2d6", DamageTables.ConvertDamageBySize ("2d10", CharacterSize.Tiny));
		Assert.AreEqual ("1d6", DamageTables.ConvertDamageBySize ("1d8", CharacterSize.Small));
		Assert.AreEqual ("1d8", DamageTables.ConvertDamageBySize ("1d8", CharacterSize.Medium));
	}


	[Test]
	[ExpectedException(typeof(NotImplementedException))]
	public void NotImplementedExceptionTriggeredForNotSupportedSizes() {
		DamageTables.ConvertDamageBySize ("1d6", CharacterSize.Colossal);
	}

}