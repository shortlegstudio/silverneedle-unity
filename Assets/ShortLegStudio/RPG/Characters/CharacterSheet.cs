using System;
using System.Collections.Generic;
using System.Linq;
using ShortLegStudio;
using ShortLegStudio.RPG.Characters.Skills;


namespace ShortLegStudio.RPG.Characters {
	public class CharacterSheet {
		// Basic Stats
		public string Name { get; set; }
		public CharacterAlignment Alignment { get; set; }
		public Gender Gender { get; set; }
		public SizeStats Size { get; set; }

		//Race and Class
		public Race Race { get; protected set; }
		public Class Class { get; set; }

		//Levels and Experience
		public int Level { get; private set; }
		public int XP { get; private set; }

		//Abilities
		public AbilityScores Abilities { get; private set; }
		public SkillRanks SkillRanks { get; private set; }
		public IList<Trait> Traits { get; private set; }
		public IList<Feat> Feats { get; private set; }
		public Initiative Initiative { get; private set; }
		public Inventory Inventory { get; private set; }
		public IList<Language> Languages { get; private set; }

		//Combat Related
		public int MaxHitPoints { get; set; }
		public int CurrentHitPoints { get; set; } 
		public OffenseStats Offense { get; private set; }
		public DefenseStats Defense { get; private set; }
		public int BaseMovementSpeed { get; set; }

		public event EventHandler<CharacterSheetEventArgs> Modified;

		public CharacterSheet(IEnumerable<Skill> skillList) {
			Abilities = new AbilityScores ();
			Size = new SizeStats ();
			Inventory = new Inventory ();
			Initiative = new Initiative (Abilities);
			Offense = new OffenseStats (Abilities, Size);
			Defense = new DefenseStats (Abilities, Size, Inventory);
			Languages = new List<Language> ();

			SkillRanks = new SkillRanks (skillList, Abilities);
			SkillRanks.ProcessModifier(Size);

			Traits = new List<Trait> ();
			Feats = new List<Feat> ();

			Level = 1;
		}

		/// <summary>
		/// Sets this character to Level 1 in specified class
		/// </summary>
		/// <param name="cls">Cls.</param>
		public void SetClass(Class cls) {
			this.Class = cls;
			Offense.BaseAttackBonus.SetValue (GetCurrentBaseAttackBonus ());

			//Handle Armor Proficiencies
			foreach (var x in cls.ArmorProficiencies) {
				AddFeat (Feat.GetFeat (x));
			}

			Defense.LevelUpDefenseStats (cls);
		}

		private int GetCurrentBaseAttackBonus() {
			return (int)Class.BaseAttackBonusRate * Level;
		}

		private int GetSaveValue(float saveRate, int modifier) {
			var val = modifier;
			if (saveRate == 0.667f)
				val = 2;

			val += (int)(saveRate * Level);
			return (int)val;
		}

		public void SetRace(Race race) {
			Race = race;

			//Update Size
			Size.SetSize(race.SizeSetting, race.HeightRange.Roll(), race.WeightRange.Roll());

			//Update Speed
			BaseMovementSpeed = race.BaseMovementSpeed;

			//Add Ability Modifiers
			foreach (var adj in race.AbilityModifiers) {
				if (adj.RacialChose) {
					var ability = EnumHelpers.ChooseOne<AbilityScoreTypes> ();
					var a = Abilities.GetAbility (ability);
					a.AddModifier (adj);
				} else {
					var a = Abilities.GetAbility (adj.ability);
					a.AddModifier (adj);
				}
			}

			//Add Traits
			foreach (var trait in race.Traits) {
				AddTrait (trait, false);
			}
			NotifyModified ();
		}

		public void SetLevel(int level) {
			Level = level;
		}

		public void AddTrait(string trait, bool notify = true) {
			var traitInfo = Trait.GetTrait (trait);
			AddTrait (traitInfo, notify);

		}

		public void AddTrait(Trait trait, bool notify = true) {
			Traits.Add (trait);
			SkillRanks.ProcessModifier (trait);

			if (notify) {
				NotifyModified ();
			}
		}

		public void AddFeat(Feat feat, bool notify = true) {
			Feats.Add (feat);
			SkillRanks.ProcessModifier (feat);

			if (notify) {
				NotifyModified ();
			}
		}

		public CharacterSkill GetSkill(Skill skill) {
			return SkillRanks.GetSkill (skill.Name);
		}

		public int GetSkillValue(string name) {
			return SkillRanks.GetScore (name);
		}

		public bool IsClassSkill(string name) {
			if (Class == null)
				return false;

			return Class.IsClassSkill (name);
		}

		public int GetSkillPointsPerLevel() {
			return Class.SkillPoints + Abilities.GetModifier (AbilityScoreTypes.Intelligence);
		}

		public void SetHitPoints(int hp) {
			MaxHitPoints = hp;
			CurrentHitPoints = hp;
		}

		private void NotifyModified() {
			if (Modified != null) { 
				var args = new CharacterSheetEventArgs ();
				args.Sheet = this;
				Modified (this, args);
			}
		}
	}

	public class CharacterSheetEventArgs : EventArgs {
		public CharacterSheet Sheet { get; set; }
	}
}