using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon_mage : MonoBehaviour
{
    public PlayerCard card;
    public Transform attackPoint;
    public LayerMask attackMask;
    //public BattleController battleController;

    public int attack;
    public int defense;
    public float attackRange;

    public int healthPoints;
    public int deathDamage;

    void Start()
    {
        attackRange = card.attackRange;
        attack = card.attack;
        defense = card.defense;

        healthPoints = card.healthPoints;
        deathDamage = card.deathDamage;
    }

    void LateUpdate()
    {
        if (healthPoints <= 0)
            Destroy(this.gameObject);
    }

    public void AttackEvent()
    {
        int doAttack = Random.Range(0, 3);
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, attackMask);

        foreach (Collider enemy in hitEnemies)
        {
            if (doAttack > 0)
            {
                enemy.GetComponent<Animator>().SetTrigger("Hurt");
                bool isDead = enemy.GetComponent<Enemy>().TakeDamage(attack);
                if (isDead == true)
                {
                    enemy.GetComponent<Animator>().SetTrigger("IsDead");
                    BattleController.state = BattleState.WON;
                    GetComponent<Animator>().enabled = false;
                }
            }
            else
                Debug.Log("Player Attack Miss");
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        //Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}

