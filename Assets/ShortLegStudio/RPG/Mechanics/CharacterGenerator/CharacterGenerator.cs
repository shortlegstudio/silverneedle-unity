using System.Linq;
using ShortLegStudio.RPG.Characters;
using ShortLegStudio.RPG.Repositories;
using ShortLegStudio.RPG.Mechanics.CharacterGenerator.Abilities;

namespace ShortLegStudio.RPG.Mechanics.CharacterGenerator {
	public class CharacterGenerator {
		private IAbilityScoreGenerator abilityGenerator;
		private LanguageSelector languageSelector;
		private RaceYamlRepository raceRepo;
		private NameGenerator nameGenerator;

		public CharacterGenerator(
			IAbilityScoreGenerator abilities,
			LanguageSelector langs,
			RaceYamlRepository races,
			NameGenerator names) {
			abilityGenerator = abilities;
			languageSelector = langs;
			raceRepo = races;
			nameGenerator = names;
		}


		public CharacterSheet CreateLevel0() {
			var repo = new SkillYamlRepository ();
			var character = new CharacterSheet (repo.All());

			character.Name = nameGenerator.CreateFullName ();
			character.Gender = EnumHelpers.ChooseOne<Gender> ();
			character.Alignment = EnumHelpers.ChooseOne<CharacterAlignment>();
			character.Abilities = abilityGenerator.Get ();
			character.SetRace(raceRepo.All ().ToList().ChooseOne ());

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
			var equip = new EquipMeleeAndRangedWeapon(new WeaponYamlRepository());
			equip.AssignWeapons(character.Inventory);
			return character;
		}

	}
}

