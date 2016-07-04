using System;
using System.Collections.Generic;
using ShortLegStudio.RPG.Equipment;
using ShortLegStudio.Dice;

namespace ShortLegStudio.RPG.Characters {
	public class OffenseStats : IStatTracker {
		const string CMD_STAT_NAME = "CMD";
		const string CMB_STAT_NAME = "CMB";
		public const int UNPROFICIENT_MODIFIER = -4;

		public BasicStat BaseAttackBonus { get; private set; }
		private AbilityScores AbilityScores { get; set; }
		private SizeStats Size { get; set; }
		private BasicStat CMD { get; set; }
		private BasicStat CMB { get; set; }
		private Inventory Inventory;


		public OffenseStats (AbilityScores scores, SizeStats size, Inventory inventory) {
			BaseAttackBonus = new BasicStat ();
			CMD = new BasicStat(10);
			CMB = new BasicStat();
			AbilityScores = scores;
			Size = size;
			Inventory = inventory;
		}

		public int MeleeAttackBonus() {
			return BaseAttackBonus.TotalValue + AbilityScores.GetModifier (AbilityScoreTypes.Strength) + Size.SizeModifier;
		}

		public int RangeAttackBonus() {
			return BaseAttackBonus.TotalValue + AbilityScores.GetModifier (AbilityScoreTypes.Dexterity) + Size.SizeModifier;
		}

		public int CombatManueverBonus() {
			
			return CMB.TotalValue + BaseAttackBonus.TotalValue + AbilityScores.GetModifier (AbilityScoreTypes.Strength) - Size.SizeModifier;
		}

		public int CombatManueverDefense() {
			return CMD.TotalValue + BaseAttackBonus.TotalValue
				+ AbilityScores.GetModifier (AbilityScoreTypes.Strength)
				+ AbilityScores.GetModifier (AbilityScoreTypes.Dexterity)
				- Size.SizeModifier;
		}

		public void ProcessModifier(IModifiesStats statModifier) {
			foreach (var m in statModifier.Modifiers) {
				switch (m.StatName) {
					case CMD_STAT_NAME:
						CMD.AddModifier(m);
						break;
					case CMB_STAT_NAME:
						CMB.AddModifier(m);
						break;
				}
			}
		}

		public void AddWeaponProficiencies(IEnumerable<string> prof) {

		}

		public IList<AttackStatistic> Attacks() {
			var attacks = new List<AttackStatistic>();

			//Get A list of weapons and return them
			foreach (var weapon in Inventory.Weapons) {
				var atk = new AttackStatistic();
				atk.Name = weapon.Name;
				atk.Weapon = weapon;
				atk.Damage = DiceStrings.ParseDice(DamageTables.ConvertDamageBySize(weapon.Damage, Size.Size));
				if (weapon.IsMelee) {
					atk.Damage.Modifier = AbilityScores.GetModifier(AbilityScoreTypes.Strength);
					atk.AttackBonus = MeleeAttackBonus();
				}
				else if (weapon.IsRanged) {
					atk.AttackBonus = RangeAttackBonus();
				}
				attacks.Add(atk);
			}

			return attacks;
		}

		public struct AttackStatistic {
			public string Name;
			public Weapon Weapon;
			public Cup Damage;
			public int AttackBonus;

			public override string ToString() {
				return string.Format("{0} {1} ({2} / {3}x{4})", Name, AttackBonus.ToModifierString(), Damage, Weapon.CriticalThreat, Weapon.CriticalModifier );
			}
		}
	}
}

