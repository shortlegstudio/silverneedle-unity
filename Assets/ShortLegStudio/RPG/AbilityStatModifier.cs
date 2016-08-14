//-----------------------------------------------------------------------
// <copyright file="BasicStatModifier.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using ShortLegStudio.RPG.Characters;
using System;


namespace ShortLegStudio.RPG
{
    public class AbilityStatModifier : BasicStatModifier
    {
        private AbilityScore abilityScore;

        public AbilityStatModifier(AbilityScore ability)
        {
            abilityScore = ability;
        }

        public override float Modifier
        {
            get
            {
                return abilityScore.TotalModifier;
            }
            set
            {
                throw new InvalidOperationException("Cannot set the modifier for an ability modifier");
            }
        }
       
    }
}