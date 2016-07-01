using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using ShortLegStudio;
using ShortLegStudio.Dice;

namespace ShortLegStudio.RPG.Characters {
	
	public class Class {
		public const float GOOD_SAVE_RATE = 0.667f;
		public const float POOR_SAVE_RATE = 0.334f;

		public string Name { get; set; }
		public IList<string> ClassSkills { get; set; }
		public int SkillPoints { get; set; }
		public DiceSides HitDice { get; set; }
		public float BaseAttackBonusRate { get; set; }
		public float FortitudeSaveRate { get; set; }
		public float ReflexSaveRate { get; set; }
		public float WillSaveRate { get; set; }
		public IList<string> ArmorProficiencies { get; set; }

		public bool IsFortitudeGoodSave { 
			get { return FortitudeSaveRate == GOOD_SAVE_RATE; }
		}
		public bool IsReflexGoodSave {
			get { return ReflexSaveRate == GOOD_SAVE_RATE; }
		}
		public bool IsWillGoodSave {
			get { return WillSaveRate == GOOD_SAVE_RATE; }
		}

		public Class() {
			ClassSkills = new List<string> ();
			ArmorProficiencies = new List<string> ();
		}

		public bool IsClassSkill(string name) {
			//Craft, Profession, and Perform are special cases 
			//All skills in that group are considered class skills 
			// in this case 
			//
			// Truncate any skill names like 'Craft (Food)' -> Craft
			// Profession (Farmer) => Profession
			// etc...
			var pattern = "(\\(.*\\))";
			var skillName = Regex.Replace (name, pattern, string.Empty).Trim();
			return ClassSkills.Any (x => x == skillName);
		}

		public void AddClassSkill(string name) {
			if (!IsClassSkill (name))
				ClassSkills.Add (name);
			else
				ShortLog.Debug ("Not adding class skill as it already is there: " + name);
		}




	}
}