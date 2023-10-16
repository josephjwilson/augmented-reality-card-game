using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static GameObject selectedObject;
    Rigidbody rb;
    Animator animator;
    PlayerCard card;
    Transform transform;

    public FixedJoystick joystick;
    public RectTransform handle;

    public float moveForce;

    public Vector3 startPosition;
    public Quaternion startRotation;

    void OnEnable()
    {
        //Setting up button
        joystick.GetComponent<FixedJoystick>();

        //Assigning the correct values
        selectedObject = SelectManager.selectObject;

        rb = selectedObject.GetComponent<Rigidbody>();
        animator = selectedObject.GetComponent<Animator>();
        card = selectedObject.GetComponent<PlayerStats>().card;
        transform = selectedObject.GetComponent<Transform>();

        startPosition = transform.position;
        startRotation = transform.rotation;

        //battleController = GameObject.FindWithTag("BattleController").GetComponent<BattleController>();

        moveForce = selectedObject.GetComponent<PlayerStats>().moveForce;
    }

    // Update is called once per frame
    void Update()
    {
        rb = selectedObject.GetComponent<Rigidbody>();
        if (BattleController.state == BattleState.PLAYERBATTLEPHASE && selectedObject != null )
        {
            rb.velocity = new Vector3(joystick.Horizontal * moveForce, rb.velocity.y, joystick.Vertical * moveForce);
            if (joystick.Horizontal != 0f || joystick.Vertical != 0f)
            {
                animator.SetFloat("Speed", 0.1f);
                transform.rotation = Quaternion.LookRotation(rb.velocity);
            }
            else
            {
                animator.SetFloat("Speed", 0f);
            }
        }
    }

    public void StopMovement()
    {
        animator.enabled = false;
        animator.SetFloat("Speed", 0f);
        animator.enabled = true;

        transform.position = startPosition;
        transform.rotation = startRotation;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    void OnDisable()
    {
        handle.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 0f);
        StopMovement();

        selectedObject = null;
        rb = null;
        animator = null;
    }
}
