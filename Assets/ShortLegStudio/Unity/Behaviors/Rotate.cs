//-----------------------------------------------------------------------
// <copyright file="Rotate.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ShortLegStudio.Unity
{
    using UnityEngine;

    /// <summary>
    /// Rotates a game object
    /// </summary>
    public class Rotate : MonoBehaviour
    {
        /// <summary>
        /// The rotate speed.
        /// </summary>
        [SerializeField]
        private Vector3 rotateSpeed = Vector3.zero;

        /// <summary>
        /// Update this instance.
        /// </summary>
        private void Update()
        {
            this.transform.Rotate(this.rotateSpeed * Time.deltaTime);
        }
    }
}