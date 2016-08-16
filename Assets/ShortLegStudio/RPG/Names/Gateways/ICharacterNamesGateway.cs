// //-----------------------------------------------------------------------
// // <copyright file="ICharacterNamesGateway.cs" company="Short Leg Studio, LLC">
// //     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------

namespace ShortLegStudio.RPG.Names.Gateways
{
    using System;
    using System.Collections.Generic;
    using ShortLegStudio.RPG.Characters;

    public interface ICharacterNamesGateway
    {
        IList<string> GetFirstNames();
        IList<string> GetFirstNames(Gender gender, string race);
        IList<string> GetLastNames();
        IList<string> GetLastNames(string race);

    }
}

