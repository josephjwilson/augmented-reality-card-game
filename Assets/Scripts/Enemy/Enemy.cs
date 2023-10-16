using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyCard card;

    public string enemyName;

    public int maxHP;
    public int currentHP;
    public int attack;

    public float attackRange;
    public float moveForce;

    public bool isDead;

    public EnemyHUD enemyHUD;

    void Start()
    {
        enemyName = card.cardName;
        //Changes stats based on difficulty
        switch (PlayerData.difficultyIndex)
        {
            case 0:
                maxHP = card.healthPoints;
                attack = card.attack;
                break;
            case 1:
                maxHP = card.healthPoints + 500;
                attack = card.attack + 100;
                break;
            case 2:
                maxHP = card.healthPoints + 1000;
                attack = card.attack + 200;
                break;
            default:
                maxHP = card.healthPoints;
                break;
        }

        currentHP = maxHP;
        attackRange = card.attackRange;
        moveForce = card.moveForce;


        //enemyHUD = GetComponentInChildren<EnemyHUD>();
        enemyHUD.SetHUD();
    }

    public bool TakeDamage(int damage)
    {
        currentHP -= damage;
        enemyHUD.SetHP(currentHP);
        //Play Hurt animation

        if (currentHP <= 0)
        {
            return true;
        } else
        {
            return false;
        }
    }
}
