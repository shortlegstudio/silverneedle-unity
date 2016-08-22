// //-----------------------------------------------------------------------
// // <copyright file="IHomelandGateway.cs" company="Short Leg Studio, LLC">
// //     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------
using System;
using System.Collections.Generic;
using ShortLegStudio.RPG.Utility;

namespace ShortLegStudio.RPG.Characters.Background.Gateways
{
    public interface IHomelandGateway
    {
        WeightedOptionTable<Homeland> GetRacialOptions(string race);
    }
}

