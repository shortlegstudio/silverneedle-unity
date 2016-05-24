using System;
using System.Linq;

namespace ShortLegStudio.RPG.Characters.Generators {
	public static class CharacterGenerator {
		public static CharacterSheet CreateLevel0() {
			var character = new CharacterSheet (Skill.GetSkills());

			character.Name = NameGenerator.CreateFullName ();
			character.Gender = EnumHelpers.ChooseOne<Gender> ();
			character.Alignment = EnumHelpers.ChooseOne<CharacterAlignment>();
			AbilityScoreGenerator.RandomStandardHeroScores (character.Abilities);
			character.SetRace(Race.GetRaces ().ChooseOne ());

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

			return character;
		}
	}
}

