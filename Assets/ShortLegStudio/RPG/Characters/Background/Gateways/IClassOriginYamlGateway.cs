// //-----------------------------------------------------------------------
// // <copyright file="IClassOriginYamlGateway.cs" company="Short Leg Studio, LLC">
// //     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------
using System;
using ShortLegStudio.RPG.Utility;

namespace ShortLegStudio.RPG.Characters.Background.Gateways
{
    public interface IClassOriginYamlGateway
    {
        WeightedOptionTable<ClassOrigin> GetClassOriginOptions(string cls);
        ClassOrigin ChooseOne(string cls);
    }
}

