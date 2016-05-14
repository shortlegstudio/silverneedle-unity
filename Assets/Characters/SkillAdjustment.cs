using UnityEngine;
using YamlDotNet.RepresentationModel;
using System.Collections;
using System.Collections.Generic;
using ShortLegStudio;

namespace ShortLegStudio.RPG.Characters {
	public class SkillAdjustment : BasicStatAdjustment {
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