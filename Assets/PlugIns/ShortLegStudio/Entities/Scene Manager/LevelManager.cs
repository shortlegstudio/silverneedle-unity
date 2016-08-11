//-----------------------------------------------------------------------
// <copyright file="LevelManager.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ShortLegStudio 
{
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using UnityEngine.UI;

    /// <summary>
    /// Helps load levels and handle switching between scenes
    /// </summary>
    public class LevelManager : MonoBehaviour
    {
        /// <summary>
        /// Gets the active scene index value
        /// </summary>
        /// <value>The index of the active scene.</value>
        public int ActiveSceneIndex
        {
            get
            {
                return SceneManager.GetActiveScene().buildIndex;
            }
        }

        /// <summary>
        /// Returns the static instance of the Level Manager
        /// </summary>
        /// <returns>The instance of the Level Manager</returns>
        public static LevelManager GetInstance()
        {
            // TODO: This is a pretty crappy implementation
            return GameObject.FindObjectOfType<LevelManager>();
        }

        /// <summary>
        /// Exits the game
        /// </summary>
        public void ExitGame()
        {
            Application.Quit();
        }

        /// <summary>
        /// Starts the game
        /// </summary>
        public void StartGame()
        {
            this.LoadNextLevel();
        }

        /// <summary>
        /// Loads the specified level or scene
        /// </summary>
        /// <param name="levelName">Level name to load</param>
        public void LoadLevel(string levelName)
        {
            SceneManager.LoadScene(levelName);
        }

        /// <summary>
        /// Loads the next level.
        /// </summary>
        public void LoadNextLevel()
        {
            SceneManager.LoadScene(this.ActiveSceneIndex + 1);
        }

        /// <summary>
        /// Loads the next level with a delay
        /// </summary>
        /// <param name="delay">Delay before loading the next level</param>
        public void LoadNextLevel(float delay)
        {
            this.Invoke("LoadNextLevel", delay);
        }
    }
}