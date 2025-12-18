using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float damage = 1.0f;
    public static float mul = 1.0f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamagable enemy = collision.GetComponent<IDamagable>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage*mul);
            Destroy(gameObject);
        }
    }
}