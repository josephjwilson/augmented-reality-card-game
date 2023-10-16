using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Run : StateMachineBehaviour
{
    Transform target;
    Rigidbody rb;

    EnemyController enemyController;

    public float speed;
    public float attackRange;
    

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyController = animator.GetComponent<EnemyController>();
        enemyController.EnemyTarget();
        target = EnemyController.target;

        rb = animator.GetComponent<Rigidbody>();

        //Add if no target exist
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        enemyController.LookAtPlayer();

        Vector3 newPos = Vector3.MoveTowards(rb.position, target.position, speed * Time.fixedDeltaTime);

        if (Vector3.Distance(target.position, rb.position) >= attackRange)
        {
            rb.MovePosition(newPos);
        }
        //Attack Transition
        if (Vector3.Distance(target.position, rb.position) <= attackRange)
        {
            animator.SetTrigger("Attack");
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
