using System.Linq;
using ShortLegStudio;
using ShortLegStudio.RPG.Characters;

namespace ShortLegStudio.RPG.Mechanics.CharacterGenerator {
	public class SkillPointGenerator  {
		public void AssignSkillPointsRandomly(CharacterSheet character) {
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