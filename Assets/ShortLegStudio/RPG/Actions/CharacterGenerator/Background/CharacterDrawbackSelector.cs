// //-----------------------------------------------------------------------
// // <copyright file="CharacterDrawbackSelector.cs" company="Short Leg Studio, LLC">
// //     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------
using System;
using ShortLegStudio.RPG.Characters.Background.Gateways;
using ShortLegStudio.RPG.Characters.Background;

namespace ShortLegStudio.RPG.Characters.Background
{
    public class CharacterDrawbackSelector
    {
        IDrawbackGateway drawbacks;

        public CharacterDrawbackSelector(IDrawbackGateway drawbacks)
        {
            this.drawbacks = drawbacks;
        }

        public Drawback SelectDrawback() 
        {
            return drawbacks.ChooseOne();
        }
    }
}

