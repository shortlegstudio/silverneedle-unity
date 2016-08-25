//-----------------------------------------------------------------------
// <copyright file="FadeIn.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ShortLegStudio.Unity
{
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// Fade in is a behavior that will fade in an object over time. Requires an image component
    /// </summary>
    public class FadeIn : MonoBehaviour
    {
        /// <summary>
        /// The fade in time.
        /// </summary>
        [SerializeField]
        private float fadeInTime;

        /// <summary>
        /// Start this instance.
        /// </summary>
        private void Start()
        {
            // Fade in and then remove object
            Image alphaChannel = GetComponent<Image>();
            alphaChannel.CrossFadeAlpha(0, this.fadeInTime, true);
            GameObject.Destroy(this.gameObject, this.fadeInTime);
        }
    }
}