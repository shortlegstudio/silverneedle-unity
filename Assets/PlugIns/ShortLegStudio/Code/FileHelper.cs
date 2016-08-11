//-----------------------------------------------------------------------
// <copyright file="FileHelper.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ShortLegStudio
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using UnityEngine;      // TODO: Dependent on Unity for Application.dataPath. Better to find a way to isolate this
    using YamlDotNet.RepresentationModel;

    /// <summary>
    /// Provides helper methods for loading and accessing files
    /// </summary>
    public static class FileHelper
    {
        /// <summary>
        /// Loads a Yaml document and returns the YamlDotNet parser
        /// </summary>
        /// <returns>The YamlDotNet parser</returns>
        /// <param name="fileName">File name to open</param>
        public static YamlNodeWrapper OpenYaml(string fileName)
        {
            var path = Path.Combine(Application.dataPath, fileName);
            Debug.LogFormat("Loading Yaml File: {0}", path);
            var input = new StreamReader(path);

            var yaml = new YamlStream();
            yaml.Load(input);

            // Examine the stream
            return new YamlNodeWrapper(yaml.Documents[0].RootNode);
        }

        /// <summary>
        /// Returns all files in a path based off of the dataPath for the application
        /// </summary>
        /// <returns>The files found in the directory</returns>
        /// <param name="path">Relative path to find the files</param>
        public static string[] GetFiles(string path)
        {
            return Directory.GetFiles(Path.Combine(Application.dataPath, path));
        }
    }
}