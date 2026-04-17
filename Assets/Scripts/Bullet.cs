using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Rigidbody2D rBody;

    public float bulletSpeed = 10;

    public int bulletDamage = 1; //para asignar la vida que quita la bala

    void Awake()
    {
        rBody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        rBody.AddForce(transform.right * bulletSpeed, ForceMode2D.Impulse); //adimos un impulso a la bala 
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        /*if(collision.gameObject.CompareTag("Player") || collision.gameObject.layer == 3)
        {
            return;
        }
        if(collision.gameObject.layer == 7)
        {
         //Destroy(collision.gameObject);
            Enemigo _enemyScript = collision.gameObject.GetComponent<Enemigo>();
            _enemyScript.TakeDamage(bulletDamage);
        }
        Destroy (gameObject);*/
    }

    void Update()
    {
        
    }
}
