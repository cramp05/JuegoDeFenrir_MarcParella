using UnityEngine;

public class ActivadorTRanmpa : MonoBehaviour
{

    public Rigidbody2D rBody2D;



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
            rBody2D.bodyType = RigidbodyType2D.Dynamic;
        }
    }


}
