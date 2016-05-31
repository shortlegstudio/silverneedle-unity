using System;
using ShortLegStudio.RPG.Equipment;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using UnityEditorInternal.VersionControl;
using YamlDotNet.Core.Tokens;

namespace ShortLegStudio.RPG.Characters {
	public class Inventory {
		public IEnumerable<IEquipment> All { get { return _inv; } }
		public IEnumerable<Weapon> Weapons { get { return _inv.OfType<Weapon>(); } }
		public IEnumerable<Armor> Armor { get { return _inv.OfType<Armor>(); } }
		public IEnumerable<IEquipment> EquippedItems { get { return _equippedItems; } }
		private IList<IEquipment> _inv;
		private IList<IEquipment> _equippedItems;

		public Inventory () {
			_inv = new List<IEquipment> ();	
			_equippedItems = new List<IEquipment> ();
		}

		public void AddItem(IEquipment equip) {
			if (!_inv.Contains (equip)) {
				_inv.Add (equip);
			}
		}

		public void EquipItem(IEquipment item) {
			AddItem (item);
			_equippedItems.Add (item);
		}

		public IEnumerable<T> OfType<T>() {
			return _inv.OfType<T> ();
		}
	}
}

