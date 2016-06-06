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
			return _skills [skill].Score();
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

		public void ProcessModifier(IModifiesStats modifier) {
			foreach (var a in modifier.Modifiers) {
				CharacterSkill sk;
				if (_skills.TryGetValue (a.StatName, out sk)) {
					sk.AddModifier (a);
				} else {
					ShortLog.ErrorFormat ("Skill: {0} was not found in the Skill Ranks and modifiers could not be applied.", a.StatName);
				}
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

