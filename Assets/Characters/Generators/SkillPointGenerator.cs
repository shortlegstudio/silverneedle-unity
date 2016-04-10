using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using ShortLegStudio;

namespace ShortLegStudio.RPG.Characters.Generators {
	public static class SkillPointGenerator  {
		public static void AssignSkillPointsRandomly(CharacterSheet character) {
			var points = character.GetSkillPointsPerLevel ();

			for (var x = 0; x < points; x++) {
				var skill = character.GetSkillList ().ChooseOne ();
				while (skill.Ranks >= character.Level) {
					skill = character.GetSkillList ().ChooseOne ();
				}

				skill.AddRank ();
			}
		}
	}
}