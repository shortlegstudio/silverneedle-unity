using System.Security.Cryptography.X509Certificates;


namespace ShortLegStudio.RPG.Characters.Skills {
	public class SkillAdjustment : BasicStatModifier {
		public string SkillName { get; private set; }
		public SkillAdjustment(int modifier, string reason, string name) : base(modifier, reason) {
			SkillName = name;
		}
	}
}