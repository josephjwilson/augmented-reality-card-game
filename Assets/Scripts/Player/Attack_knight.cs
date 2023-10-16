using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Attack_knight : MonoBehaviour
{
    public Button attackButton;
    public Button specialButton;

    public Transform attackPoint;
    public Transform shieldPoint;
    public LayerMask attackMask;

    public float attackRange;

    public GameObject selectedObject;
    public GameObject knightShield;

    Animator animator;

    bool sanityCheck;

    void Start()
    {
        animator = GetComponent<Animator>();
        attackRange = GetComponent<PlayerStats>().attackRange;
    }

    // Update is called once per frame
    void Update()
    {

        selectedObject = PlayerController.selectedObject;
        //Debug.Log(sanityCheck);
        if (this.gameObject == PlayerController.selectedObject && sanityCheck == false)
        {
            sanityCheck = true;
            attackButton.onClick.AddListener(PlayerAttack);
            specialButton.onClick.AddListener(PlayerSpecial);
        }

        if (this.gameObject != PlayerController.selectedObject)
        {
            sanityCheck = false;
            attackButton.onClick.RemoveListener(PlayerAttack);
            specialButton.onClick.RemoveListener(PlayerSpecial);
        }
    }

    public void PlayerAttack()
    {
        int doAttack = Random.Range(0, 4);
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, attackMask);
        animator.SetTrigger("Attack");

        foreach (Collider enemy in hitEnemies)
        {
            if (doAttack > 0)
            {

                FindObjectOfType<AudioManager>().Play("Arthur_Sword");

                FindObjectOfType<AudioManager>().Play(enemy.GetComponent<Enemy>().enemyName + "_Hurt");
                enemy.GetComponent<Animator>().SetTrigger("Hurt");

                bool isDead = enemy.GetComponent<Enemy>().TakeDamage(GetComponent<PlayerStats>().attack);
                if (isDead == true)
                {
                    enemy.GetComponent<Animator>().SetTrigger("IsDead");
                    FindObjectOfType<AudioManager>().Play(enemy.GetComponent<Enemy>().enemyName + "_Death");
                }
            }
            else
                Debug.Log("Player Attack Miss");
        }
        StartCoroutine(AttackCooldown());
        Debug.Log(doAttack);
    }

    void PlayerSpecial()
    {
        FindObjectOfType<AudioManager>().Play("Arthur_Power-up");
        Instantiate(knightShield, shieldPoint.position, shieldPoint.rotation);
        GetComponent<PlayerStats>().attack *= 2;
        GetComponent<PlayerStats>().defense *= 2;
        StartCoroutine(SpecialReset());
        StartCoroutine(SpecialCooldown());
    }

    IEnumerator SpecialReset()
    {
        yield return new WaitForSeconds((GetComponent<PlayerStats>().specialCooldown) / 2);
        GetComponent<PlayerStats>().attack /= 2;
        GetComponent<PlayerStats>().defense /= 2;
    }

    IEnumerator AttackCooldown()
    {
        GameObject.Find("AttackButton").GetComponent<Button>().interactable = false;
        yield return new WaitForSeconds(GetComponent<PlayerStats>().attackCooldown);
        GameObject.Find("AttackButton").GetComponent<Button>().interactable = true;
    }

    IEnumerator SpecialCooldown()
    {
        GameObject.Find("SpecialButton").GetComponent<Button>().interactable = false;
        yield return new WaitForSeconds(GetComponent<PlayerStats>().specialCooldown);
        GameObject.Find("SpecialButton").GetComponent<Button>().interactable = true;
    }

    //Shows collider area of attack
    
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        //Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}
