using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using ShortLegStudio.RPG.Characters;
using ShortLegStudio.RPG.Characters.Generators;
using System.Collections.Generic;
using System.Linq;

[TestFixture]
public class SkillRanksTests {
	List<Skill> _skillList;
	AbilityScores _abilityScores;

	SkillRanks Subject;

	[SetUp]
	public void SetupCharacter() {
		_skillList = new List<Skill> ();
		_skillList.Add (new Skill ("Climb", AbilityScoreTypes.Strength, false));
		_skillList.Add (new Skill ("Disable Device", AbilityScoreTypes.Dexterity, true));

		_abilityScores = new AbilityScores ();
		_abilityScores.SetScore (AbilityScoreTypes.Strength, 14);
		_abilityScores.SetScore (AbilityScoreTypes.Dexterity, 12);

		Subject = new SkillRanks (_skillList, _abilityScores);

	}

	[Test]
	public void SkillRanksLoadsAllTheSkills() {
		Assert.AreEqual (2, Subject.GetScore ("Climb"));
		Assert.AreEqual (int.MinValue, Subject.GetScore ("Disable Device"));
	}

	[Test]
	public void CanProcessASkillModifierForModifyingSkills() {
		Subject.ProcessModifier (new MockMod ());
		Assert.AreEqual (5, Subject.GetScore ("Climb"));
	}

	class MockMod : ISkillModifier {
		public IList<SkillAdjustment> SkillModifiers { get; set;  }

		public MockMod() {
			SkillModifiers = new List<SkillAdjustment>();
			SkillModifiers.Add(new SkillAdjustment("Cause", 3, "Climb"));
		}
	}
}
