// //-----------------------------------------------------------------------
// // <copyright file="NameCharacter.cs" company="Short Leg Studio, LLC">
// //     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------
using ShortLegStudio.RPG.Names.Gateways;

namespace ShortLegStudio.RPG.Actions.NamingThings
{
	using System;

	public interface INameCharacter
	{
        string CreateFullName();
	}

}

