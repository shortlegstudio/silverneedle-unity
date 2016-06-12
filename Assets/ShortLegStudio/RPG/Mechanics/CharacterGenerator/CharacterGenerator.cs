using System.Linq;
using ShortLegStudio.RPG.Characters;
using ShortLegStudio.RPG.Mechanics.CharacterGenerator.Abilities;
using ShortLegStudio.RPG.Equipment.Gateways;
using ShortLegStudio.RPG.Gateways;
using ShortLegStudio.Enchilada;
using System.Collections.Generic;
using ShortLegStudio.RPG.Equipment;

namespace ShortLegStudio.RPG.Mechanics.CharacterGenerator {
	public class CharacterGenerator {
		private IAbilityScoreGenerator abilityGenerator;
		private LanguageSelector languageSelector;
		private RaceSelector raceSelector;
		private NameGenerator nameGenerator;

		private IArmorGateway armorGateway;
		private EntityGateway<Weapon> weaponGateway;
		private EntityGateway<Skill> skillGateway;
		private EntityGateway<Class> classGateway;

		public CharacterGenerator(
			IAbilityScoreGenerator abilities,
			LanguageSelector langs,
			RaceSelector races,
			NameGenerator names) {
			abilityGenerator = abilities;
			languageSelector = langs;
			raceSelector = races;
			nameGenerator = names;

			armorGateway = new ArmorYamlGateway();
			weaponGateway = new WeaponYamlGateway();
			skillGateway = new SkillYamlGateway();
		}


		public CharacterSheet CreateLevel0() {
			var character = new CharacterSheet (skillGateway.All());

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
			var equip = new EquipMeleeAndRangedWeapon(weaponGateway);
			equip.AssignWeapons(character.Inventory);


			var equipArmor = new PurchaseInitialArmor (armorGateway);
			equipArmor.PurchaseArmorAndShield (character.Inventory);

			return character;
		}

	}

}

