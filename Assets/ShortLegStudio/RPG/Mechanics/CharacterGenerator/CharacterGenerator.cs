using System.Linq;
using ShortLegStudio.RPG.Characters;
using ShortLegStudio.RPG.Repositories;

namespace ShortLegStudio.RPG.Mechanics.CharacterGenerator {
	public static class CharacterGenerator {
		public static CharacterSheet CreateLevel0() {
			var character = new CharacterSheet (Skill.GetSkills());

			character.Name = NameGenerator.CreateFullName ();
			character.Gender = EnumHelpers.ChooseOne<Gender> ();
			character.Alignment = EnumHelpers.ChooseOne<CharacterAlignment>();
			AbilityScoreGenerator.RandomStandardHeroScores (character.Abilities);
			character.SetRace(Race.GetRaces ().ChooseOne ());

			var languageSelector = new LanguageSelector (new LanguageYamlRepository());

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
			character.SetHitPoints (HitPointGenerator.RollHitPoints (character));
			return character;
		}

		public static CharacterSheet GenerateRandomCharacter() {
			var character = CharacterGenerator.CreateLevel0 ();
			CharacterGenerator.SelectClass (character);
			character.AddFeat (Feat.GetQualifyingFeats (character).ToList ().ChooseOne ());

			LevelUpGenerator.BringCharacterToLevel(character, UnityEngine.Random.Range (1, 21));

			//Assign Skill Points
			SkillPointGenerator.AssignSkillPointsRandomly(character);

			//Get some gear!
			var equip = new EquipMeleeAndRangedWeapon(new WeaponYamlRepository());
			equip.AssignWeapons(character.Inventory);
			return character;
		}

	}
}

