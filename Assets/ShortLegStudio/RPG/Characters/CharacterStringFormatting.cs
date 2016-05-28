using System;
using System.Collections;

namespace ShortLegStudio.RPG.Characters
{
	public static class CharacterStringFormatting
	{
		public static string ShortString(this CharacterAlignment align) {
			switch (align) {
			case CharacterAlignment.LawfulGood:
				return "LG";
			case CharacterAlignment.NeutralGood:
				return "NG";
			case CharacterAlignment.ChaoticGood:
				return "CG";
			case CharacterAlignment.LawfulNeutral:
				return "LN";
			case CharacterAlignment.Neutral:
				return "N";
			case CharacterAlignment.ChaoticNeutral:
				return "CN";
			case CharacterAlignment.LawfulEvil:
				return "LE";
			case CharacterAlignment.NeutralEvil:
				return "NE";
			case CharacterAlignment.ChaoticEvil:
				return "CE";
			}

			return "??";
		}

		public static string ToModifierString(this int value) {
			if (value >= 0)
				return string.Format ("+{0}", value);

			return value.ToString ();
		}
	}
}

