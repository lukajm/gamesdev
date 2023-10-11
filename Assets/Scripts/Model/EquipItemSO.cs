using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model {

    [CreateAssetMenu]
    public class EquipItemSO : ItemSO, IDestroyableItem, IItemAction
    {

        public string ActionName => "Equip";
        public AnimationClip equipAnimation;
        public int strength; 
        public float range;
        public float speed;

        public bool PerformAction(GameObject player) {
            AgentWeapon weaponSystem = player.GetComponent<AgentWeapon>();

            if (weaponSystem != null) {
                weaponSystem.SetWeapon(this);
                return true;
            }
            return false;
        }

        private void PlayEquipAnimation(GameObject player)
        {
            Animator animator = player.GetComponent<Animator>();
            if (animator != null && equipAnimation != null)
            {
                animator.Play(equipAnimation.name);
            }
        }        
    }

}