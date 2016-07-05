using System;
using System.Collections.Generic;
using ShortLegStudio.RPG.Characters;


namespace ShortLegStudio.RPG.Equipment.Gateways {
	public interface IWeaponGateway {
		IEnumerable<Weapon> All();
		IEnumerable<Weapon> FindByProficient(IEnumerable<WeaponProficiency> proficiencies);
	}
}

