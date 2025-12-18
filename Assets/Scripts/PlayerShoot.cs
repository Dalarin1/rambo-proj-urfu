using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed = 25.0f;
    [SerializeField] private float fireRate = 0.2f; 

    private bool isShooting = false;
    private float nextFireTime = 0f;

 
    public void OnShootInput(InputAction.CallbackContext context)
    {
        isShooting = context.started || context.performed;
    }

    void Update()
    {

        if (isShooting && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate; 
        }
    }

    private void Shoot()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 shootDirection = (mousePos - new Vector2(transform.position.x, transform.position.y));
        shootDirection.Normalize();

        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = 
            new Vector2(shootDirection.x * bulletSpeed, shootDirection.y * bulletSpeed);
        Destroy(bullet, 2f);
    }
}