using ShortLegStudio.RPG.Characters;


namespace ShortLegStudio.RPG.Mechanics.CharacterGenerator {
	public class LevelUpGenerator  {
		HitPointGenerator HPGenerator;

		public LevelUpGenerator(HitPointGenerator gen) {
			HPGenerator = gen;
		}

		public void BringCharacterToLevel(CharacterSheet character, int targetLevel) {
			for (int i = character.Level; i <= targetLevel; i++) {
				LevelUp(character);
			}
		}

		public void LevelUp(CharacterSheet character) {
			character.SetLevel (character.Level + 1);
			var incrementHitpoints = HPGenerator.RollLevelUp (character);
			character.MaxHitPoints += incrementHitpoints;
			character.CurrentHitPoints += incrementHitpoints;
			character.Defense.LevelUpDefenseStats (character.Class);

			//Special Level ups
			if (character.Level % 4 == 0) {
				AssignAbilityPoints (character);
			}
		}

		private void AssignAbilityPoints(CharacterSheet character) {
			var ability = EnumHelpers.ChooseOne<AbilityScoreTypes>();
			var adjust = new AbilityScoreAdjustment();
			adjust.Reason = "Level Up";
			adjust.Modifier = 1;
			adjust.ability = ability;

			character.Abilities.GetAbility(ability).AddAdjustment(adjust);
		}
	}
}
