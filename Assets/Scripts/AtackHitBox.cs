using UnityEngine;

public class AtackHitBox : MonoBehaviour
{
    public int attackDamage = 3;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.layer == 6)
        {
            Yimir enemyScript = collider.gameObject.GetComponent<Yimir>();
            enemyScript.TakeDamage(attackDamage);
        }
    }
}
