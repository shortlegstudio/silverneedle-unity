//-----------------------------------------------------------------------
// <copyright file="PurchaseInitialArmor.cs" company="Short Leg Studio, LLC">
//     Copyright (c) Short Leg Studio, LLC. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace ShortLegStudio.RPG.Mechanics.CharacterGenerator
{
    using System;
    using System.Linq;
    using ShortLegStudio.RPG.Characters;
    using ShortLegStudio.RPG.Equipment;
    using ShortLegStudio.RPG.Equipment.Gateways;

    /// <summary>
    /// Purchase initial armor for a character
    /// </summary>
    public class PurchaseInitialArmor
    {
        /// <summary>
        /// The armors available
        /// </summary>
        private IArmorGateway armors;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="ShortLegStudio.RPG.Mechanics.CharacterGenerator.PurchaseInitialArmor"/> class.
        /// </summary>
        /// <param name="armorRepo">Armor gateway to load from.</param>
        public PurchaseInitialArmor(IArmorGateway armorRepo)
        {
            this.armors = armorRepo;
        }

        /// <summary>
        /// Purchases the armor and shield.
        /// </summary>
        /// <param name="inventory">Inventory to assign to</param>
        public void PurchaseArmorAndShield(Inventory inventory)
        {
            var armor = this.armors.FindByArmorTypes(
                            ArmorType.Light,
                            ArmorType.Medium,
                            ArmorType.Heavy).ToList().ChooseOne();
            inventory.EquipItem(armor);

            var shield = this.armors.FindByArmorType(ArmorType.Shield).ToList().ChooseOne();
            inventory.EquipItem(shield);
        }
    }
}