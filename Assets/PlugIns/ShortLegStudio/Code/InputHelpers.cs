//-----------------------------------------------------------------------
// <copyright file="InputHelpers.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ShortLegStudio
{
    using UnityEngine;

    /// <summary>
    /// Methods for providing assistance managing Input methods in Unity
    /// </summary>
    public static class InputHelpers
    {
        /// <summary>
        /// Gets the mouse world coordinates in 2D. Based off of the main camera
        /// </summary>
        /// <returns>The mouse world coordinates2D.</returns>
        public static Vector3 GetMouseWorldCoordinates2D()
        {
            var mouse = Input.mousePosition;
            mouse.z = mouse.z - Camera.main.transform.position.z;
            return Camera.main.ScreenToWorldPoint(mouse);
        }
    }
}