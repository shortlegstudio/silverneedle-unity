// //-----------------------------------------------------------------------
// // <copyright file="RaceMaturityYamlGateway.cs" company="Short Leg Studio, LLC">
// //     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------
using System;
using ShortLegStudio;
using System.Collections.Generic;
using ShortLegStudio.Dice;
using System.Linq;




namespace ShortLegStudio.RPG.Characters
{
	public interface IRaceMaturityGateway
	{
        Maturity Get(Race race);
        IEnumerable<Maturity> All();
	}

}

