using System.Linq;
using ShortLegStudio.RPG.Characters;
using ShortLegStudio.RPG.Repositories;

namespace ShortLegStudio.RPG.Mechanics.CharacterGenerator {
	public static class CharacterGenerator {
		public static CharacterSheet CreateLevel0() {
			var scoreGen = new AbilityScoreGenerator ();
			var languageSelector = new LanguageSelector (new LanguageYamlRepository());
			var raceRepo = new RaceYamlRepository ();
			var nameGen = new NameGenerator ();

			var character = new CharacterSheet (Skill.GetSkills());

			character.Name = nameGen.CreateFullName ();
			character.Gender = EnumHelpers.ChooseOne<Gender> ();
			character.Alignment = EnumHelpers.ChooseOne<CharacterAlignment>();
			scoreGen.RandomStandardHeroScores (character.Abilities);
			character.SetRace(raceRepo.All ().ToList().ChooseOne ());

			character.Languages.Add (
				languageSelector.PickLanguage (
					character.Race, 
					character.Abilities.GetModifier (AbilityScoreTypes.Intelligence)
				)
			);
			return character;
		}

		public static CharacterSheet SelectClass(CharacterSheet character) {
			character.SetClass (Class.GetClasses ().ChooseOne ());
			var hp = new HitPointGenerator ();
			character.SetHitPoints (hp.RollHitPoints (character));
			return character;
		}

		public static CharacterSheet GenerateRandomCharacter() {
			var skillGen = new SkillPointGenerator ();
			var levelUpGen = new LevelUpGenerator (new HitPointGenerator());
			var character = CharacterGenerator.CreateLevel0 ();
			CharacterGenerator.SelectClass (character);
			character.AddFeat (Feat.GetQualifyingFeats (character).ToList ().ChooseOne ());

			levelUpGen.BringCharacterToLevel(character, UnityEngine.Random.Range (1, 21));

			//Assign Skill Points
			skillGen.AssignSkillPointsRandomly(character);

			//Get some gear!
			var equip = new EquipMeleeAndRangedWeapon(new WeaponYamlRepository());
			equip.AssignWeapons(character.Inventory);
			return character;
		}

	}
}

