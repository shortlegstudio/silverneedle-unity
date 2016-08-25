//-----------------------------------------------------------------------
// <copyright file="ShredOnExit3D.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ShortLegStudio.Unity 
{
    using UnityEngine;

    /// <summary>
    /// A behavior that can be added to a unity object that shreds any object that leaves
    /// it's boundaries by destroying that object. Shreds indiscriminatly
    /// </summary>
    public class ShredOnExit3D : MonoBehaviour 
    {
        /// <summary>
        /// Called from Unity when another object has left the boundaries of the shredder
        /// </summary>
        /// <param name="other">GameObject that is leaving the boundary</param>
        private void OnTriggerExit(Collider other) 
        {
            Debug.LogFormat("{0} is destroying {1}", this.name, other.name);
            GameObject.Destroy(other.gameObject);
        }
    }
}
