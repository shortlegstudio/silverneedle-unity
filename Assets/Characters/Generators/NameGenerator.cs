using UnityEngine;
using System.Collections;
using System.Linq;
using ShortLegStudio;

namespace ShortLegStudio.RPG.Characters.Generators {
	public static class NameGenerator {
		public static string CreateFullName() {
			return string.Format ("{0} {1}", CreateFirstName (), CreateLastName ());
		}


		public static string BuildNameFromSyllables(string[] syllables, int count) {
			string name = "";

			for (var i = 0; i < count; i++) {
				name += syllables.ChooseOne ();	
			}
			return name.Capitalize ();
		}

		public static string CreateFirstName() {
			return BuildNameFromSyllables (GetFirstNameSyllables (), Random.Range (1, 5));
		}

		public static string CreateLastName() {
			return BuildNameFromSyllables (GetLastNameSyllables (), Random.Range (1, 5));
		}

		public static string[] GetFirstNameSyllables() {
			return new string[] {
				"zag",
				"shi",
				"flo",
				"sklar",
				"mon",
				"iven",
				"clar"
			};
		}

		public static string[] GetLastNameSyllables() {
			return new string[] {
				"lor",
				"cho",
				"mog",
				"ogg",
				"lok",
				"fix",
				"vet",
				"hent"
			};
		}
	}


}