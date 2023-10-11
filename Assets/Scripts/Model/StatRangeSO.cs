using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class StatRangeSO : StatModifierSO
{

    public override void AffectPlayer(GameObject player, float val) {
        player playerScript = player.GetComponent<player>();

        if (playerScript != null)
        {
            playerScript.AddRange((int)val);
        }
    }
}
