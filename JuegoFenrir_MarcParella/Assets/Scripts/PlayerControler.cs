using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerControler : MonoBehaviour
{
    public float movementSpeed = 5;
    public int direction = 1;
    public InputAction moveAction;
    public Vector2 moveDirection;
    private InputAction jumpAction;

    public Rigidbody2D rBody2D;
    public float jumpForce = 10;

    private SpriteRenderer renderer;
    private BoxCollider2D _boxCollider;

    private GroundSensor sensor;

    private Animator animator;

    public void Awake()
    {
        rBody2D = GetComponent<Rigidbody2D>();

        renderer = GetComponent<SpriteRenderer>();
        _boxCollider = GetComponent<BoxCollider2D>();

        sensor = GetComponentInChildren<GroundSensor>();

        moveAction = InputSystem.actions["Move"];

        jumpAction = InputSystem.actions["Jump"];

        animator = GetComponent<Animator>();
    }
    void Start()
    {
        transform.position = new Vector3(4, 5, 0);

       // transform.position = startPosition;
    }


    void Update()
    {
        moveDirection = moveAction.ReadValue<Vector2>();

        if (moveDirection.x > 0)
        {
            renderer.flipX = false;
            animator.SetBool("IsRunning", true);
        }

        else if (moveDirection.x < 0)
        {
            renderer.flipX = true;
            animator.SetBool("IsRunning", true);
        }

        else
        {
            animator.SetBool("IsRunning", false); //detectar botones pulsados siempre en la funcion update
        }

        if (jumpAction.WasPressedThisFrame() && sensor.isGrouned)
        {
            rBody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }



        animator.SetBool("IsJumping", !sensor.isGrouned);
    }
    public void Bounce()
    {
        rBody2D.linearVelocity = new Vector2(rBody2D.linearVelocity.x, 0);
        rBody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        //animator.SetBool("IsJumping", !sensor.isGrouned);

    }


    void FixedUpdate()
    {
        rBody2D.linearVelocity = new Vector2(moveDirection.x * movementSpeed, rBody2D.linearVelocity.y);
    }

}

