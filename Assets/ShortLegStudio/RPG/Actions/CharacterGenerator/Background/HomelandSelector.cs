// //-----------------------------------------------------------------------
// // <copyright file="HomelandSelector.cs" company="Short Leg Studio, LLC">
// //     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------
using System;
using ShortLegStudio.RPG.Characters.Background.Gateways;
using ShortLegStudio.RPG.Characters.Background;
using System.Linq;
using UnityEngine;
using ShortLegStudio.RPG.Utility;

namespace ShortLegStudio.RPG.Actions.CharacterGenerator.Background
{
    public class HomelandSelector
    {
        private IHomelandGateway homelands;

        public HomelandSelector(IHomelandGateway homelands)
        {
            this.homelands = homelands;
        }

        public Homeland SelectHomelandByRace(string race)
        {
            return this.homelands.GetRacialOptions(race).ChooseRandomly();
        }
    }
}

