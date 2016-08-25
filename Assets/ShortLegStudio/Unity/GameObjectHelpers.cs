//-----------------------------------------------------------------------
// <copyright file="GameObjectHelpers.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ShortLegStudio.Unity
{
    using UnityEngine;

    /// <summary>
    /// Helper Methods for GameObjects in Unity
    /// </summary>
    public static class GameObjectHelpers
    {
        /// <summary>
        /// Tries to find the specific game object, if it does not exist creates one with that name. 
        /// This is useful for organization or hierarchical game objects instead of cluttering up
        /// every scene with them
        /// </summary>
        /// <returns>A GameObject with the correct name</returns>
        /// <param name="name">Name of the object to find</param>
        public static GameObject FindOrCreate(string name)
        {
            GameObject obj = GameObject.Find(name);
            if (!obj)
            {
                obj = new GameObject(name);
            }

            return obj;
        }

        /// <summary>
        /// Convenience method to organize a GameObject into a hierarchy. Makes sure the hierarchy exists
        /// and then places the object underneath. Maintains world position
        /// </summary>
        /// <param name="folderName">Folder name GameObject to use</param>
        /// <param name="obj">Object to organize in the scene tree</param>
        public static void Organize(string folderName, GameObject obj)
        {
            var parent = FindOrCreate(folderName);
            obj.transform.SetParent(parent.transform, true);
        }
    }
}