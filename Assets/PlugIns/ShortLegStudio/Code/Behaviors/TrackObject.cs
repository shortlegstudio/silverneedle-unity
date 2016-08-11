//-----------------------------------------------------------------------
// <copyright file="TrackObject.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ShortLegStudio.Behaviors
{
    using UnityEngine;

    /// <summary>
    /// Tracks a game object by moving the main camera
    /// </summary>
    public class TrackObject : MonoBehaviour
    {
        /// <summary>
        /// The target object to track
        /// </summary>
        [SerializeField]
        private GameObject target;

        /// <summary>
        /// The minimum boundary to stay within
        /// </summary>
        [SerializeField]
        private Vector3 minBoundary;

        /// <summary>
        /// The max boundary to stay within
        /// </summary>
        [SerializeField]
        private Vector3 maxBoundary;

        /// <summary>
        /// The offset from which to track the object
        /// </summary>
        private Vector3 offset;

        /// <summary>
        /// Start this instance.
        /// </summary>
        private void Start()
        {
            this.offset = this.transform.position - this.target.transform.position;
        }

        /// <summary>
        /// Update this instance.
        /// </summary>
        private void Update()
        {
            if (!this.target)
            {
                return;
            }

            Vector3 pos = this.target.transform.position + this.offset;
            pos = VectorHelpers.Clamp(pos, this.minBoundary, this.maxBoundary);
            this.transform.position = pos;
        }
    }
}