// //-----------------------------------------------------------------------
// // <copyright file="ICharacterNamesGateway.cs" company="Short Leg Studio, LLC">
// //     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------
using System.Collections;

namespace ShortLegStudio.RPG.Names.Gateways
{
    using System;
    using System.Collections.Generic;

    public interface ICharacterNamesGateway
    {
        IList<string> GetFirstNames();
        IList<string> GetLastNames();

    }
}

