using NUnit.Framework;
using ShortLegStudio.RPG.Characters;
using System.Linq;
using System.Xml.Linq;

[TestFixture]
public class SizeStatsTests {

	[Test]
	public void ASmallCreatureAsASizeModifierOfOne() {
		var smallCreature = new SizeStats (CharacterSize.Small, 3.2f, 36.1f);
		Assert.AreEqual (1, smallCreature.SizeModifier);
	}

	[Test]
	public void UpdatingTheSizeChangesTheModifier() {
		var smallToStart = new SizeStats (CharacterSize.Small, 3.2f, 28.1f);
		smallToStart.SetSize (CharacterSize.Large, 8.2f, 283f);
		Assert.AreEqual (-1, smallToStart.SizeModifier);
	}

	[Test]
	public void ContainsModifiersForFlyAndStealth() {
		var medium = new SizeStats (CharacterSize.Medium, 5.9f, 184f);
		Assert.IsTrue(medium.SkillModifiers.Any(x => x.SkillName == "Stealth"));
		Assert.IsTrue (medium.SkillModifiers.Any (x => x.SkillName == "Fly"));	
	}

	[Test]
	public void SmallCreaturesProvideABonusToStealthAndFly() {
		var small = new SizeStats (CharacterSize.Small, 3.2f, 37.1f);
		var stealth = small.SkillModifiers.First (x => x.SkillName == "Stealth");
		Assert.AreEqual (4, stealth.Modifier);
	}

	[Test]
	public void ColossalCreaturesAreBadAtStealth() {
		var col = new SizeStats (CharacterSize.Colossal, 68.4f, 29932f);
		var stealth = col.SkillModifiers.First (x => x.SkillName == "Stealth");
		Assert.AreEqual (-16, stealth.Modifier);
	}

	[Test]
	public void FineCreaturesAreGoodAtFlying() {
		var fine = new SizeStats (CharacterSize.Fine, 0.4f, 0.2f);
		var fly = fine.SkillModifiers.First (x => x.SkillName == "Fly");
		Assert.AreEqual (8, fly.Modifier);
	}

	[Test]
	public void LargeCreaturesArePoorAtFlying() {
		var large = new SizeStats (CharacterSize.Large, 0.4f, 0.2f);
		var fly = large.SkillModifiers.First (x => x.SkillName == "Fly");
		Assert.AreEqual (-2, fly.Modifier);
	}
}
