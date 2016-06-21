using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.CodeDom.Compiler;
using System.Text;
using System.Text.RegularExpressions;

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

		public override string ToString() {
			var diceGroups = Dice.GroupBy(die => die.Sides)
				.Select(group => new { 
					Sides = group.Key,
					Count = group.Count()
				});
			var result = new StringBuilder();
			foreach (var d in diceGroups) {
				if (result.Length == 0) {
					result.AppendFormat("{0}d{1}", d.Count, (int)d.Sides);
				}
				else {
					result.AppendFormat("+{0}d{1}", d.Count, (int)d.Sides);
				}

			}

			if (Modifier > 0) {
				result.AppendFormat("+{0}", Modifier);
			}
			else if (Modifier < 0) {
				result.Append(Modifier);
			}
			return result.ToString();
		}
	}
}