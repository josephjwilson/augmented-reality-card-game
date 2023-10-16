using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Player Card MK4")]
public class PlayerCard : ScriptableObject
{
    public string cardName;
    public string description;

    public Sprite artwork;

    public int attack;
    public int defense;

    public float attackRange;
    public float moveForce;

    public int healthPoints;
    public int deathDamage;

    public float attackCooldown;
    public float specialCooldown;
}
