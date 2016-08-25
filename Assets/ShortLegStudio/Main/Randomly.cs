//-----------------------------------------------------------------------
// <copyright file="Randomly.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ShortLegStudio
{
    using System;
    using UnityEngine; //TODO: Remove UnityEngine Reference

    /// <summary>
    /// Provides random number functionality in a consistent way that allows 
    /// unit tests and Unity to run smoothly. Basically just abstracts away
    /// the various behavior
    /// </summary>
    public class Randomly
    {
        /// <summary>
        /// Tracks whether to use the System Random number generator or Unity's
        /// </summary>
        private static bool useSystem = false;

        /// <summary>
        /// The System Random number generator instance
        /// </summary>
        private static System.Random systemRandom;

        /// <summary>
        /// Initializes static members of the <see cref="ShortLegStudio.Randomly"/> class.
        /// </summary>
        static Randomly()
        {
            try
            {
                UnityEngine.Random.Range(0, 10);
            }
            catch
            {
                useSystem = true;
            }

            systemRandom = new System.Random();
        }

        /// <summary>
        /// Generates a random number in Range. Max is exclusive
        /// </summary>
        /// <param name="min">Inclusive minimum value.</param>
        /// <param name="max">Exclusive maximum value.</param>
        /// <returns>A random number within range</returns>
        public static int Range(int min, int max)
        {
            if (useSystem)
            {
                return systemRandom.Next(min, max);
            } 

            return UnityEngine.Random.Range(min, max);
        }

        /// <summary>
        /// Range the specified min and max, inclusive.
        /// </summary>
        /// <param name="min">Inclusive minimum value.</param>
        /// <param name="max">Inclusive maximum value.</param>
        /// <returns>A random number in range</returns>
        public static float Range(float min, float max)
        {
            if (useSystem)
            {
                return (float)(systemRandom.NextDouble() * (max - min)) + min;
            }

            return UnityEngine.Random.Range(min, max);
        }
    }
}