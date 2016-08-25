//-----------------------------------------------------------------------
// <copyright file="MoveStraight.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ShortLegStudio.Unity
{
    using UnityEngine;

    /// <summary>
    /// Simple script that moves straight line along some direction
    /// </summary>
    public class MoveStraight : MonoBehaviour
    {
        /// <summary>
        /// The move direction.
        /// </summary>
        [SerializeField]
        private Vector3 moveDirection;

        /// <summary>
        /// The move speed.
        /// </summary>
        [SerializeField]
        private float moveSpeed;

        /// <summary>
        /// Start this instance.
        /// </summary>
        private void Start()
        {
            float angle = Mathf.Atan2(this.moveDirection.x, this.moveDirection.y) * Mathf.Rad2Deg;
            this.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        /// <summary>
        /// Called each physic update
        /// </summary>
        private void FixedUpdate()
        {
            this.transform.position += this.moveDirection * this.moveSpeed * Time.deltaTime;
        }
    }
}