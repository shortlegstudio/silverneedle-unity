// //-----------------------------------------------------------------------
// // <copyright file="Maturity.cs" company="Short Leg Studio, LLC">
// //     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------
using System;
using ShortLegStudio.Dice;

namespace ShortLegStudio.RPG.Characters
{
    public class Maturity
    {
        public Maturity()
        {
        }

        public int Adulthood { get; set; }
        public Cup Young { get; set; }
        public Cup Trained { get; set; }
        public Cup Studied { get; set; } 
    }
}

