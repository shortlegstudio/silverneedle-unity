using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ShortLegStudio.Dice {
	public class Cup {
		private List<Die> _dice;
		public int Modifier { get; set; }

		public Cup() {
			_dice = new List<Die> ();
		}

		public Cup(IList<Die> dice) : this() {
			_dice.AddRange (dice);
		}

		public IList<Die> Dice { get { return _dice; } }

		public void AddDie(Die die) {
			_dice.Add (die);
		}

		public void AddDice(IList<Die> dice) {
			_dice.AddRange (dice);
		}

		public int Roll() {
			int total = 0;
			foreach (Die d in _dice) {
				total += d.Roll ();
			}
			return Modifier + total;
		}

		public int SumTop(int number) {
			return _dice
				.OrderByDescending (d => { return d.LastRoll; })
				.Take(number)
				.Sum(d => { return d.LastRoll; });
		}
	}
}