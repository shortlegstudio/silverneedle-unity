using System;
using NUnit.Framework;
using ShortLegStudio.RPG.Characters;
using ShortLegStudio.Dice;
using System.Linq;
using ShortLegStudio;
using System.Collections.Generic;
using ShortLegStudio.RPG.Mechanics.CharacterGenerator;
using ShortLegStudio.RPG.Actions.CharacterGenerator;

namespace RPG.Mechanics.CharacterGenerator {
	[TestFixture]
	public class AssignAgeTests {
        [Test]
        public void AssignsAnAgeToACharacterBasedOnClassAndMaturity()
        {
            var cls = ClassDevelopmentAge.Young;
            var maturity = new Maturity();
            maturity.Adulthood = 15;
            maturity.Young = DiceStrings.ParseDice("1d4");

            var assignAges = new AssignAge();
            var age = assignAges.RandomAge(cls, maturity);
            Assert.GreaterOrEqual(age, 16);
            Assert.LessOrEqual(age, 19);
        }
	}
}

