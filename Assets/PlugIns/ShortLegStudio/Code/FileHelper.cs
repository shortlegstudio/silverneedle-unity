using System.Collections.Generic;
using System.IO;
using System.Text;
using System;
using UnityEngine;
using YamlDotNet.RepresentationModel;

namespace ShortLegStudio {
	public static class FileHelper  {
		public static YamlNodeWrapper OpenYaml(string fileName) {
			var path = Path.Combine (Application.dataPath, fileName);
			Debug.LogFormat ("Loading Yaml File: {0}", path);
			var input = new StreamReader(path);

			var yaml = new YamlStream();
			yaml.Load(input);

			//Debug.Log (yaml.Documents[0].RootNode);
			// Examine the stream
			return new YamlNodeWrapper(yaml.Documents[0].RootNode);
		}

		public static string[] GetFiles(string path) {
			return Directory.GetFiles (Path.Combine (Application.dataPath, path));
		}

	}



}