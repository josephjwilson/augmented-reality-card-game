using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public PlayerCard card;
    public BattleController battleController;

    public int attack;
    public int defense;

    public float attackRange;
    public float moveForce;

    public int healthPoints;
    public int deathDamage;

    public float attackCooldown;
    public float specialCooldown;

    void Awake()
    {
        //attackRange = card.attackRange;
        attack = card.attack;
        defense = card.defense;

        attackRange = card.attackRange;
        moveForce = card.moveForce;

        healthPoints = card.healthPoints;
        deathDamage = card.deathDamage;

        attackCooldown = card.attackCooldown;
        specialCooldown = card.specialCooldown;
    }

    public bool TakeHealthPoints(int hitPoints)
    {
        healthPoints -= hitPoints;
        if (healthPoints <= 0)
        {
            healthPoints = 0;
            return true;
        }
        else
            return false;
    }

    public void Dead()
    {
        Destroy(this.gameObject);
    }
}
