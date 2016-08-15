// //-----------------------------------------------------------------------
// // <copyright file="NameCharacter.cs" company="Short Leg Studio, LLC">
// //     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------
using ShortLegStudio.RPG.Names.Gateways;

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

        public string CreateFullName()
        {
            // Choose a first name 
            var firstName = namesGateway.GetFirstNames().ChooseOne();
            var lastName = namesGateway.GetLastNames().ChooseOne();

            return string.Format("{0} {1}", firstName, lastName);
        }
    }
}

