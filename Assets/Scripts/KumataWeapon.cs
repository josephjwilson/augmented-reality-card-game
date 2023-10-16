using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KumataWeapon : MonoBehaviour
{
    public Transform attackPoint;
    public LayerMask attackMask;

    GameObject target;

    BattleHUD playerHUD;

    void Start()
    {
        playerHUD = GameObject.FindWithTag("PlayerHUD").GetComponent<BattleHUD>();
    }

    public void AttackEvent()
    {
        target = EnemyController.targetObject;
        int doAttack = Random.Range(0, 2);

        if (doAttack > 0)
        {
            int damage = GetComponent<Enemy>().attack - target.GetComponent<PlayerStats>().defense;

            if (damage < 0)
                damage = 0;

            FindObjectOfType<AudioManager>().Play("Kumata_Attack");
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
        }
        else
            Debug.Log("Enemy Attack Miss");


    }

    public void SuperAttack()
    {
        target = EnemyController.targetObject;

        int damage = (GetComponent<Enemy>().attack * 2) + 100 - target.GetComponent<PlayerStats>().defense;

        Debug.Log(damage);

        if (damage < 0)
            damage = 0;

        FindObjectOfType<AudioManager>().Play("Kumata_Passive");
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
    }
}
