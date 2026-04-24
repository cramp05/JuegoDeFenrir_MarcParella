using UnityEngine;

public class ActivadorTRanmpa : MonoBehaviour
{

    public Rigidbody2D rBody2D;

    public AudioClip trampa;
    private AudioSource _audioSource;


    void Awake()
    {
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
        if (collision.gameObject.CompareTag("Player"))
        {
            _audioSource.PlayOneShot(trampa);
            rBody2D.bodyType = RigidbodyType2D.Dynamic;
        }
    }


}
