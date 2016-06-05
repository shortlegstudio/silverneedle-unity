using System;
using System.Collections.Generic;

namespace ShortLegStudio.RPG.Characters {
	public interface ITraitGateway {
		IEnumerable<Trait> All();
	}
}

