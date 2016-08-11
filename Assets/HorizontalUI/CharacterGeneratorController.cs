//-----------------------------------------------------------------------
// <copyright file="CharacterGeneratorController.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace ShortLegStudio.SilverNeedle 
{
    using System;
    using ShortLegStudio.RPG.Characters;
    using ShortLegStudio.RPG.Gateways;
    using ShortLegStudio.RPG.Mechanics.CharacterGenerator;
    using ShortLegStudio.RPG.Mechanics.CharacterGenerator.Abilities;
    using UnityEngine;

    /// <summary>
    /// Manages the interaction of generating new characters
    /// </summary>
    public class CharacterGeneratorController : MonoBehaviour
    {
        /// <summary>
        /// The character genetator mechanic
        /// </summary>
        private CharacterGenerator generator;

        /// <summary>
        /// Occurs when a character is generated.
        /// </summary>
        public event EventHandler Generated;

        /// <summary>
        /// Gets the character currently being generatred
        /// </summary>
        public CharacterSheet Character { get; private set; }

        /// <summary>
        /// Generates the character.
        /// </summary>
        public void GenerateCharacter()
        {
            this.Character = this.generator.GenerateRandomCharacter();
            this.OnGenerated();
        }

        /// <summary>
        /// Start this instance.
        /// </summary>
        private void Start()
        {
            this.generator = new CharacterGenerator(
                new StandardAbilityScoreGenerator(),
                new LanguageSelector(new LanguageYamlGateway()),
                new RaceSelector(new RaceYamlGateway(), new TraitYamlGateway()),
                new NameGenerator());
        }

        /// <summary>
        /// Raises the generated event.
        /// </summary>
        private void OnGenerated()
        {
            if (this.Generated != null)
            {
                this.Generated(this, new EventArgs());
            }
        }
    }
}