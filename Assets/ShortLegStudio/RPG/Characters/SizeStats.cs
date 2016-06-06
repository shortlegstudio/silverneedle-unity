using System;
using System.Collections.Generic;

namespace ShortLegStudio.RPG.Characters {
	public class SizeStats : ISizeStats, IModifiesSkills {
		const int STEALTH_MODIFIER = 4;
		const int FLY_MODIFIER = 2;

		public CharacterSize Size { get; private set; }
		public int Height { get; private set; }
		public int Weight { get; private set; }
		public int SizeModifier { get; private set; }
		public System.Collections.Generic.IList<SkillModifier> SkillModifiers { get; private set; }
		public IList<ConditionalSkillModifier> ConditionalModifiers { get { return new List<ConditionalSkillModifier>(); } }

		//Accessories to common attributes modified
		private SkillModifier StealthAdj { get; set; }
		private SkillModifier FlyAdj { get; set; }


		public SizeStats() : this(CharacterSize.Medium, 72, 180) { }
		public SizeStats(CharacterSize size) : this(size, 72, 180) { }
		public SizeStats (CharacterSize size, int height, int weight) {
			SetupSkillModifiers ();
			SetSize (size, height, weight);
		}

		public void SetSize (CharacterSize size, int height, int weight)
		{
			Size = size;
			Height = height;
			Weight = weight;
			SizeModifier = (int)size;

			UpdateStealth ();
			UpdateFly ();
		}


		private void SetupSkillModifiers() {
			StealthAdj = new SkillModifier (0, "Size", "Stealth");
			FlyAdj = new SkillModifier (0, "Size", "Fly");

			SkillModifiers = new List<SkillModifier> ();
			SkillModifiers.Add (StealthAdj);
			SkillModifiers.Add (FlyAdj);
		}

		private void UpdateStealth() {
			//Small = 4, Tiny = 8, Diminuitive = 12, fine = 16
			//Large = -4....
			StealthAdj.Modifier = GetSkillMultiplier() * STEALTH_MODIFIER;
		}

		private void UpdateFly() {
			FlyAdj.Modifier = GetSkillMultiplier () * FLY_MODIFIER;
		}

		private int GetSkillMultiplier() {
			//8 == 4
			//4 == 3
			//2 == 2
			//1 == 1
			int mod = (int)Size;
			if (Math.Abs (mod) == 8) {
				mod = 4 * Math.Sign (mod);
			} else if (Math.Abs (mod) == 4) {
				mod = 3 * Math.Sign (mod);
			}

			return mod;
		}
	}
}

