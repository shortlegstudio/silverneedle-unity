using NUnit.Framework;
using ShortLegStudio.RPG.Characters;
using ShortLegStudio;
using System.IO;
using System.Linq;
using ShortLegStudio.RPG.Repositories;



[TestFixture]
public class RaceYamlRepositoryTests {
	Race dwarf;
	Race elf;
	Race halfling;
	Race human;

	[SetUp]
	public void SetUp() {
		var repository = new RaceYamlRepository (SkillsYamlFile.ParseYaml());
		var races = repository.All ();
		dwarf = races.First (x => x.Name == "Dwarf");
		elf = races.First (x => x.Name == "Elf");
		halfling = races.First (x => x.Name == "Halfling");
		human = races.First (x => x.Name == "Human");
	}

	[Test]
	public void LoadRaceYamlFile() {
		Assert.IsNotNull (dwarf);
		Assert.IsNotNull (elf);
		Assert.IsNotNull (halfling);
		Assert.IsNotNull (human);
	}

	[Test]
	public void HumansCanChooseAbilityModifier() {
		var mod = human.AbilityModifiers.First ();
		Assert.IsTrue (mod.RacialChose);
		Assert.AreEqual (2, mod.Modifier);

	}

	[Test]
	public void DwarvesHaveSpecificAbilitiesToModifier() {
		var cons = dwarf.AbilityModifiers.First (x => x.ability == AbilityScoreTypes.Constitution);
		Assert.AreEqual (2, cons.Modifier);

		var wis = dwarf.AbilityModifiers.First (x => x.ability == AbilityScoreTypes.Wisdom);
		Assert.AreEqual (2, wis.Modifier);

		var cha = dwarf.AbilityModifiers.First (x => x.ability == AbilityScoreTypes.Charisma);
		Assert.AreEqual (-2, cha.Modifier);
	}

	[Test]
	public void RacesHaveTraitsThatTheCanPullFrom() {
		CollectionAssert.Contains (dwarf.Traits, "Darkvision");
		CollectionAssert.Contains (dwarf.Traits, "Hardy");
		CollectionAssert.Contains (halfling.Traits, "Halfling Luck");
	}

	[Test]
	public void RacesHaveSizeInformation() {
		Assert.AreEqual (CharacterSize.Medium, dwarf.SizeSetting);
		Assert.AreEqual (CharacterSize.Small, halfling.SizeSetting);

		//Should have a dice cup for making height rolls
		var cup = dwarf.HeightRange;
		Assert.AreEqual (cup.Dice.Count, 2);
		Assert.AreEqual (cup.Modifier, 45);

		cup = human.WeightRange;
		Assert.AreEqual (cup.Dice.Count, 10);
		Assert.AreEqual (cup.Modifier, 120);
	}

	[Test]
	public void KnownLanguagesAreAssigned() {
		Assert.IsTrue(dwarf.KnownLanguages.Any(x => x == "Common"));
		Assert.IsTrue(dwarf.KnownLanguages.Any(x => x == "Dwarven"));

		Assert.IsTrue (dwarf.AvailableLanguages.Any (x => x == "Giant"));
		Assert.IsTrue (dwarf.AvailableLanguages.Any (x => x == "Gnome"));
		Assert.IsTrue (dwarf.AvailableLanguages.Any (x => x == "Terran"));
		Assert.IsTrue (dwarf.AvailableLanguages.Any (x => x == "Undercommon"));


	}

	private const string SkillsYamlFile = @"--- 
- race: 
  name: Dwarf
  abilities: 
    constitution: 2
    wisdom: 2
    charisma: -2
  size: Medium
  height: 2d4+45
  weight: 14d4+120
  traits:
    - Darkvision
    - Hardy
  languages: 
    known: Common, Dwarven
    available: Giant, Gnome, Goblin, Orc, Terran, Undercommon
- race: 
  name: Elf
  abilities:
    constitution: 2
    wisdom: 2
    charisma: -2
  size: Medium
  height: 64+2d8
  weight: 14d4+120
  traits:
    - Elfy Stuff
    - Smart Guys
  languages: 
    known: Common, Dwarven
    available: Giant, Gnome, Goblin, Orc, Terrain, Undercommon
- race:
  name: Human
  abilities:
    choose: 2
  size: Medium
  height: 2d10+58
  weight: 10d10+120
  traits:
    - Boring Stuff
    - Extra Skill Point
  languages: 
    known: Common
    available: ALL
- race: 
  name: Halfling
  size: Small
  height: 2d4+32
  weight: 14d4+120
  abilities:
    constitution: 2
    wisdom: 2
    charisma: -2
  traits:
    - Halfling Luck
    - Foobar
  languages: 
    known: Common, Halfling
    available: Gnome
...";
	
}