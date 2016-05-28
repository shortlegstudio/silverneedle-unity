using System.Collections.Generic;
using ShortLegStudio.RPG.Characters;
using System;

namespace ShortLegStudio.RPG.Equipment {
	public static class DamageTables {
		public static List<string> TINY_DAMAGE_TABLE = new List<string>
		{ "0", 		"1", 	"1d2", 	"1d3", 	"1d4", 	"1d6", 	"1d8", 	"1d4", 	"1d8", 	"1d10", "2d6" };
		public static List<string> SMALL_DAMAGE_TABLE = new List<string>
		{ "1", 		"1d2", 	"1d3", 	"1d4", 	"1d6", 	"1d8", 	"1d10", "1d6", 	"1d10", "2d6", 	"2d8" };	
		public static List<string> MEDIUM_DAMAGE_TABLE = new List<string>
		{ "1d2", 	"1d3", 	"1d4", 	"1d6", 	"1d8", 	"1d10", "1d12", "2d4", 	"2d6", 	"2d8", 	"2d10" };
		public static List<string> LARGE_DAMAGE_TABLE = new List<string> 
		{ "1d3", 	"1d4", 	"1d6", 	"1d8", 	"2d6", 	"2d8", 	"3d6", 	"2d6", 	"3d6", 	"3d8", 	"4d8" };


		public static string ConvertDamageBySize(string mediumDamageAmount, CharacterSize size) {
			int index = MEDIUM_DAMAGE_TABLE.IndexOf (mediumDamageAmount);
			switch (size) {
			case CharacterSize.Tiny:
				return TINY_DAMAGE_TABLE [index];
			case CharacterSize.Small:
				return SMALL_DAMAGE_TABLE [index];
			case CharacterSize.Medium:
				return mediumDamageAmount;
			case CharacterSize.Large:
				return LARGE_DAMAGE_TABLE [index];
			}
			//Get Index for medium damage
			throw new NotImplementedException(string.Format("Character Size: {0} has not been implemented in damage tables.", size));
		}

	}
}

