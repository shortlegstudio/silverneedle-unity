//-----------------------------------------------------------------------
// <copyright file="ClampToViewport.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ShortLegStudio.Unity 
{
    using UnityEngine;

    /// <summary>
    /// Ensures a GameObject stays within the boundary of the current view. Works only for 2D
    /// </summary>
    public class ClampToViewport : MonoBehaviour
    {
        /// <summary>
        /// Minimum allowable X value
        /// </summary>
        private float minX;

        /// <summary>
        /// Maximum allowable X value
        /// </summary>
        private float maxX;

        /// <summary>
        /// Minimum allowable Y value
        /// </summary>
        private float minY;

        /// <summary>
        /// Maximum allowable Y value
        /// </summary>
        private float maxY;

        /// <summary>
        /// Start this instance.
        /// </summary>
        private void Start()
        {
            float distance = transform.position.z - Camera.main.transform.position.z;

            Vector3 leftBottomMost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));
            Vector3 rightTopMost = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, distance));

            // Calculate Offsets
            SpriteRenderer render = GetComponent<SpriteRenderer>();
            float offsetX = render.sprite.pivot.x / render.sprite.pixelsPerUnit;
            float offsetY = render.sprite.pivot.y / render.sprite.pixelsPerUnit;

            // set Clamps
            this.minX = leftBottomMost.x + offsetX;
            this.maxX = rightTopMost.x - offsetX;

            this.minY = leftBottomMost.y + offsetY;
            this.maxY = rightTopMost.y - offsetY;
        }

        /// <summary>
        /// Update this instance.
        /// </summary>
        private void Update()
        {
            // Clamp
            Vector3 clamped = new Vector3(
                            Mathf.Clamp(transform.position.x, this.minX, this.maxX),
                            Mathf.Clamp(transform.position.y, this.minY, this.maxY),
                            transform.position.z);
            transform.position = clamped;
        }
    }
}