//-----------------------------------------------------------------------
// <copyright file="LinkTextToProperty.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using ShortLegStudio.Conversions;


namespace ShortLegStudio.SilverNeedle
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ShortLegStudio.RPG.Characters;
    using ShortLegStudio.RPG.Equipment;
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// Links the text field to a property of the character
    /// </summary>
    public class LinkTextToProperty : MonoBehaviour
    {
        /// <summary>
        /// The text field that is being linked
        /// </summary>
        private Text textField;

        /// <summary>
        /// The character generator that is creating characters
        /// </summary>
        private CharacterGeneratorController character;

        /// <summary>
        /// The property to tie to the character sheet
        /// This property is exposed in the inspector
        /// </summary>
        [SerializeField]
        private string property;

        /// <summary>
        /// Start this instance.
        /// </summary>
        private void Start()
        {
            this.textField = this.GetComponent<Text>();
            this.character = GameObject.FindObjectOfType<CharacterGeneratorController>();
            this.character.Generated += this.Character_Generated;
        }

        /// <summary>
        /// Called when a character is generatred
        /// </summary>
        /// <param name="sender">Object that triggered the event</param>
        /// <param name="args">EventArgs for this event</param>
        private void Character_Generated(object sender, System.EventArgs args)
        {
            this.textField.text = this.GetProperty(this.character.Character);
        }

        /// <summary>
        /// Gets the property text from the character
        /// </summary>
        /// <returns>The property.</returns>
        /// <param name="character">Character to pull the property from</param>
        private string GetProperty(CharacterSheet character)
        {
            // TODO: This really is a kind of different mapping and could really be placed around the character module itself. Nothing here is Unity Specific
            switch (this.property)
            {
                case "Age":
                    return character.Age.ToString();
                case "AlignmentSizeType":
                    return string.Format("{0} {1} humanoid({2})", character.Alignment.ShortString(), character.Size.Size, character.Race.Name);
                case "ArmorClass":
                    return string.Format(
                        "{0}, touch {1}, flat-footed {2}", 
                        character.Defense.ArmorClass(), 
                        character.Defense.TouchArmorClass(), 
                        character.Defense.FlatFootedArmorClass());
                case "BaseAttackBonus":
                    return string.Format("{0}", character.Offense.BaseAttackBonus.TotalValue.ToModifierString());
                case "CMB":
                    return string.Format("{0}", character.Offense.CombatManueverBonus().ToModifierString());
                case "CMD":
                    return string.Format("{0}", character.Offense.CombatManueverDefense());
                case "DefenseAbilities":
                    return string.Format("Defense!");
                case "Eyes":
                    return character.FacialDescription.EyeColor.ToString();
                case "Facial Hair":
                    return character.FacialDescription.FacialHair.ToString();
                case "Father":
                    return character.History.FamilyTree.Father;
                case "Feats":
                    return this.MakeFeatList(character);
                case "FortitudeSave":
                    return character.Defense.FortitudeSave.ToString();
                case "GenderRaceClass":
                    return string.Format("{0} {1} {2} {3}", character.Gender, character.Race.Name, character.Class.Name, character.Level);
                case "Hair":
                    return string.Format("{0} {1}", character.FacialDescription.HairColor, character.FacialDescription.HairStyle);
                case "Height":
                    return character.Size.Height.ToInchesAndFeet();
                case "HitPoints":
                    return character.MaxHitPoints.ToString();
                case "Homeland":
                    return character.History.Homeland.Location;
                case "Initiative":
                    return character.Initiative.ToString();
                case "Languages":
                    return this.GetLanguageList(character);
                case "Mother":
                    return character.History.FamilyTree.Mother;
                case "MovementSpeed":
                    return string.Format("{0} ft ({1} sq)", character.Movement.BaseMovement.TotalValue, character.Movement.BaseSquares);
                case "Name":
                    return character.Name;
                case "OtherGear":
                    return this.GetInventoryList(character.Inventory.All);
                case "Proficiencies":
                    return this.GetProficiencyList(character);
                case "ReflexSave":
                    return character.Defense.ReflexSave.ToString();
                case "Senses":
                    return this.GetSenses(character);
                case "SkillsList":
                    return this.MakeSkillList(character);
                case "Strength":
                case "Dexterity":
                case "Constitution":
                case "Intelligence":
                case "Wisdom":
                case "Charisma":
                    return string.Format(
                        "{0} ({1})", 
                        character.AbilityScores.GetScore(this.property), 
                        character.AbilityScores.GetModifier(this.property).ToModifierString());
                case "WeaponOneType":
                    return character.Offense.Attacks().ToList()[0].Weapon.GetBasicType();
                case "WeaponTwoType":
                    return character.Offense.Attacks().ToList()[1].Weapon.GetBasicType();
                case "WeaponOneInfo":
                    return character.Offense.Attacks().ToList()[0].ToString();
                case "WeaponTwoInfo":
                    return character.Offense.Attacks().ToList()[1].ToString();
                case "Weight":
                    return character.Size.Weight.ToPoundsString();
                case "WillSave":
                    return character.Defense.WillSave.ToString();
            }

            return "NOT FOUND: " + this.property;
        }

        /// <summary>
        /// Formats the skill list into a nice comma delimited string
        /// </summary>
        /// <returns>The skill list.</returns>
        /// <param name="character">Character to parse skills</param>
        private string MakeSkillList(CharacterSheet character)
        {
            return string.Join(
                ",", 
                character.SkillRanks.GetRankedSkills().Select(x => x.ToString()).ToArray<string>());
        }

        /// <summary>
        /// Makes the feat list into a nice comma separated list
        /// </summary>
        /// <returns>The feat list.</returns>
        /// <param name="character">Character to fetch featrs</param>
        private string MakeFeatList(CharacterSheet character)
        {
            return string.Join(
                ",",
                character.Feats.Select(x => x.Name).ToArray<string>());
        }

        /// <summary>
        /// Gets the senses for this character
        /// </summary>
        /// <returns>The senses</returns>
        /// <param name="character">Character to fetch senses</param>
        private string GetSenses(CharacterSheet character)
        {
            var senses = character.GetSpecialAbilities("sense");
            var str = string.Join(",", senses.ToArray());
            return str + " ; " + character.SkillRanks.GetSkill("Perception").ToString();
        }

        /// <summary>
        /// Gets the weapon info.
        /// </summary>
        /// <returns>The weapon info string</returns>
        /// <param name="character">Character to use for stats</param>
        /// <param name="weapon">The weapon to use for determining stats</param>
        private string GetWeaponInfo(CharacterSheet character, Weapon weapon)
        {
            var format = "{0} {1} ({2})";
            if (weapon.Type == WeaponType.Ranged)
            {
                return string.Format(format, weapon.Name, character.Offense.RangeAttackBonus().ToModifierString(), weapon.Damage);
            }
            else
            {
                return string.Format(format, weapon.Name, character.Offense.MeleeAttackBonus().ToModifierString(), weapon.Damage);
            }
        }

        /// <summary>
        /// Gets the language list.
        /// </summary>
        /// <returns>The language list.</returns>
        /// <param name="character">Character to fetch languages from</param>
        private string GetLanguageList(CharacterSheet character)
        {
            return string.Join(",", character.Languages.Select(x => x.Name).ToArray());
        }

        /// <summary>
        /// Gets the inventory list
        /// </summary>
        /// <returns>The list</returns>
        /// <param name="inv">Inventory formatted into string</param>
        private string GetInventoryList(IEnumerable<IEquipment> inv)
        {
            return string.Join(
                ", ", 
                inv.Select(x => x.Name).ToArray());
        }

        /// <summary>
        /// Gets the proficiency list.
        /// </summary>
        /// <returns>The proficiency list.</returns>
        /// <param name="character">Character to fetch proficiencies from</param>
        private string GetProficiencyList(CharacterSheet character)
        {
            var proficiencies = string.Join(
                ", ",
                character.Offense.WeaponProficiencies.Select(x => x.Name).ToArray());

            proficiencies += string.Join(
                ", ",
                character.Defense.ArmorProficiencies.Select(x => x.Name).ToArray());

            return proficiencies;
        }
    }
}