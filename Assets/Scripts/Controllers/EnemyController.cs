using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public EntityManager entityManager;

    public static GameObject targetObject;
    public static Transform target;

    BattleController battleState;
    Vector3 startPosition;

    // Start is called before the first frame update
    void Awake()
    {
        startPosition = transform.position;
    }

    public void LookAtPlayer()
    {
        Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 1 * Time.deltaTime);
    }

    public void StartPosition()
    {
        transform.position = startPosition;
    }

    public void EnemyTarget()
    {
        targetObject = entityManager.PlayerIndex();
        target = targetObject.transform;
        Debug.Log(target);
    }
}
