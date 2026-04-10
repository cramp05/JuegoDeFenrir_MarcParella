using UnityEngine;

public class GroundSensor : MonoBehaviour
{
    public bool isGrouned;
    PlayerControler _playerScript;

    void Awake()
    {
        _playerScript = GetComponentInParent<PlayerControler>();
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            isGrouned = true;
        }

    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            isGrouned = false;
        }
    }
}
