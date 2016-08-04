using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using ShortLegStudio.RPG.Characters;
using System;
using ShortLegStudio.RPG.Equipment;

public class LinkTextToProperty : MonoBehaviour {
	private Text text;
	private CharacterGeneratorController character;
	public string Property;

	void Start () {
		text = GetComponent<Text> ();
		character = FindObjectOfType<CharacterGeneratorController> ();
		character.Generated += Character_Generated;
	}

	void Character_Generated (object sender, System.EventArgs e) {
		text.text = GetProperty (character.Character);
	}

	private string GetProperty(CharacterSheet character) {
		switch (Property) {
			case "AlignmentSizeType":
				return string.Format ("{0} {1} humanoid({2})", character.Alignment.ShortString(), character.Size.Size, character.Race.Name);
			case "ArmorClass":
				return string.Format ("{0}, touch {1}, flat-footed {2}", 
					character.Defense.ArmorClass(), 
					character.Defense.TouchArmorClass(), 
					character.Defense.FlatFootedArmorClass());
			case "BaseAttackBonus":
				return string.Format ("{0}", character.Offense.BaseAttackBonus.TotalValue.ToModifierString());
			case "CMB":
				return string.Format ("{0}", character.Offense.CombatManueverBonus().ToModifierString());
			case "CMD":
				return string.Format ("{0}", character.Offense.CombatManueverDefense());
			case "DefenseAbilities":
				return string.Format("Defense!");
			case "Feats":
				return MakeFeatList (character);
			case "FortitudeSave":
				return character.Defense.FortitudeSave ().ToModifierString ();
			case "GenderRaceClass":
				return string.Format ("{0} {1} {2} {3}", character.Gender, character.Race.Name, character.Class.Name, character.Level);
			case "HitPoints":
				return character.MaxHitPoints.ToString ();
			case "Initiative":
				return character.Initiative.ToString();
			case "Languages":
				return GetLanguageList (character);
			case "MovementSpeed":
				return string.Format ("{0} ft ({1} sq)", character.Movement.BaseMovement.TotalValue, character.Movement.BaseSquares);
			case "Name":
				return character.Name;
			case "OtherGear":
				return InventoryList (character.Inventory.All);
			case "Proficiencies":
				return MakeProficiencyList(character);
			case "ReflexSave":
				return character.Defense.ReflexSave ().ToModifierString ();
			case "Senses":
				return GetSenses (character);
			case "SkillsList":
				return MakeSkillList(character);

			case "Strength":
			case "Dexterity":
			case "Constitution":
			case "Intelligence":
			case "Wisdom":
			case "Charisma":
				return string.Format("{0} ({1})", character.Abilities.GetScore(Property), character.Abilities.GetModifier(Property).ToModifierString());
			case "WeaponOneType":
				return character.Offense.Attacks().ToList () [0].Weapon.GetBasicType();
			case "WeaponTwoType":
				return character.Offense.Attacks().ToList () [1].Weapon.GetBasicType();
			case "WeaponOneInfo":
				return character.Offense.Attacks().ToList () [0].ToString();
			case "WeaponTwoInfo":
				return character.Offense.Attacks().ToList () [1].ToString();
				
			case "WillSave":
				return character.Defense.WillSave ().ToModifierString ();
		}

		return "NOT FOUND: " + Property;
	}

	private string MakeSkillList(CharacterSheet character) {
		return string.Join(",", character.SkillRanks.GetRankedSkills().Select(
			x => x.ToString()
		).ToArray<string>());
	}


	private string MakeFeatList(CharacterSheet character) {
		return string.Join (",",
			character.Feats.Select (
				x => {
					return x.Name;
				}
			).ToArray<String> ()
		);
	}

	private string GetSenses(CharacterSheet character) {
		var senses = character.GetSpecialAbilities("sense");
		var str = string.Join(",", senses.ToArray());
		return str + " ; " + character.SkillRanks.GetSkill("Perception").ToString();
		//return perception score
	}

	private string GetWeaponInfo(CharacterSheet character, Weapon wpn) {
		var format = "{0} {1} ({2})";
		if (wpn.Type == WeaponType.Ranged) {
			return string.Format (format, wpn.Name, character.Offense.RangeAttackBonus().ToModifierString(), wpn.Damage);
		} else {
			return string.Format (format, wpn.Name, character.Offense.MeleeAttackBonus().ToModifierString(), wpn.Damage);
		}
	}

	private string GetLanguageList(CharacterSheet character) {
		return string.Join(",", character.Languages.Select (x => x.Name).ToArray());
	}

	private string InventoryList(IEnumerable<IEquipment> inv) {
		return string.Join (", ", 
			inv.Select (x => x.Name).ToArray()
		);
	}

	private string MakeProficiencyList(CharacterSheet character) {
		return string.Join(", ",
			character.Offense.WeaponProficiencies.Select(x => x.Name).ToArray()
		);
	}
}
