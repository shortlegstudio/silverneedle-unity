using System;

namespace ShortLegStudio.RPG.Characters {
	public interface ISizeStats {
		CharacterSize Size { get; }
		int SizeModifier { get; }
		float Height { get; }
		float Weight { get; }

		void SetSize(CharacterSize size, float height, float width);
	}
}

