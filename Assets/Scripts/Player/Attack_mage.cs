using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Attack_mage : MonoBehaviour
{
    public Button attackButton;
    public Button specialButton;

    public Transform attackPoint;

    public GameObject mageAttack;
    public GameObject mageHeal;
    public GameObject selectedObject;

    public Transform healLocation;

    bool sanityCheck;
    // Start is called before the first frame update
    
    void Update()
    {

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

    void PlayerAttack()
    {
        Debug.Log("Mage attack");
        Instantiate(mageAttack, attackPoint.position, attackPoint.rotation);
        StartCoroutine(AttackCooldown());
    }

    void PlayerSpecial()
    {
        FindObjectOfType<AudioManager>().Play("Arthur_Heal");
        Instantiate(mageHeal, healLocation.position, healLocation.rotation);
        int randomHeal = Random.Range(1, 6);
        GameObject.FindWithTag("PlayerHUD").GetComponent<BattleHUD>().TakeDamage(-randomHeal * 100);
        StartCoroutine(SpecialCooldown());
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
}
