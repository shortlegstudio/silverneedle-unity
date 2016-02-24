using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ShortLegStudio.Dice {
	public class Cup {
		private IList<Die> _dice;

		public Cup() {
			_dice = new List<Die> ();
		}

		public IList<Die> Dice { get { return _dice; } }

		public void AddDie(Die die) {
			_dice.Add (die);
		}

		public int Roll() {
			int total = 0;
			foreach (Die d in _dice) {
				total += d.Roll ();
			}
			return total;
		}

		public int TakeTop(int number) {
			return _dice
				.OrderByDescending (d => { return d.LastRoll; })
				.Take(number)
				.Sum(d => { return d.LastRoll; });
		}
	}
}