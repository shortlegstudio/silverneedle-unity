using System;
using UnityEngine;

namespace ShortLegStudio
{
	public static class ShortLog {
		private static bool _useConsole = false;

		static ShortLog () {
			try {
				UnityEngine.Debug.Log("Starting Logger");
			} catch {
				_useConsole = true;
			}

		}

		public static void Error(string message) {
			if (_useConsole) {
				Console.WriteLine ("ERROR: {0}", message);
				return;
			}

			UnityEngine.Debug.LogError (message);
		}

		public static void ErrorFormat(string format, params string[] args) {
			var s = string.Format (format, args);
			Error (s);
		}

		public static void Debug(string message) {
			if (_useConsole) {
				Console.WriteLine ("DEBUG: {0}", message);
				return;
			}

			UnityEngine.Debug.Log (message);
		}

		public static void DebugFormat(string format, params string[] args) {
			var s = string.Format (format, args);
			Debug (s);
		}
	}
}

