using System.Collections.Generic;

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

	}
}

