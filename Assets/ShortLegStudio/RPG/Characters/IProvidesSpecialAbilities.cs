// //-----------------------------------------------------------------------
// // <copyright file="ISpecialAbility.cs" company="Short Leg Studio, LLC">
// //     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace ShortLegStudio.RPG.Characters
{
    public interface IProvidesSpecialAbilities
    {
        IList<SpecialAbility> SpecialAbilities { get; }
    }
}

