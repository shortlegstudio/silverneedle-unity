using UnityEngine;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using YamlDotNet.RepresentationModel;
using System;

namespace ShortLegStudio {
	public static class YamlHelpers {
		public static YamlNodeWrapper ParseYaml(this string yamlString) {
			var input = new StringReader(yamlString);
			var yaml = new YamlStream();
			yaml.Load(input);
			return new YamlNodeWrapper(yaml.Documents [0].RootNode);
		}
	}

	public class YamlNodeWrapper {
		public string BOOLEAN_TRUE = "yes";
		public YamlNode Node { get; private set; }
		private YamlSequenceNode _seqNode;
		private YamlScalarNode _scalarNode;
		private YamlMappingNode _mappingNode;

		public YamlNodeWrapper(YamlNode wrap) {
			Node = wrap;
			_seqNode = Node as YamlSequenceNode;
			_scalarNode = Node as YamlScalarNode;
			_mappingNode = Node as YamlMappingNode;


		}

		public bool HasChildren() {
			return _seqNode != null && _seqNode.Children.Count > 0;
		}

		public IList<YamlNodeWrapper> Children() {
			return _seqNode.Children.Select (x => new YamlNodeWrapper (x)).ToList();
		}

		public string GetString(string key) {
			var item = GetNode (key);
			return item.Value;
		}

		public string GetStringOptional(string key) {
			var item = GetNodeOptional (key);
			if (item != null)
				return item.Value;

			return null;
		}

		public string[] GetCommaStringOptional(string key) {
			var val = GetStringOptional (key);
			if (val != null)
				return Regex.Split(val, "\\s*,\\s*");
		
			return new string [] { };
		}

		public bool GetBoolOptional(string key) {
			return GetStringOptional (key) == "yes";
		}

		public int GetIntegerOptional(string key) {
			var v = GetStringOptional (key);
			if (v == null)
				return 0;

			return int.Parse (v);
		}

		public int GetInteger(string key) {
			return int.Parse (GetString (key));
		}

		public float GetFloat(string key) {
			return float.Parse (GetString (key));
		}

		public t GetEnum<t>(string key) {
			return (t)Enum.Parse (typeof(t), GetStringOptional (key));
		}

		public YamlNodeWrapper GetNode(string key) {
			try {
				//ShortLog.DebugFormat("Retrieving Node: {0}", key);
				var item = _mappingNode.Children [new YamlScalarNode(key)];
				return new YamlNodeWrapper (item);
			} catch {
				ShortLog.ErrorFormat ("Yaml Node not found: {0}", key);
				throw;
			}
		}

		public YamlNodeWrapper GetNodeOptional(string key) {
			try {
				var item = _mappingNode.Children [new YamlScalarNode(key)];
				return new YamlNodeWrapper (item);
			} catch(KeyNotFoundException) {
				return null;
			}
		}

		public IDictionary<string, string> ChildrenToDictionary() {
			var results = new Dictionary<string, string> (); 
			if (_mappingNode != null) {
				foreach (var item in _mappingNode.Children) {
					results.Add (item.Key.ToString(), 
						item.Value.ToString());
				}
			}
			return results;
		}

		public string Value { 
			get {
				if (_scalarNode != null)
					return _scalarNode.Value;
				return null;
			}
		}

		public string Key {
			get {
				if (_scalarNode != null)
					return _scalarNode.Tag;

				return null;
			}
		}

		public override string ToString ()
		{
			return string.Format ("[YamlNodeWrapper: Node={0}, Value={1}, Key={2}, HasChildren={3}]", Node, Value, Key, HasChildren());
		}
	}

}