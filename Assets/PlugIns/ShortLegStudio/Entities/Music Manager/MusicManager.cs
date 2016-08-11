//-----------------------------------------------------------------------
// <copyright file="MusicManager.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ShortLegStudio 
{
    using ShortLegStudio;
    using UnityEngine;

    /// <summary>
    /// Music Manager handles loading and playing music in the scene
    /// </summary>
    public class MusicManager : MonoBehaviour
    {
        /// <summary>
        /// Audio Source to use playing the music
        /// </summary>
        private AudioSource audioSource;

        /// <summary>
        /// Gets or sets the Audio clips to play on each level
        /// </summary>
        public AudioClip[] LevelMusic { get; set; }

        /// <summary>
        /// Ability to set the volume of the music that should be played
        /// </summary>
        /// <param name="volume">Volume to set to. Range is 1 to 0</param>
        public void SetVolume(float volume)
        {
            this.audioSource.volume = volume;
        }

        /// <summary>
        /// Called from Unity when level is loaded
        /// </summary>
        /// <param name="level">Level that was loaded</param>
        private void OnLevelWasLoaded(int level)
        {
            if (level >= 0 && level < this.LevelMusic.Length)
            {
                var clip = this.LevelMusic[level];
                if (clip && this.audioSource.clip != clip)
                {
                    this.audioSource.loop = true;
                    this.audioSource.clip = clip;
                    this.audioSource.Play();
                }
            }
        }

        /// <summary>
        /// Called when script is first executed
        /// </summary>
        private void Start()
        {
            this.audioSource = this.GetComponent<AudioSource>();
            this.SetVolume(PlayerPrefsManager.GetMasterVolume());
        }
    }
}