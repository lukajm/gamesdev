using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Inventory.Model {

    [CreateAssetMenu]
    public class HealthItemSO : ItemSO, IDestroyableItem, IItemAction
    {

        [SerializeField]
        private List<ModifierData> modifiersData = new List<ModifierData>();

        public string ActionName => "Consume";

        public bool PerformAction(GameObject player) {
            foreach (ModifierData data in modifiersData) {
                data.statModifier.AffectPlayer(player, data.value);
            }
            return true;
        }
    }


    public interface IDestroyableItem {

    }

    public interface IItemAction {
        string ActionName {get;}
        bool PerformAction(GameObject player);
    }

    [Serializable]
    public class ModifierData {
        public StatModifierSO statModifier;
        public float value;
    }
}