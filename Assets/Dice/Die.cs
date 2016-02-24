using System.Collections;

namespace ShortLegStudio.Dice {
	public class Die  {
		private DiceSides _sides;
		private int _lastRoll = -1;
		private int _dieMaxValue = 0;

		public Die(DiceSides sides) {
			_sides = sides;
			_dieMaxValue = (int)_sides + 1; //Max value is exclusive
		}

		public int LastRoll { get { return _lastRoll; } }
		public int Roll() {
			_lastRoll = UnityEngine.Random.Range(1, _dieMaxValue); 
			return _lastRoll;
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


	}
}
