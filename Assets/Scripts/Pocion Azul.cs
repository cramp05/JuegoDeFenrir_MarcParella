using UnityEngine;

public class PocionAzul : MonoBehaviour
{
    //private GameManger _gameManager;

    public AudioClip coin;
    //private AudioSource _audioSource;

    public SpriteRenderer renderSprite;

    private BoxCollider2D _boxCollider;

    void Awake()
    {
       // _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
       // _audioSource = GetComponent<AudioSource>();
        renderSprite = GetComponent<SpriteRenderer>();
        _boxCollider = GetComponent<BoxCollider2D>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))  //destruccion de monedas y activar el contador de moneda
        {

            //_audioSource.PlayOneShot(coin);
            _boxCollider.enabled = false; //desactiva el box collider
            renderSprite.enabled = false;
           // _gameManager.TimePocionAzul();
            Destroy(gameObject, 1.2f);

        }
    }
}
