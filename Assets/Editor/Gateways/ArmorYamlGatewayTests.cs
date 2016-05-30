using System;
using System.Linq;
using NUnit.Framework;
using ShortLegStudio.RPG.Equipment;
using ShortLegStudio.RPG.Equipment.Gateways;
using ShortLegStudio;

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


	const string ArmorYamlFile = @"
- armor:
  name: Leather Armor
  armorclass: 2
- armor:
  name: Full Plate
  armorclass: 9
";
}


