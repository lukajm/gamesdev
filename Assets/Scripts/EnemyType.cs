using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Enemy Type", menuName = "Enemy Type")]
public class EnemyType : ScriptableObject
{
    public string typeName;
    public float speed;
    public int health;
    public int damage;
    public int xp;

    public void AddXPToPlayer(LevelSystem levelSystem)
    {
        levelSystem.GainExperienceFlatRate(xp);
    }

}