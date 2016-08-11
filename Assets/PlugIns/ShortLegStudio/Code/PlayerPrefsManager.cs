//-----------------------------------------------------------------------
// <copyright file="PlayerPrefsManager.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ShortLegStudio 
{
    using UnityEngine;

    /// <summary>
    /// Provides a clean interface for accessing Unity's PlayerPref store
    /// </summary>
    public class PlayerPrefsManager
    {
        /// <summary>
        /// The key to find for Master Volume in the PlayerPrefs Store
        /// </summary>
        private const string MasterVolumeKey = "MasterVolume";

        /// <summary>
        /// The key to find for Difficulty in the PlayerPrefs Store
        /// </summary>
        private const string DifficultyKey = "Difficulty";

        /// <summary>
        /// Returns the master volume setting 
        /// </summary>
        /// <returns>The master volume.</returns>
        public static float GetMasterVolume()
        {
            return PlayerPrefs.GetFloat(MasterVolumeKey); 
        }

        /// <summary>
        /// Stores the master volume in the PlayerPrefs store
        /// </summary>
        /// <param name="volume">Volume to store.</param>
        public static void SetMasterVolume(float volume)
        {
            PlayerPrefs.SetFloat(
                MasterVolumeKey, 
                Mathf.Clamp(volume, 0, 1)); 
        }

        /// <summary>
        /// Gets the difficulty.
        /// </summary>
        /// <returns>The difficulty value</returns>
        public static float GetDifficulty()
        {
            return PlayerPrefs.GetInt(DifficultyKey);
        }

        /// <summary>
        /// Stores the difficulty
        /// </summary>
        /// <param name="difficulty">Difficulty level to store</param>
        public static void SetDifficulty(int difficulty)
        {
            PlayerPrefs.SetInt(DifficultyKey, Mathf.Clamp(difficulty, 1, 3));
        }
    }
} 