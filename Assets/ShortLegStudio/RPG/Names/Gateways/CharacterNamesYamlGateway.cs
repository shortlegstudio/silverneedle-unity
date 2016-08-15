// //-----------------------------------------------------------------------
// // <copyright file="CharacterNamesGatewayYaml.cs" company="Short Leg Studio, LLC">
// //     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// // </copyright>
// //-----------------------------------------------------------------------
using System.Collections;
using System.Xml.Linq;

namespace ShortLegStudio.RPG.Names.Gateways
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CharacterNamesYamlGateway : ICharacterNamesGateway
    {
        private const string CharacterNamesDataFile = "Data/Names.yml";
        private IList<NameInformation> namesDatabase;

        public CharacterNamesYamlGateway() : this(FileHelper.OpenYaml(CharacterNamesDataFile))
        {
            
        }

        public CharacterNamesYamlGateway(YamlNodeWrapper yamlData)
        {
            namesDatabase = new List<NameInformation>();
            LoadFromYaml(yamlData);
        }

        public System.Collections.Generic.IList<string> GetFirstNames()
        {
            return namesDatabase
                .Where(x => x.Type == NameTypes.First)
                .SelectMany(x => x.Names)
                .ToList();
        }

        public System.Collections.Generic.IList<string> GetLastNames()
        {
            return namesDatabase
                .Where(x => x.Type == NameTypes.Last)
                .SelectMany(x => x.Names)
                .ToList();
        }


        private void LoadFromYaml(YamlNodeWrapper yamlData)
        {
            foreach (var node in yamlData.Children())
            {
                var name = new NameInformation();
                name.Gender = node.GetString("gender");
                name.Type = node.GetEnum<NameTypes>("category");
                name.Race = node.GetString("race");
                var names = node.GetCommaString("names");

                name.Names.Add(
                    names.Where(x => string.IsNullOrEmpty(x) == false));
                namesDatabase.Add(name);
            }
        }
    }
}

