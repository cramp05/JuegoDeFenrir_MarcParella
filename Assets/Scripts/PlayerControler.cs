using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Timeline.DirectorControlPlayable;

public class PlayerControler : MonoBehaviour
{
    public float movementSpeed = 5;
    public int direction = 1;
    public int dash = 2;
    private bool _isDash = false;
    public InputAction moveAction;
    public Vector2 moveDirection;
    private InputAction jumpAction;
    private InputAction _pauseAction;
    private InputAction _attackAction;

    public Rigidbody2D rBody2D;
    public float jumpForce = 10;

    private SpriteRenderer renderer;
    private BoxCollider2D _boxCollider;
    public GameObject attackHitBox;
    private BGMManager _bgmManagerScript;

    private GroundSensor sensor;

    private Animator animator;

    public AudioSource audioSource;
    public AudioSource audioSourceWalk;
    public AudioSource audioSourceAgua;

    public AudioClip deathSFXFenrir;
    public AudioClip hitFenrirSFX;
    public AudioClip walkSFX;
    public AudioClip attackSFX;
    public AudioClip aguaSFX;
    //public AudioClip win;

    public ParticleSystem _walkParticles;

    private GameManager _gameManager;


    bool _canPowerUPAzul = false;
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

        _pauseAction = InputSystem.actions["Pause"];
        _attackAction = InputSystem.actions["Attack"];

        animator = GetComponent<Animator>();

        _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        _bgmManagerScript = GameObject.Find("BGM Manager").GetComponent<BGMManager>();



    }
    void Start()
    {
        transform.position = new Vector3(4, 5, 0);

       // transform.position = startPosition;
    }


    void Update()
    {
        if(_attackAction.WasPerformedThisFrame() && _isDash == false)
        {
            //Attack();
            animator.SetTrigger("Atack");
            rBody2D.AddForce(transform.right * dash, ForceMode2D.Impulse);
            audioSource.PlayOneShot(attackSFX);
            return;
        }

        moveDirection = moveAction.ReadValue<Vector2>();

        if (_pauseAction.WasPressedThisFrame()) //para pausar la partida
        {
            _gameManager.Pause();
        }

        if (_gameManager._pause == true)
        {
            return;//para la funcion, todo lo que haya debajo lo para
        }

        

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
        if(_isDash == false)
        {
            rBody2D.linearVelocity = new Vector2(moveDirection.x * movementSpeed, rBody2D.linearVelocity.y);
        }
    }
    public void RecivirDaño()
    {
        audioSource.PlayOneShot(hitFenrirSFX);
    }



    public void InicioMuerteFenrir()
    {
        StartCoroutine(Fenrirdeath());
    }
    
    public IEnumerator Fenrirdeath()
    {
        _boxCollider.enabled = false; //desactiva el box collider

        _bgmManagerScript.StopBGM();

        animator.SetBool("IsDeath", true);

        audioSource.PlayOneShot(deathSFXFenrir);

        movementSpeed = 0;

        yield return new WaitForSeconds(2f);
        Destroy(gameObject);

        _gameManager.GameOver();

    }

    public void VelocidadPocionAzul()
    {
        if (_canPowerUPAzul == true)
        {
            movementSpeed = 7;
        }
    }
    public void VelocidadPocionAzulOFF()
    {
        if (_canPowerUPAzul == true)
        {
            _canPowerUPAzul = false;
            movementSpeed = 5;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

       /* if (collision.gameObject.tag == "Win")
        {
            StartCoroutine(WinMario());
        }*/

        if (collision.gameObject.CompareTag("PocionAzul"))
        {
            _pocionAzulTimer = 0;
            _canPowerUPAzul = true;
            _gameManager.TimePocionAzul();
            //PowerUpAzul();
        }
        if (collision.gameObject.CompareTag("Trampa"))
        {
            
        }
    }

    void Attack()
    {
        if(attackHitBox.activeInHierarchy)
        {
            attackHitBox.SetActive(false);
            _isDash = false;
            
        }
        else
        {
            attackHitBox.SetActive(true);
            _isDash = true;
        }
    }

    public void WalkSFX()
    {
        audioSourceWalk.PlayOneShot(walkSFX);
    }

    private void OnTriggerEnter (Collider collision)
    {
        if (collision.gameObject.CompareTag("Agua Entrada"))
        {
            audioSourceAgua.PlayOneShot(aguaSFX);
        }

    }

    /*void PowerUpAzul()
    {
        _pocionAzulTimer += Time.deltaTime;
        _gameManager.ContadorPocionAzul();

        if (_pocionAzulTimer >= _pocionAzulDuration)
        {
            _canPowerUPAzul = false;
        }
    }*/
}

