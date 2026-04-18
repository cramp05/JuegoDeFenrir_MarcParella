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

    public ParticleSystem _walkParticles;

    float _pocionAzulDuration = 10;
    float _pocionAzulTimer;

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
            //renderer.flipX = false;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            animator.SetBool("IsRunning", true);


            if(!_walkParticles.isPlaying && sensor.isGrouned)
            {
                _walkParticles.Play();
            }
        }

        else if (moveDirection.x < 0)
        {
            //renderer.flipX = true;
            transform.rotation = Quaternion.Euler(0, 180, 0);
            animator.SetBool("IsRunning", true);

            if(!_walkParticles.isPlaying && sensor.isGrouned)
            {
                _walkParticles.Play();
            }

        }

        else
        {
            animator.SetBool("IsRunning", false); //detectar botones pulsados siempre en la funcion update
            if(_walkParticles.isPlaying)
            {
                _walkParticles.Stop();
            }
        }

        if (jumpAction.WasPressedThisFrame() && sensor.isGrouned)
        {
            rBody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            
        }

        if(!sensor.isGrouned && _walkParticles.isPlaying)
        {
            _walkParticles.Stop();
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

