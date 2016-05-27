using System;
using System.Linq;
using ShortLegStudio.RPG.Characters;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using ShortLegStudio;

namespace ShortLegStudio.RPG.Characters.Generators
{
	public static class LanguagePicker
	{
		public static IEnumerable<Language> PickLanguage(Race race, IEnumerable<Language> languages, int bonusLanguages) {
			var result = new List<Language> ();

			//Assign Known Languages
			foreach (var l in race.KnownLanguages) {
				result.Add (languages.First (x => x.Name == l));
			}

			for (var i = 0; i < bonusLanguages; i++) {
				var available = languages.Where (x => !result.Any (r => r == x) && race.AvailableLanguages.Any(avail => x.Name == avail));
				if (available.Count () > 0) {
					var language = available.ToList ().ChooseOne ();
					result.Add (language);
				}
			}
			return result;
		}
	}
}

