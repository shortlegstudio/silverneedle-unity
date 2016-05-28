using System.Linq;
using ShortLegStudio;

namespace ShortLegStudio.RPG.Characters.Generators {
	public static class SkillPointGenerator  {
		public static void AssignSkillPointsRandomly(CharacterSheet character) {
			var points = character.GetSkillPointsPerLevel ();
			var skillList = character.SkillRanks.GetSkills ().ToList ();
			for (var x = 0; x < points; x++) {
				var skill = skillList.ChooseOne ();
				while (skill.Ranks >= character.Level) {
					skill = skillList.ChooseOne ();
				}

				skill.AddRank ();
			}
		}
	}
}