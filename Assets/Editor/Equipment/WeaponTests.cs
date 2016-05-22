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
	public void AllImportantStatsForALongSwordAreAvailable() {
		var weapons = Weapon.LoadFromYaml(WeaponYamlFile.ParseYaml());
		var longsword = weapons [0];
		Assert.AreEqual ("Longsword", longsword.Name);
		Assert.AreEqual ("1d8", longsword.Damage);
		Assert.AreEqual (4, longsword.Weight);
		Assert.AreEqual (19, longsword.CriticalThreat);
		Assert.AreEqual (2, longsword.CriticalModifier);
		Assert.AreEqual (DamageTypes.Slashing, longsword.DamageType);
		Assert.AreEqual (WeaponType.OneHanded, longsword.Type);
		Assert.AreEqual (WeaponGroup.HeavyBlades, longsword.Group);
		Assert.AreEqual (WeaponTrainingLevel.Martial, longsword.Level);
	}

	[Test]
	public void AllImportantStatsForADaggerAreAvailable() {
		var weapons = Weapon.LoadFromYaml(WeaponYamlFile.ParseYaml());
		var dagger = weapons [1];
		Assert.AreEqual ("Dagger", dagger.Name);
		Assert.AreEqual ("1d4", dagger.Damage);
		Assert.AreEqual (1, dagger.Weight);
		Assert.AreEqual (19, dagger.CriticalThreat);
		Assert.AreEqual (2, dagger.CriticalModifier);
		Assert.AreEqual (DamageTypes.Piercing, dagger.DamageType);
		Assert.AreEqual (10, dagger.Range);
		Assert.AreEqual (WeaponType.Light, dagger.Type);
		Assert.AreEqual (WeaponGroup.LightBlades, dagger.Group);
		Assert.AreEqual (WeaponTrainingLevel.Simple, dagger.Level);
	}


	private const string WeaponYamlFile = @"--- 
- weapon: 
  name: Longsword
  damage: 1d8
  damage_type: Slashing
  critical_threat: 19
  critical_modifier: 2
  weight: 4
  group: HeavyBlades
  type: OneHanded
  training_level: Martial
- weapon:
  name: Dagger
  damage: 1d4
  damage_type: Piercing
  critical_threat: 19
  critical_modifier: 2
  weight: 1
  range: 10
  group: LightBlades
  type: Light
  training_level: Simple
";
}
