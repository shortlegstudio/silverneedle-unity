using System;
using System.Collections.Generic;
using System.Collections;
using System.Reflection;

namespace ShortLegStudio.RPG.Characters {
	public class Language {
		
		public string Name { get; set; }
		public string Description { get; set; }

		public Language () { }
		public Language(string name, string desc) {
			Name = name;
			Description = desc;
		}


	}
}

