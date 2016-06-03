
namespace ShortLegStudio.RPG.Characters {
	public class SkillAdjustment : BasicStatModifier {
		public string SkillName { get; set; }

		public SkillAdjustment(
			string reason,
			int modifier,
			string skill) {
			Reason = reason;
			Modifier = modifier;
			SkillName = skill;
		}
	}
}