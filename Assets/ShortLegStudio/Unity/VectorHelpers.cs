//-----------------------------------------------------------------------
// <copyright file="VectorHelpers.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ShortLegStudio.Unity
{
    using UnityEngine;

    /// <summary>
    /// Provides help for managing vectors
    /// </summary>
    public static class VectorHelpers
    {
        /// <summary>
        /// Takes a vector3 and rounds all coordinates to nearest integer
        /// </summary>
        /// <returns>The vector aligned to the grid.</returns>
        /// <param name="pos">Position to snap to the grid</param>
        public static Vector3 SnapToGrid(Vector3 pos)
        {
            return new Vector3(
                Mathf.RoundToInt(pos.x),
                Mathf.RoundToInt(pos.y),
                Mathf.RoundToInt(pos.z));
        }

        /// <summary>
        /// Clamp the vector between min and max.
        /// </summary>
        /// <param name="value">Vector with properties to clap</param>
        /// <param name="min">Minimum value for all 3 coordinates</param>
        /// <param name="max">Max value for all 3 coordinates</param>
        /// <returns>The clamped Vector3</returns>
        public static Vector3 Clamp(Vector3 value, Vector3 min, Vector3 max)
        {
            return new Vector3(
                Mathf.Clamp(value.x, min.x, max.x),
                Mathf.Clamp(value.y, min.y, max.y),
                Mathf.Clamp(value.z, min.z, max.z));
        }
    }
}