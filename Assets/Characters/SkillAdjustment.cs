using UnityEngine;
using YamlDotNet.RepresentationModel;
using System.Collections;
using System.Collections.Generic;
using ShortLegStudio;

namespace ShortLegStudio.RPG.Characters {
	public struct SkillAdjustment {
		public string Reason { get ; set; }
		public int Amount { get; set; }
		public string SkillName { get; set; }

		public SkillAdjustment(
			string reason,
			int amount,
			string skill) {
			Reason = reason;
			Amount = amount;
			SkillName = skill;
		}
	}
}