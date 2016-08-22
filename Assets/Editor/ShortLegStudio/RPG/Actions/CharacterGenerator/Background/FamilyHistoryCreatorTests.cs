// //-----------------------------------------------------------------------
// // <copyright file="FamilyTreeTests.cs" company="Short Leg Studio, LLC">
// //     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------
using System;
using NUnit.Framework;
using ShortLegStudio.RPG.Characters.Background;
using ShortLegStudio.RPG.Actions.NamingThings;


namespace RPG.Characters.Background 
{
    [TestFixture]
    public class FamilyHistoryCreatorTests
    {
        [Test]
        public void CanCreateFamilyTreeWithParents()
        {
            var generator = new FamilyHistoryCreator(new NameTestBuilder());
            var familyTree = generator.CreateFamilyTree("Human");

            Assert.IsNotNullOrEmpty(familyTree.Father);
            Assert.IsNotNullOrEmpty(familyTree.Mother);
        }

        private class NameTestBuilder : INameCharacter
        {
            public string CreateFullName(ShortLegStudio.RPG.Characters.Gender gender, string race)
            {
                return "Lucille";
            }
        }
    }
}

