﻿using System;

namespace ShortLegStudio.RPG.Characters.Generators {
	public static class CharacterGenerator {
		public static CharacterSheet CreateLevel0() {
			var character = new CharacterSheet ();

			character.Name = NameGenerator.CreateFullName ();
			character.Alignment = EnumHelpers.ChooseOne<CharacterAlignment>();

			character.SetAbilityScores (AbilityScoreGenerator.RandomStandardHeroScores ());
			character.SetSkills (Skill.GetSkills());
			character.SetRace(Race.GetRaces ().ChooseOne ());


			return character;
		}

		public static CharacterSheet SelectClass(CharacterSheet character) {
			character.SetClass (Class.GetClasses ().ChooseOne ());
			character.SetHitPoints (HitPointGenerator.RollHitPoints (character));
			return character;
		}
	}
}

