// //-----------------------------------------------------------------------
// // <copyright file="IClassOriginYamlGateway.cs" company="Short Leg Studio, LLC">
// //     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------
using System;

namespace ShortLegStudio.RPG.Characters.Background
{
    public interface IClassOriginYamlGateway
    {
        WeightedOptionTable<ClassOrigin> GetClassOriginOptions(string cls);
        ClassOrigin ChooseOne(string cls);
    }
}

