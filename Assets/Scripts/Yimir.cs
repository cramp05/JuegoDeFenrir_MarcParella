using System.Collections;
using UnityEngine;

public class Yimir : MonoBehaviour
{
    public float movementSpeed = 4;
    public int direction = 1;
    public Rigidbody2D rBody2D;
    private Animator _animator;

    private SpriteRenderer renderer;

    public AudioSource audioSource;
    public AudioSource audioSourceWalk;
    private BoxCollider2D _boxCollider;

    public AudioClip deathSFX;
    public AudioClip attackYimirSFX;
    public AudioClip yimirWalk;

    //private GameManger _gameManager;

    private GameObject _fenrir;

    private PlayerControler _playerScript;
    private int _yimirHealtyh = 3;
  //private Slider _healthSlider;

  public Transform[] patrolPoints;
  public int patrolIndex = 0;

  private Transform playerPosition;

  public float detectionRange = 5; //detectar como de cerca esta el jugador
  public float attackRange = 7;
  public GameObject bulletPrefab; //para asignar el objeto
  public Transform bulletSpawn; //variable para controlar donde aparecen las balas
  bool _canShoot = false;
  float _attackDelay = 2;
  float _shotTimer = 0;


  void Awake()
  {
    _animator = GetComponent<Animator>();
    rBody2D = GetComponent<Rigidbody2D>();
    _boxCollider = GetComponent<BoxCollider2D>();
    // _gameManager = GameObject.Find("Game Manager").GetComponent<GameManger>();

    _playerScript = GameObject.Find("Fenrir").GetComponent<PlayerControler>();
    _fenrir = GameObject.Find("Fenrir");
    //_healthSlider = GetComponentInChildren<Slider>();

    _animator.SetBool("Yimir walk", true);
    _animator.SetBool("Yimir idle", false);

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
    if (_fenrir != null)//&& _fenrir.gameObject != null
    {
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
        _animator.SetBool("Yimir idle", false);
        _animator.SetBool("Yimir walk", true);
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
    //WalkSFX();
  }
    public void WalkSFX()
    {
        /*if (_animator.GetBool("Yimir walk"))
        {
            if (!audioSourceWalk.isPlaying)
            {
                audioSourceWalk.Play();
            }
        }
        else
        {
            if (audioSourceWalk.isPlaying)
            {
                audioSourceWalk.Stop();
            }
        }*/
        audioSourceWalk.PlayOneShot(yimirWalk);
        //yield return null;
    }

    void Attack()
  {
        _animator.SetBool("Yimir idle", true);
        _animator.SetBool("Yimir walk", false);
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
     StartCoroutine(ShootTime());
   }

    public IEnumerator ShootTime()
    {
        _animator.SetTrigger("Yimir atack");
        yield return new WaitForSeconds(_attackDelay);
    }

    public void Shoot()
    {
        Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
        audioSource.PlayOneShot(attackYimirSFX);
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

        audioSource.PlayOneShot(deathSFX);

         movementSpeed = 0;

        _boxCollider.enabled = false; //desactiva el box collider

        Destroy(gameObject, 1.2f);
  }
}
