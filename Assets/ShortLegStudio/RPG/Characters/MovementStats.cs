using System;

namespace ShortLegStudio.RPG.Characters {
	public class MovementStats : IStatTracker {
		const int SQUARE_SIZE = 5;

		public MovementStats() : this(30) { }

		public MovementStats(int baseSpeed) {
			BaseMovement = new BasicStat(baseSpeed);
		}

		public int BaseSquares { 
			get { return BaseMovement.TotalValue / SQUARE_SIZE; }
		}

		public BasicStat BaseMovement { get; private set; }
		public void ProcessModifier(IModifiesStats modifier) {
			throw new NotImplementedException();
		}

		public void SetBaseSpeed(int spd) {
			BaseMovement.SetValue(spd);
		}
	}
}

