using UnityEngine;

public class PocionAzul : MonoBehaviour
{
    private GameManager _gameManager;

    public AudioClip powerUp;
    //private AudioSource _audioSource;

    public SpriteRenderer renderSprite;

    private BoxCollider2D _boxCollider;

    public AudioClip pocionSound;
    private AudioSource _audioSource;

    void Awake()
    {
        _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        renderSprite = GetComponent<SpriteRenderer>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _audioSource = GetComponent<AudioSource>();
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

            _audioSource.PlayOneShot(pocionSound);
            _boxCollider.enabled = false; //desactiva el box collider
            renderSprite.enabled = false;
           // _gameManager.TimePocionAzul();
            Destroy(gameObject, 1.2f);

        }
    }
}
