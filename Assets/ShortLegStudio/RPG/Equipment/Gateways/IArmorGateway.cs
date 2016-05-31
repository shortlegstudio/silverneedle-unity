using System;
using System.Collections.Generic;
using System.Security.Policy;

namespace ShortLegStudio.RPG.Equipment.Gateways {
	public interface IArmorGateway {
		IEnumerable<Armor> All();
		Armor GetByName(string name);
		IEnumerable<Armor> FindByArmorType(ArmorType type);
		IEnumerable<Armor> FindByArmorTypes (params ArmorType[] types);
			
	}
}

