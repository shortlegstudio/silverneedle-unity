
namespace ShortLegStudio.RPG.Characters {
	public class SkillAdjustment : BasicStatModifier {
		public string SkillName { get; private set; }
		public SkillAdjustment(int modifier, string reason, string name) : base(modifier, reason) {
			SkillName = name;
		}
	}
}