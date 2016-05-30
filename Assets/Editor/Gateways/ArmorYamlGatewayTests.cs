using System;
using System.Linq;
using NUnit.Framework;
using ShortLegStudio.RPG.Equipment;
using ShortLegStudio.RPG.Equipment.Gateways;
using ShortLegStudio;
using System.Runtime.InteropServices;

[TestFixture]
public class ArmorYamlGatewayTests
{
	ArmorYamlGateway gateway;

	[SetUp]
	public void ReadArmorYamlString() {
		gateway = new ArmorYamlGateway (ArmorYamlFile.ParseYaml ());
	}
		
	[Test]
	public void YouCanGetAllTheArmors() {
		var armors = gateway.All ();
		Assert.AreEqual (2, armors.Count ());
	}

	[Test]
	public void YouCanAccessASpecificSetOfArmor() {
		var leather = gateway.GetArmorByName ("Leather Armor");
		Assert.IsNotNull (leather);
		Assert.AreEqual ("Leather Armor", leather.Name);

		var plate = gateway.GetArmorByName ("Full Plate");
		Assert.IsNotNull (plate);
		Assert.AreEqual ("Full Plate", plate.Name);
	}

	[Test]
	public void ArmorLoadsItsArmorClass() {
		var leather = gateway.GetArmorByName ("Leather Armor");
		Assert.AreEqual (2, leather.ArmorClass);
		var plate = gateway.GetArmorByName ("Full Plate");
		Assert.AreEqual (9, plate.ArmorClass);
	}

	[Test]
	public void ArmorHasWeight() {
		var leather = gateway.GetArmorByName ("Leather Armor");
		Assert.AreEqual (15, leather.Weight);
	}

	[Test]
	public void ArmorHasMaxDexBonus() {
		var plate = gateway.GetArmorByName ("Full Plate");
		Assert.AreEqual (1, plate.MaximumDexterityBonus);
	}	

	[Test]
	public void ArmorHasArcaneSpellFailure() {
		var leather = gateway.GetArmorByName ("Leather Armor");
		Assert.AreEqual (10, leather.ArcaneSpellFailureChance);
	}

	[Test]
	public void ArmorHasACheckPenalty() {
		var plate = gateway.GetArmorByName ("Full Plate");
		Assert.AreEqual (-6, plate.ArmorCheckPenalty);
	}

	const string ArmorYamlFile = @"
- armor:
  name: Leather Armor
  armor_class: 2
  weight: 15
  maximum_dexterity_bonus: 6
  armor_check_penalty: 0
  arcane_spell_failure_chance: 10
- armor:
  name: Full Plate
  armor_class: 9
  weight: 50
  maximum_dexterity_bonus: 1
  armor_check_penalty: -6
  arcane_spell_failure_chance: 35
";
}


