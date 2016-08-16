// //-----------------------------------------------------------------------
// // <copyright file="NameCharacter.cs" company="Short Leg Studio, LLC">
// //     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------
using ShortLegStudio.RPG.Names.Gateways;
using ShortLegStudio.RPG.Characters;

namespace ShortLegStudio.RPG.Actions.NamingThings
{
    using System;

    public class NameCharacter : INameCharacter
    {
        private ICharacterNamesGateway namesGateway;

        public NameCharacter(ICharacterNamesGateway namesGateway)
        {
            this.namesGateway = namesGateway;    
        }

        public string CreateFullName(Gender gender, string race)
        {
            var firstName = namesGateway.GetFirstNames(gender, race).ChooseOne();
            var lastName = namesGateway.GetLastNames(race).ChooseOne();

            return string.Format("{0} {1}", firstName, lastName);
        }
    }
}

