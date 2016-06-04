
namespace ShortLegStudio.RPG.Characters {
	public class SkillModifier : BasicStatModifier {
		public string SkillName { get; private set; }
		public SkillModifier(int modifier, string reason, string name) : base(modifier, reason) {
			SkillName = name;
		}
	}
}