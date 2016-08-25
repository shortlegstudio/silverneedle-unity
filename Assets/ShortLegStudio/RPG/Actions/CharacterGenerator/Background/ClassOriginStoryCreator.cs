// //-----------------------------------------------------------------------
// // <copyright file="ClassOriginStoryCreator.cs" company="Short Leg Studio, LLC">
// //     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------
using System;
using ShortLegStudio.RPG.Characters.Background;

namespace ShortLegStudio.RPG.Actions.CharacterGenerator.Background
{
    public class ClassOriginStoryCreator
    {
        IClassOriginYamlGateway classOrigins;

        public ClassOriginStoryCreator(IClassOriginYamlGateway classOrigins)
        {
            this.classOrigins = classOrigins;
        }

        public ClassOrigin CreateStory(string cls)
        {
            return classOrigins.ChooseOne(cls);
        }
    }
}

