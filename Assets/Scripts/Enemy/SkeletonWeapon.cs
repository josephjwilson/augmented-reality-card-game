using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonWeapon : MonoBehaviour
{
    public Transform attackPoint;
    public LayerMask attackMask;

    GameObject target;

    public float attackRange = 5f;
    //public BattleController battleController;

    BattleHUD playerHUD;

    void Start()
    {
        playerHUD = GameObject.FindWithTag("PlayerHUD").GetComponent<BattleHUD>();
    }

    public void AttackEvent()
    {
        target = EnemyController.targetObject;
        int doAttack = Random.Range(0, 3);

        if (doAttack > 0)
        {
            int damage = GetComponent<Enemy>().attack - target.GetComponent<PlayerStats>().defense;   

           if (damage < 0)
                damage = 0;

            FindObjectOfType<AudioManager>().Play("Skeleton_Attack");
            bool unitDead = target.GetComponent<PlayerStats>().TakeHealthPoints(Random.Range(1, 3));

            if (unitDead == true)
            {
                FindObjectOfType<AudioManager>().Play("Arthur_Dead");
                target.GetComponent<Animator>().SetTrigger("IsDead");
                playerHUD.TakeDamage(target.GetComponent<PlayerStats>().deathDamage);
            }
            else
            {
                FindObjectOfType<AudioManager>().Play("Arthur_Hurt");
                target.GetComponent<Animator>().SetTrigger("Hurt");
            }

            bool isDead = playerHUD.TakeDamage(damage);
            if (isDead == true)
            {
                GetComponent<EnemyController>().StartPosition();
                GetComponent<Animator>().enabled = false;
                BattleController.state = BattleState.LOST;
            }
        } else
            Debug.Log("Enemy Attack Miss");

    }
}
