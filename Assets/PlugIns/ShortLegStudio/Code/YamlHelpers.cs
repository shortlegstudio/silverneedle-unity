﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using YamlDotNet.RepresentationModel;

namespace ShortLegStudio {
	public class YamlHelpers {
		
	}

	public class YamlNodeWrapper {
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

		public string GetValue(string key) {
			var item = GetNode (key);
			return item.Value;
		}

		public YamlNodeWrapper GetNode(string key) {
			var item = _mappingNode.Children [new YamlScalarNode(key)];
			return new YamlNodeWrapper (item);
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
	}

}