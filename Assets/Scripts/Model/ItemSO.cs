using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Inventory.Model {
    //[CreateAssetMenu]
    public abstract class ItemSO : ScriptableObject
    {

        [field: SerializeField]
        public bool IsStackable { get; set; }

        [field: SerializeField]
        public int ID => GetInstanceID();

        [field: SerializeField]
        public string Name { get; set; }

        [field: SerializeField]
        [field: TextArea]
        public string Description { get; set; }

        [field: SerializeField]
        public Sprite ItemImage { get; set; }
    }
}
