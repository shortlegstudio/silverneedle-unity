using UnityEngine;
using System.Linq;
using System.Collections;
using ShortLegStudio;

namespace ShortLegStudio.Dice {
	public class Die  {
		public DiceSides Sides { get; private set; }
		private int _lastRoll = -1;
		private int _dieMaxValue = 0;

		public Die(DiceSides sides) {
			Sides = sides;
			_dieMaxValue = (int)Sides + 1; //Max value is exclusive
		}

		public int LastRoll { get { return _lastRoll; } }
		public int Roll() {
			_lastRoll = Randomly.Range(1, _dieMaxValue); 
			return _lastRoll;
		}

		public int SideCount() {
			return (int)Sides;
		}

		public static Die[] GetDice(DiceSides sides, int count) {
			var result = new Die[count];
			for (int i = 0; i < count; i++)
				result [i] = new Die (sides);
			return result;
		}

		public static Die d4() {
			return new Die (DiceSides.d4);
		}

		public static Die d6() {
			return new Die (DiceSides.d6);
		}

		public static Die d8() {
			return new Die (DiceSides.d8);
		}

		public static Die d10() {
			return new Die (DiceSides.d10);
		}

		public static Die d12() {
			return new Die (DiceSides.d12);
		}

		public static Die d20() {
			return new Die (DiceSides.d20);
		}

		public static Die d100() {
			return new Die (DiceSides.d100);
		}

		public override string ToString ()
		{
			return string.Format ("[Die: Sides={0}, LastRoll={1}]", Sides, LastRoll);
		}

		public override bool Equals (object obj)
		{
			var die = obj as Die;
			if (die == null)
				return false;

			if (die.Sides != this.Sides)
				return false;

			return die.LastRoll == this.LastRoll;
		}

		public override int GetHashCode ()
		{
			return base.GetHashCode ();
		}
	}
}
