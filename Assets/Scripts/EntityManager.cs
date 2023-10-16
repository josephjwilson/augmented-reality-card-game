using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour
{
    public GameObject[] players;
    public GameObject[] enemies;

    public GameObject target;

    //public BattleController battleController;


    void Update()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        /*
        if (Input.GetKeyDown(KeyCode.B))
        {
            Debug.Log(players[0]);
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            Destroy(players[0]);
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            //Debug.Log(enemies.Length);

            Destroy(enemies[0]);
        }
        */
        //Enemy Detection if alive
        if (enemies.Length == 0)
            BattleController.state = BattleState.WON;

        if (players.Length == 0)
            BattleController.state = BattleState.LOST;

    }

    public GameObject PlayerIndex()
    {
        int index;
        index = Random.Range(0, players.Length);
        target = players[index];
        return target;
    }
}
