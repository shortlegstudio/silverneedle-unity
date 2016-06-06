using System;
using System.Collections.Generic;

namespace ShortLegStudio.RPG.Characters {
	public interface IModifiesStats {
		IList<BasicStatModifier> Modifiers { get; }
	}
}

