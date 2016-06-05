using System.Linq;
using ShortLegStudio.RPG.Characters;
using ShortLegStudio.RPG.Mechanics.CharacterGenerator.Abilities;
using ShortLegStudio.RPG.Equipment.Gateways;
using ShortLegStudio.RPG.Gateways;
using ShortLegStudio.Enchilada;
using System.Collections.Generic;

namespace ShortLegStudio.RPG.Mechanics.CharacterGenerator {
	public class CharacterGenerator {
		private IAbilityScoreGenerator abilityGenerator;
		private LanguageSelector languageSelector;
		private RaceSelector raceSelector;
		private NameGenerator nameGenerator;

		public CharacterGenerator(
			IAbilityScoreGenerator abilities,
			LanguageSelector langs,
			RaceSelector races,
			NameGenerator names) {
			abilityGenerator = abilities;
			languageSelector = langs;
			raceSelector = races;
			nameGenerator = names;
		}


		public CharacterSheet CreateLevel0() {
			var repo = new SkillYamlGateway ();
			var character = new CharacterSheet (repo.All());

			character.Name = nameGenerator.CreateFullName ();
			character.Gender = EnumHelpers.ChooseOne<Gender> ();
			character.Alignment = EnumHelpers.ChooseOne<CharacterAlignment>();
			abilityGenerator.AssignAbilities (character.Abilities);
			raceSelector.ChooseRace(character);

			character.Languages.Add (
				languageSelector.PickLanguage (
					character.Race, 
					character.Abilities.GetModifier (AbilityScoreTypes.Intelligence)
				)
			);
			return character;
		}

		public CharacterSheet SelectClass(CharacterSheet character) {
			character.SetClass (Class.GetClasses ().ChooseOne ());
			var hp = new HitPointGenerator ();
			character.SetHitPoints (hp.RollHitPoints (character));
			return character;
		}

		public CharacterSheet GenerateRandomCharacter() {
			var skillGen = new SkillPointGenerator ();
			var levelUpGen = new LevelUpGenerator (new HitPointGenerator());
			var character = CreateLevel0 ();
			SelectClass (character);
			character.AddFeat (Feat.GetQualifyingFeats (character).ToList ().ChooseOne ());

			levelUpGen.BringCharacterToLevel(character, UnityEngine.Random.Range (1, 21));

			//Assign Skill Points
			skillGen.AssignSkillPointsRandomly(character);

			//Get some gear!
			var equip = new EquipMeleeAndRangedWeapon(new WeaponYamlGateway());
			equip.AssignWeapons(character.Inventory);


			var equipArmor = new PurchaseInitialArmor (new ArmorYamlGateway ());
			equipArmor.PurchaseArmorAndShield (character.Inventory);

			return character;
		}

	}

}

