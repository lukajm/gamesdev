using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatModifierSO : ScriptableObject
{

    public abstract void AffectPlayer(GameObject player, float val);
}
