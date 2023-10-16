using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum BattleState { START, PLAYERTURN, PLAYERDRAWPHASE, PLAYERMAINPHASE, PREPLAYERBATTLEPHASE, PLAYERBATTLEPHASE, PLAYERAFTERBATTLEPHASE, ENEMYTURN, ENEMYATTACKPHASE, WON, LOST}

public class BattleController : MonoBehaviour
{
    //Gamestate Variable
    public static BattleState state;

    public BattleState test1;

    //Turn&Phase variables
    public TextMeshProUGUI turnText;
    public TextMeshProUGUI phaseText;
    public TextMeshProUGUI battleText;
    private int turnCount;

    //PlayerHUD
    public BattleHUD playerHUD;

    //Panels
    public GameObject displayPanel;
    public TextMeshProUGUI displayPanel_Text;
    public GameObject battlePanel;
    public GameObject phaseSelector;
    public GameObject outcome;

   //Timer
    private GameObject target;
    public GameObject timerPanel;

    //GameWheel buttons
    public GameObject hideButton;
    public GameObject selector;
    public GameObject endPhase;
    public GameObject battlePhase;
    public GameObject popUP;

    public GameObject enemyMISC;

    //PreBattle
    public GameObject preBattlePanel;
    public GameObject gameTemp;
    public GameObject playerController;

    bool sanityCheck = false;

    //Outcome
    public TextMeshProUGUI outcomeText;
    public TextMeshProUGUI outcomeTurn_Text;

    // Start is called before the first frame update
    void Start()
    {
        //enemyMISC = GameObject.FindWithTag("Enemy");
        ClearScreen();
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    // Update is called once per frame
    void Update()
    {
        turnText.text = turnCount.ToString("0");

        //Functions to check Outcome state
        if (sanityCheck == false && state == BattleState.WON || state == BattleState.LOST)
        {
            FindObjectOfType<AudioManager>().Stop("BGM_Main");
            Outcome();
            sanityCheck = true;
        }

        test1 = BattleController.state;
        //Debug.Log(test1);
    }

    IEnumerator SetupBattle()
    {
        yield return new WaitForSeconds(2f);
        turnCount = 0;
        playerHUD.SetHUD();

        yield return new WaitForSeconds(2f);
        popUP.SetActive(true);
        battleText.text = "DUEL";
        yield return new WaitForSeconds(1f);
        popUP.SetActive(false);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }
    
    void PlayerTurn()
    {
        turnCount++;
        phaseText.text = "Your Turn";

        state = BattleState.PLAYERDRAWPHASE;
        StartCoroutine(DrawPhase());
    }

    IEnumerator DrawPhase()
    {
        yield return new WaitForSeconds(2f);
        popUP.SetActive(true);
        battleText.text = "YOUR TURN";
        yield return new WaitForSeconds(1f);
        popUP.SetActive(false);


        phaseText.text = "Your Draw Phase";
        yield return new WaitForSeconds(1f);
        displayPanel.gameObject.SetActive(true);
        displayPanel_Text.text = "Draw a card.";

    }

    public void Continue()
    {
        if (state == BattleState.PLAYERDRAWPHASE)
        {
            Debug.Log("Draw Completed");
            displayPanel.gameObject.SetActive(false);
            state = BattleState.PLAYERMAINPHASE;
            StartCoroutine(MainPhase());
        } else if (state == BattleState.ENEMYTURN)
        {
            Debug.Log("Place Spawner");
            displayPanel.gameObject.SetActive(false);
            StartCoroutine(EnemyAttackPhase());
        }
    }

    IEnumerator MainPhase()
    {
        yield return new WaitForSeconds(1f);
        phaseText.text = "Your Main Phase";

        PhaseSelectorCondition();

        yield return new WaitForSeconds(1f);
        Debug.Log("Place Monsters");
    }

    IEnumerator PreBattlePhase()
    {
        preBattlePanel.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        gameTemp.SetActive(true);
    }

    IEnumerator BattlePhase()
    {
        phaseText.text = "Your Battle Phase";
        yield return new WaitForSeconds(0.5f);

        battlePanel.gameObject.SetActive(true);
        GameObject.Find("AttackButton").GetComponent<Button>().interactable = true;
        GameObject.Find("SpecialButton").GetComponent<Button>().interactable = true;

        GameObject newTimerPanel = Instantiate(timerPanel) as GameObject;
        newTimerPanel.transform.SetParent(GameObject.FindGameObjectWithTag("GameElements").transform, false);
        yield return new WaitForSeconds(10f);

        /* Add wheel button to select this option
         * Add countdown timer mechanic 
         */
        //Debug.Log("Time Up"); //Add actual timer

        phaseText.text = "Battle Phase Over";
        battlePanel.gameObject.SetActive(false);
        playerController.SetActive(false);
        yield return new WaitForSeconds(1f);

        state = BattleState.PLAYERAFTERBATTLEPHASE;
        StartCoroutine(AfterBattlePhase());

    }


    IEnumerator AfterBattlePhase()
    {
        playerController.SetActive(false);
        PhaseSelectorCondition();
        phaseText.text = "Your AfterBattle Phase";
        yield return new WaitForSeconds(2f);
        /* 
         * Add misc
         */
    }

    IEnumerator EndPhase()
    {
        phaseSelector.gameObject.SetActive(false);
        phaseText.text = "Your End Phase";
        yield return new WaitForSeconds(2f);
        state = BattleState.ENEMYTURN;
        Debug.Log("Turn End");
        /* 
         * Add misc
         */ 
        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurnStart());
    }

