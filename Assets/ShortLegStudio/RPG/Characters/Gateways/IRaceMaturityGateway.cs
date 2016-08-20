// //-----------------------------------------------------------------------
// // <copyright file="RaceMaturityYamlGateway.cs" company="Short Leg Studio, LLC">
// //     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------
using System;
using ShortLegStudio.Enchilada;
using System.Collections.Generic;
using ShortLegStudio.Dice;
using System.Linq;




namespace ShortLegStudio.RPG.Characters.Gateways
{
	public interface IRaceMaturityGateway
	{
        Maturity Get(Race race);
        IEnumerable<Maturity> All();
	}

}

