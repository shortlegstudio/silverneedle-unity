//-----------------------------------------------------------------------
// <copyright file="LifeLimit.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ShortLegStudio.Behaviors
{
    using UnityEngine;

    /// <summary>
    /// Destroy an object after some amount of time
    /// </summary>
    public class LifeLimit : MonoBehaviour
    {
        /// <summary>
        /// How long until the object is destroyed
        /// </summary>
        [SerializeField]
        private float killTime;

        /// <summary>
        /// Start this instance.
        /// </summary>
        private void Start()
        {
            GameObject.Destroy(this.gameObject, this.killTime);
        }
    }
}