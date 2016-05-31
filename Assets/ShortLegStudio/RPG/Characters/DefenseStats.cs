using System.Collections;
using System.Reflection;
using ShortLegStudio.RPG.Equipment;
using System.Linq;

namespace ShortLegStudio.RPG.Characters {
	
	public class DefenseStats  {
		const int BASE_ARMOR_CLASS = 10;
		const int GOOD_SAVE_BASE = 2;

		private AbilityScores Abilities;
		private SizeStats Size;
		private BasicStat Fortitude { get; set; }
		private BasicStat Reflex { get; set; }
		private BasicStat Will { get; set; }
		private Inventory Inventory { get; set; }

		public DefenseStats(AbilityScores abilityScores, SizeStats size, Inventory inv) {
			Abilities = abilityScores;	
			Size = size;
			Fortitude = new BasicStat ();
			Reflex = new BasicStat ();
			Will = new BasicStat ();
			Inventory = inv;
		}

		public int EquipedArmorBonus() {
			return Inventory.Armor.Sum (x => x.ArmorClass);
		}

		public int ArmorClass() {
			return BASE_ARMOR_CLASS 
				+ Abilities.GetModifier (AbilityScoreTypes.Dexterity)
				+ Size.SizeModifier
				+ EquipedArmorBonus();
		}

		public int TouchArmorClass() {
			return BASE_ARMOR_CLASS 
				+ Abilities.GetModifier (AbilityScoreTypes.Dexterity)
				+ Size.SizeModifier;
		}

		public int FlatFootedArmorClass() {
			return BASE_ARMOR_CLASS
				+ Size.SizeModifier
				+ EquipedArmorBonus();
		}

		public void SetFortitudeGoodSave() {
			Fortitude.SetValue (GOOD_SAVE_BASE);
		}

		public void SetReflexGoodSave() {
			Reflex.SetValue (GOOD_SAVE_BASE);
		}

		public void SetWillGoodSave() {
			Will.SetValue (GOOD_SAVE_BASE);
		}

		public int FortitudeSave() {
			return Fortitude.TotalValue + Abilities.GetModifier (AbilityScoreTypes.Constitution);
		}

		public int ReflexSave() {
			//Ref
			return Reflex.TotalValue + Abilities.GetModifier(AbilityScoreTypes.Dexterity);
		}

		public int WillSave() {
			return Will.TotalValue +  Abilities.GetModifier (AbilityScoreTypes.Wisdom);
		}

		public void LevelUpDefenseStats(Class cls) {
			//Mark any good saves
			if (cls.IsFortitudeGoodSave)
				SetFortitudeGoodSave ();
			if (cls.IsReflexGoodSave)
				SetReflexGoodSave ();
			if (cls.IsWillGoodSave)
				SetWillGoodSave ();

			var reason = string.Format ("LEVEL UP ({0})", cls.Name);
			//Add Adjustment for each level
			Fortitude.AddAdjustment(new BasicStatAdjustment(cls.FortitudeSaveRate, reason));
			Reflex.AddAdjustment (new BasicStatAdjustment (cls.ReflexSaveRate, reason));
			Will.AddAdjustment (new BasicStatAdjustment (cls.WillSaveRate, reason));

		}
	}
}
