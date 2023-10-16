using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Card", menuName = "Enemy Card MK1")]
public class EnemyCard : ScriptableObject
{
    public string cardName;
    public string description;
    public Sprite artwork;

    public int healthPoints; 
    public int attack;

    public float attackRange;
    public float moveForce;
}
