using System;
using UnityEngine;

namespace ShortLegStudio
{
	public class Randomly 
	{
		private static bool UseSystem = false;
		private static System.Random _sysRandom;

		static Randomly() {
			try {
				UnityEngine.Random.Range(0, 10);
			} catch {
				UseSystem = true;
			}
			_sysRandom = new System.Random ();
		}

		public static int Range(int min, int max) {
			if (UseSystem) {
				return _sysRandom.Next (min, max);
			} 

			return UnityEngine.Random.Range (min, max);
		}

		public static float Range(float min, float max) {
			if (UseSystem) {
				return (float)_sysRandom.NextDouble () * (max - min) + min;
			}

			return UnityEngine.Random.Range (min, max);
		}
	}
}

