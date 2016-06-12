using System;
using System.Collections.Generic;

namespace ShortLegStudio.RPG.Gateways {
	public static class ParseStatModifiersYaml {
		public static IList<BasicStatModifier> ParseYaml(YamlNodeWrapper modifierNode, string source) {
			IList<BasicStatModifier> modifiers = new List<BasicStatModifier>();

			foreach (var mod in modifierNode.Children()) {
				var statName = mod.GetString("stat");
				var amount = mod.GetInteger ("modifier");
				var type = mod.GetString("type");
				var condition = mod.GetStringOptional("condition");
				BasicStatModifier modifier;

				if (!string.IsNullOrEmpty(condition)) {
					modifier = new ConditionalStatModifier(
						condition,
						statName,
						amount,
						type,
						source
					);
				} else {
					modifier = new BasicStatModifier(
						statName,
						amount,
						type,
						source
					);
				}
				modifiers.Add(modifier);
			}

			return modifiers;
		}
	}
}

