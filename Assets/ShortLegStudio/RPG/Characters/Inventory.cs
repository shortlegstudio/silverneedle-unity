using System;
using ShortLegStudio.RPG.Equipment;
using System.Collections.Generic;
using System.Linq;

namespace ShortLegStudio.RPG.Characters {
	public class Inventory {
		public IEnumerable<IEquipment> All { get { return _inv; } }
		public IEnumerable<Weapon> Weapons { get { return _inv.OfType<Weapon>(); } }

		private IList<IEquipment> _inv;

		public Inventory () {
			_inv = new List<IEquipment> ();	
		}

		public void AddItem(IEquipment equip) {
			_inv.Add (equip);
		}
	}
}

