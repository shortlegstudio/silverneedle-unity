using System;
using System.Collections.Generic;

namespace ShortLegStudio.RPG.Equipment.Gateways {
	public interface IArmorGateway {
		Armor GetByName(string name);
		IEnumerable<Armor> FindByArmorType(ArmorType type);
	}
}

