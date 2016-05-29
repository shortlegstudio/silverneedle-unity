using System.Linq;
using ShortLegStudio.RPG.Characters;
using System.Collections.Generic;
using ShortLegStudio.Enchilada;

namespace ShortLegStudio.RPG.Mechanics.CharacterGenerator {
	public class LanguageSelector {
		private EntityGateway<Language> Languages;

		public LanguageSelector(EntityGateway<Language> languages) {
			Languages = languages;
		}

		public IEnumerable<Language> PickLanguage(Race race, int bonusLanguages) {
			var result = new List<Language> ();

			//Assign Known Languages
			foreach (var l in race.KnownLanguages) {
				result.Add (Languages.All().First (x => x.Name == l));
			}

			for (var i = 0; i < bonusLanguages; i++) {
				var available = Languages.All().Where (
					x => !result.Any(r => r.Name == x.Name) && race.AvailableLanguages.Any(avail => x.Name == avail)
				);
				if (available.Count () > 0) {
					var language = available.ToList ().ChooseOne ();
					result.Add (language);
				}
			}
			return result;
		}
	}
}

