//-----------------------------------------------------------------------
// <copyright file="ShredOnExit.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ShortLegStudio.Unity
{
    using UnityEngine;

    /// <summary>
    /// Shreds objects that leave the boundaries of the object containing this script
    /// </summary>
    public class ShredOnExit : MonoBehaviour
    {
        /// <summary>
        /// Event raised when object leaves boundaries
        /// </summary>
        /// <param name="other">Other game object that is leaving</param>
        private void OnTriggerExit2D(Collider2D other)
        {
            GameObject.Destroy(other.gameObject);
        }
    }
}