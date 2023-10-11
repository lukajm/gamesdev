using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory.Model;
using UnityEngine.UI;

public class AgentWeapon : MonoBehaviour
{
    [SerializeField]
    private EquipItemSO weapon;

    [SerializeField]
    private InventorySO inventoryData;

    public Text CurrentItem;
    public void SetWeapon(EquipItemSO weaponItemSO) {

        
        if (weapon != null) {
            inventoryData.AddItem(weapon, 1);
        }

        this.weapon = weaponItemSO;
    }

    public EquipItemSO GetEquippedWeapon()
    {
        return weapon;
    }
    
    public void Update()
    {
        if(weapon != null)
        {
        CurrentItem.text = "Current Weapon: " + weapon.name;
        }
    }

}
