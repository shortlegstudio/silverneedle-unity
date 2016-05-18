using System;
using System.Collections.Generic;
using System.Linq;

namespace ShortLegStudio.RPG.Characters {
	public interface ISkillRanks {
		int GetScore (string skill);
		CharacterSkill GetSkill (string skill);
		IEnumerable<CharacterSkill> GetSkills ();
	}

	public class SkillRanks : ISkillRanks {
		private IDictionary<string, CharacterSkill> _skills { get; set; }

		public SkillRanks (IEnumerable<Skill> skills, AbilityScores scores) {
			_skills = new Dictionary<string, CharacterSkill> ();

			FillSkills (skills, scores);
		}

		public int GetScore(string skill) {
			return _skills [skill].Score;
		}

		public CharacterSkill GetSkill(string skill) {
			return _skills [skill];
		}

		public IEnumerable<CharacterSkill> GetSkills() {
			return _skills.Values;
		}

		public IEnumerable<CharacterSkill> GetRankedSkills() {
			return _skills.Values.Where (x => 
				x.Ranks > 0
			);
		}

		public void ProcessModifier(ISkillModifier modifier) {
			foreach (var a in modifier.SkillModifiers) {
				var sk = _skills [a.SkillName];
				sk.AddAdjustment (a);
			}
		}
			
		private void FillSkills(IEnumerable<Skill> skills, AbilityScores scores) {
			foreach (var s in skills) {
				_skills.Add(s.Name, new CharacterSkill(
					s,
					scores.GetAbility(s.Ability),
					false)
				);
			}
		}
	}
}

