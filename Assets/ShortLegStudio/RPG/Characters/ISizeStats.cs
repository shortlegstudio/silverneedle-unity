using System;

namespace ShortLegStudio.RPG.Characters {
	public interface ISizeStats {
		CharacterSize Size { get; }
		int SizeModifier { get; }
		int Height { get; }
		int Weight { get; }

		void SetSize(CharacterSize size, int height, int width);
	}
}

