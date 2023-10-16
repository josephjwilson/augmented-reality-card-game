using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kumata_Run : StateMachineBehaviour
{
    Transform target;
    Rigidbody rb;

    EnemyController enemyController;

    public float attackRange;
    public float moveForce;

    bool doAttackCheck = false;
    int doAttack;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        attackRange = animator.GetComponent<Enemy>().attackRange;
        moveForce = animator.GetComponent<Enemy>().moveForce;

        enemyController = animator.GetComponent<EnemyController>();
        enemyController.EnemyTarget();
        target = EnemyController.target;

        rb = animator.GetComponent<Rigidbody>();

        //Attack selector
        if(doAttackCheck == false)
            doAttack = Random.Range(0, 3);

        doAttackCheck = true;
        //Add if no target exist
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log(doAttack);
        enemyController.LookAtPlayer();

        Vector3 newPos = Vector3.MoveTowards(rb.position, target.position, moveForce * Time.fixedDeltaTime);

        if (Vector3.Distance(target.position, rb.position) >= attackRange)
        {
            rb.MovePosition(newPos);
        }
        //Attack Transition
        if (Vector3.Distance(target.position, rb.position) <= attackRange)
        {
            Debug.Log(doAttack);
            if (doAttack > 0)
            {
                animator.SetTrigger("Attack");
                doAttackCheck = false;
            }
            else
            {
                animator.SetTrigger("Passive");
                doAttackCheck = false;
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        //Make sure to reset trigger 
        animator.ResetTrigger("Attack");
    }

}
