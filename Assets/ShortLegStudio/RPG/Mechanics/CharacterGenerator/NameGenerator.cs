using ShortLegStudio;

namespace ShortLegStudio.RPG.Mechanics.CharacterGenerator {
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
			return BuildNameFromSyllables (GetFirstNameSyllables (), Randomly.Range (1, 5));
		}

		public static string CreateLastName() {
			return BuildNameFromSyllables (GetLastNameSyllables (), Randomly.Range (1, 5));
		}

		public static string[] GetFirstNameSyllables() {
			return new string[] {
				"li",
				"pe",
				"le",
				"la",
				"hi",
				"wi",
				"ha",
				"hai",
				"'i",
				"na",
				"ne",
				"hei",
				"lei"
			};
		}

		public static string[] GetLastNameSyllables() {
			return new string[] {
				"li",
				"pe",
				"le",
				"la",
				"hi",
				"wi",
				"ha",
				"hai",
				"'i",
				"na",
				"ne",
				"hei",
				"lei"
			};
		}
	}


}