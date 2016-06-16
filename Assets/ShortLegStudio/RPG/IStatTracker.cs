using System;
using ShortLegStudio.RPG.Characters;

namespace ShortLegStudio.RPG {
	public interface IStatTracker {
		void ProcessModifier(IModifiesStats modifier);
	}
}

