using System.Collections.Generic;

namespace ShortLegStudio.Enchilada {
	/// <summary>
	/// EntityGateway defines the access methodology for getting entities into the mechanic engine
	/// </summary>
	public interface EntityGateway<T> {
		IEnumerable<T> All();
	}
}

