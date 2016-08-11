//-----------------------------------------------------------------------
// <copyright file="LoadNextDelay.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ShortLegStudio.Behaviors 
{
    using ShortLegStudio;
    using UnityEngine;

    /// <summary>
    /// Loads the next level after a delay
    /// </summary>
    public class LoadNextDelay : MonoBehaviour
    {
        /// <summary>
        /// Gets or sets load delay.
        /// </summary>
        public float LoadDelay { get; set; }

        /// <summary>
        /// Start this instance.
        /// </summary>
        private void Start()
        {
            this.Invoke("LoadLevel", this.LoadDelay);
        }

        /// <summary>
        /// Loads the level.
        /// </summary>
        private void LoadLevel()
        {
            LevelManager.GetInstance().LoadNextLevel();
        }
    }
}