    IEnumerator EnemyTurnStart()
    {
        turnCount++;
        phaseText.text = "Enemy Turn";
        yield return new WaitForSeconds(2f);
        popUP.SetActive(true);
        battleText.text = "OPPONENT'S TURN";
        yield return new WaitForSeconds(1f);
        popUP.SetActive(false);
        phaseText.text = "Enemy Main Phase";
        if (turnCount <= 2)
        {
            displayPanel.gameObject.SetActive(true);
            displayPanel_Text.text = "Place enemy spawner.";
        }
        else
            StartCoroutine(EnemyAttackPhase());
    }

    IEnumerator EnemyAttackPhase()
    {
        yield return new WaitForSeconds(2f);
        phaseText.text = "Enemy Attack Phase";
        yield return new WaitForSeconds(1f);
        Debug.Log("Enemy Attacks");
        yield return new WaitForSeconds(1f);
        state = BattleState.ENEMYATTACKPHASE;

        //Attack sequence
        //Add dynamic incase more then 1 player
        enemyMISC.GetComponent<Animator>().SetBool("Walk", true);
        yield return new WaitForSeconds(30f);                                           /////////////////Change here
        enemyMISC.GetComponent<EnemyController>().StartPosition();
        enemyMISC.GetComponent<Animator>().SetBool("Walk", false);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    public void PreBattleButton()
    {
        phaseSelector.gameObject.SetActive(false);
        state = BattleState.PREPLAYERBATTLEPHASE;
        StartCoroutine(PreBattlePhase());

    }

    public void BattleButton()
    {
        playerController.SetActive(true);
        StartCoroutine(BattleButtonEnumerator());
    }

    IEnumerator BattleButtonEnumerator()
    {
        preBattlePanel.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        gameTemp.SetActive(false);
        state = BattleState.PLAYERBATTLEPHASE;
        StartCoroutine(BattlePhase());
    }

    public void EndButton()
    {
        StartCoroutine(EndPhase());
    }

    void ClearScreen()
    {
        displayPanel.gameObject.SetActive(false);
        //battlePanel.gameObject.SetActive(false);
        phaseSelector.gameObject.SetActive(false);
    }


    //Scripts for Buttons
    public void SelectButton()
    {
        selector.gameObject.SetActive(false);
        hideButton.gameObject.SetActive(true);
        endPhase.gameObject.SetActive(true);
        if (state == BattleState.PLAYERAFTERBATTLEPHASE || turnCount == 10)     //////////////////////Change here
        {
            battlePhase.gameObject.SetActive(false);
        }
        else if (state == BattleState.PLAYERMAINPHASE)
        {
            battlePhase.gameObject.SetActive(true);

        }
    }

    void PhaseSelectorCondition()
    {
        phaseSelector.gameObject.SetActive(true);
        selector.gameObject.SetActive(true);
        hideButton.gameObject.SetActive(false);
        endPhase.gameObject.SetActive(false);
        battlePhase.gameObject.SetActive(false);
    }

    public void Outcome()
    {
        StartCoroutine(OutComeCont());
    }

    IEnumerator OutComeCont()
    {

        yield return new WaitForSeconds(1f);


        popUP.gameObject.SetActive(true);
        if (state == BattleState.WON)
        {
            FindObjectOfType<AudioManager>().Play("BGM_Winner");
            battleText.text = "YOU WON!";
            outcomeText.text = "VICTORY";
        }

        if (state == BattleState.LOST)
        {
            FindObjectOfType<AudioManager>().Play("BGM_Loser");
            battleText.text = " YOU LOST...";
            outcomeText.text = "DEFEATED";
        }

        yield return new WaitForSeconds(1.5f);
        Time.timeScale = 0;
        popUP.gameObject.SetActive(false);
        ClearScreen();

        outcomeTurn_Text.text = turnCount.ToString("0");
        outcome.gameObject.SetActive(true);

    }
}
