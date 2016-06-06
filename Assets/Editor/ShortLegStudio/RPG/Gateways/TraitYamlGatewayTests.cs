using System;
using System.Linq;
using NUnit.Framework;
using ShortLegStudio;
using ShortLegStudio.RPG.Characters;
using ShortLegStudio.RPG.Gateways;

namespace RPG.Gateways {
	[TestFixture]
	public class TraitYamlGatewayTests {
		Trait darkvision;
		Trait hardy;
		Trait halflingLuck;
		Trait stoneCunning;
		TraitYamlGateway gateway;

		[SetUp]
		public void SetUp() {
			gateway = new TraitYamlGateway(TraitYamlFile.ParseYaml());
			var traits = gateway.All();
			darkvision = traits.First (x => x.Name == "Darkvision");
			hardy = traits.First (x => x.Name == "Hardy");
			halflingLuck = traits.First (x => x.Name == "Halfling Luck");
			stoneCunning = traits.First(x => x.Name == "Stonecunning");
		}

		[Test]
		public void LoadTraitYamlFile() {
			Assert.IsNotNull (darkvision);
			Assert.IsNotNull (hardy);
			Assert.IsNotNull (halflingLuck);
		}

		[Test]
		public void TraitsHaveADescription() {
			Assert.AreEqual ("See in the dark.", darkvision.Description);
			Assert.AreEqual ("Really tough", hardy.Description);
		}

		[Test]
		public void TraitsCanHaveSkillAdjustments() {
			var modifiers = hardy.SkillModifiers;
			Assert.AreEqual (2, modifiers.Count);
			var skillAdj = modifiers.First ();
			Assert.AreEqual ("Heal", skillAdj.SkillName);
			Assert.AreEqual ("Hardy (trait)", skillAdj.Reason);
			Assert.AreEqual (2, skillAdj.Modifier);

			var flyAdj = modifiers.Last ();
			Assert.AreEqual ("Fly", flyAdj.SkillName);
			Assert.AreEqual (4, flyAdj.Modifier);
		}


		private const string TraitYamlFile = @"
- trait: 
  name: Darkvision
  description: See in the dark.
- trait:
  name: Hardy
  description: Really tough
  skillmodifiers:
    - skill: Heal
      amount: 2 
    - skill: Fly
      amount: 4
- trait:
  name: Stonecunning
  description: Work the stone
  conditionalskillmodifiers:
    - skill: Perception
      amount: 2
- trait:
  name: Halfling Luck
  description: Savings throw bonus
...";
	}
}

