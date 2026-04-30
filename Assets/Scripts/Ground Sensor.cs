using UnityEngine;

public class GroundSensor : MonoBehaviour
{
    public bool isGrouned;
    PlayerControler _playerScript;

    public ParticleSystem _jumpParticles;
    public AudioSource audioSourceJump;
    public AudioClip jumpSFX;
    public AudioClip aterrizaSFX;

    void Awake()
    {
        _playerScript = GetComponentInParent<PlayerControler>();
    
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            isGrouned = true;
            _jumpParticles.Play();
            audioSourceJump.PlayOneShot(aterrizaSFX);
        }

    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            isGrouned = false;
            audioSourceJump.PlayOneShot(jumpSFX);

        }
    }
}
