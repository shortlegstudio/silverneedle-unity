using System;
using System.Linq;
using System.Collections.Generic;
using ShortLegStudio.RPG.Characters;
using ShortLegStudio.Enchilada;

namespace ShortLegStudio.RPG.Mechanics.CharacterGenerator {
	public class RaceSelector {
		EntityGateway<Trait> _traitGateway;
		EntityGateway<Race> _racesGateway;

		public RaceSelector( EntityGateway<Race> races, EntityGateway<Trait> traitGateway) {
			_traitGateway = traitGateway;
			_racesGateway = races;
		}

		public void ChooseRace(CharacterSheet sheet) {
			SetRace(sheet, _racesGateway.All().ToList().ChooseOne());
		}

		public void SetRace(CharacterSheet sheet, Race race) {
			sheet.SetRace(race);	
			SetSpeedForRace(sheet, race);
			SetTraitsForRace(sheet, race);
			SetAbilityScoresForRace(sheet.Abilities, race);
			SetSizeForRace(sheet.Size, race);
			sheet.NotifyModified();
		}

		private void SetSpeedForRace(CharacterSheet sheet, Race race) {
			//Update Speed
			sheet.Movement.SetBaseSpeed(race.BaseMovementSpeed);
		}

		private void SetTraitsForRace(CharacterSheet sheet, Race race) {
			//Add Traits
			foreach (var trait in race.Traits) {
				var t = _traitGateway.All().First(x => x.Name == trait);
				sheet.AddTrait (t, false);
			}
		}

		private void SetAbilityScoresForRace(AbilityScores abilities, Race race) {
			//Add Ability Modifiers
			foreach (var adj in race.AbilityModifiers) {
				if (adj.RacialChose) {
					var ability = EnumHelpers.ChooseOne<AbilityScoreTypes> ();
					var a = abilities.GetAbility (ability);
					a.AddModifier (adj);
				} else {
					var a = abilities.GetAbility (adj.ability);
					a.AddModifier (adj);
				}
			}
		}

		private void SetSizeForRace(ISizeStats size, Race race) {

			//Update Size
			size.SetSize(race.SizeSetting, race.HeightRange.Roll(), race.WeightRange.Roll());

		}
	}
}

