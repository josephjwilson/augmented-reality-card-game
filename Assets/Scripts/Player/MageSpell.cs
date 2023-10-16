using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageSpell : MonoBehaviour
{
    public float speed = 10f;

    public Rigidbody rb;
    public GameObject impactEffect;

    public GameObject playerMage;
    public int attack;

    // Start is called before the first frame update
    void Awake()
    {
        FindObjectOfType<AudioManager>().Play("Arthur_mage");
        playerMage = GameObject.Find("Arthur_mage");
        rb.velocity = transform.forward * speed;
        attack = playerMage.GetComponent<PlayerStats>().attack;
        Debug.Log("attack");
    }

    void OnTriggerEnter(Collider hitInfo)
    {
        Enemy enemy = hitInfo.GetComponent<Enemy>();
        if(enemy != null)
        {
            hitInfo.GetComponent<Animator>().SetTrigger("Hurt");
            FindObjectOfType<AudioManager>().Play(enemy.GetComponent<Enemy>().enemyName + "_Hurt");

            bool isDead = enemy.TakeDamage(attack);
            if (isDead == true )
            {
                hitInfo.GetComponent<Animator>().SetTrigger("IsDead");
                FindObjectOfType<AudioManager>().Play(enemy.GetComponent<Enemy>().enemyName + "_Death");
                //battleController.state = BattleState.WON;
                //battleController.Outcome();
            }
        }
        Instantiate(impactEffect, transform.position, transform.rotation);
        //Destroy(gameObject);
    }
}
