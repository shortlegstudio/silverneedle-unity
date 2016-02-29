using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using ShortLegStudio.RPG;

[TestFixture]
public class CharacterTests {

    [Test]
    public void CharactersHaveVitalStats() {
		var sheet = new CharacterSheet ();
		sheet.Name = "Foobar";
		sheet.Alignment = CharacterAlignment.LawfulGood;
		sheet.Height = 72;
		sheet.Weight = 150;
		Assert.AreEqual ("Foobar", sheet.Name);
		Assert.AreEqual (CharacterAlignment.LawfulGood, sheet.Alignment);
		Assert.AreEqual (72, sheet.Height);
		Assert.AreEqual (150, sheet.Weight);
    }
}
