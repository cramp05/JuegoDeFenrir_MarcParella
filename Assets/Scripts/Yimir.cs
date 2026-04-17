using UnityEngine;

public class Yimir : MonoBehaviour
{
    public float movementSpeed = 4;
    public int direction = 1;
    public Rigidbody2D rBody2D;
    private Animator _animator;

    private SpriteRenderer renderer;

   // private AudioSource _audioSource;
    private BoxCollider2D _boxCollider;

    //public AudioClip deathSFX;

   // private GameManger _gameManager;

    private PlayerControler _playerScript;
    private int _yimirHealtyh = 3;
  //private Slider _healthSlider;

  public Transform[] patrolPoints;
  public int patrolIndex = 0;

  private Transform playerPosition;

  public float detectionRange = 5; //detectar como de cerca esta el jugador
  public float attackRange = 7;


  void Awake()
  {
    _animator = GetComponent<Animator>();
    rBody2D = GetComponent<Rigidbody2D>();
    //_audioSource = GetComponent<AudioSource>();
    _boxCollider = GetComponent<BoxCollider2D>();
    // _gameManager = GameObject.Find("Game Manager").GetComponent<GameManger>();

    _playerScript = GameObject.Find("Fenrir").GetComponent<PlayerControler>();
    //_healthSlider = GetComponentInChildren<Slider>();

    _animator.SetBool("Yimir walk", true);

    playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
    
        
    }
  void Start()
  {
    //_healthSlider.maxValue = _yimirHealtyh;
    //_healthSlider.value = _yimirHealtyh;
  }

    void Update()
    {
      
    }
  void FixedUpdate()
  {
    //rBody2D.linearVelocity = new Vector2(direction * movementSpeed, rBody2D.linearVelocity.y);  
    float distanceToPlayer = Vector3.Distance(playerPosition.position, transform.position);
    if (distanceToPlayer > detectionRange)
    {
      Patrol();
    }
    else if (distanceToPlayer < detectionRange && distanceToPlayer > attackRange)
    {
      //FollowPlayer();
    }
    else if (distanceToPlayer < attackRange)
    {
      Attack();
    }
  }
  void OnCollisionEnter2D(Collision2D collision)
  {
      if(collision.gameObject.CompareTag("Player")) 
      {
        // StartCoroutine(_playerScript.Mariodeath());
      }
  }

  void Patrol()
  {
    float distanceToPoint = Vector3.Distance(transform.position, patrolPoints[patrolIndex].position);
    if (distanceToPoint < 0.5f)
    {
      if (patrolIndex == 0)
      {
        patrolIndex = 1;
      }
      else
      {
        patrolIndex = 0;
      }
    }

    Vector3 moveDirection = patrolPoints[patrolIndex].position - transform.position;
    Movement(moveDirection);
  }

  void FollowPlayer()
  {
    Vector3 moveDirection = playerPosition.position - transform.position;
    Movement(moveDirection);
  }

  void Movement(Vector3 moveDirection)
  {
    if (moveDirection.x < 0)
    {
      direction = -1;
      transform.rotation = Quaternion.Euler(0, 180, 0);
    }
    else if (moveDirection.x > 0)
    {
      direction = 1;
      transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    rBody2D.linearVelocity = new Vector2(direction * movementSpeed, rBody2D.linearVelocity.y);  
  }

  void Attack()
  {
    Vector3 moveDirection = playerPosition.position - transform.position;
    direction = 0;
    if (moveDirection.x < 0)
    {
      transform.rotation = Quaternion.Euler(0, 180, 0);
    }
    else if (moveDirection.x > 0)
    {
      transform.rotation = Quaternion.Euler(0, 0, 0);
    }

  }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("TopeParaEnemigos"))
        {
          direction *= -1;

          if (direction == 1)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
          if (direction == -1)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }  
          
        }
    }

  public void TakeDamage(int damage) //funcion para quitar vida al goomba
  {
    _yimirHealtyh -= damage; //resta uno a la variable vida
    //_healthSlider.value = _yimirHealtyh;

    if (_yimirHealtyh <= 0)
    {
      YimirDeath();
    }

  
  }
  public void YimirDeath()
  {
    _animator.SetBool("Yimir idle", true);

    //_gameManager.Addkill();

    //_audioSource.PlayOneShot(deathSFX);

    movementSpeed = 0;

    _boxCollider.enabled = false; //desactiva el box collider

    Destroy(gameObject, 1.2f);
  }
}
