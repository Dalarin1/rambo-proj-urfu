using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float movementSpeed = 3.0f;
    [SerializeField] private float speedMultipluer = 1f;
    [SerializeField] private float jumpForce = 3.0f;
    private float horizontalMovement;
    private float verticalMovement; // Добавляем вертикальное движение

    [Header("GroundCheck")]
    [SerializeField] private Transform GroundCheckPos;
    [SerializeField] private Vector2 GroundCheckSize = new Vector2(0.5f, 0.05f);
    [SerializeField] private LayerMask GroundLayer;
    private bool isFacingRight = true;

    [Header("Ladder Climbing")]
    [SerializeField] private float climbSpeed = 5f; // Скорость лазания
    [SerializeField] private LayerMask LadderLayer; // Слой лестниц
    private bool isOnLadder = false;
    private bool isClimbing = false;
    private float originalGravityScale;

    [SerializeField] private int health;
    [SerializeField] public int maxHealth = 5;

    //[SerializeField] private float damageMultiplyer = 1.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        health = maxHealth - 2;
        HPBoostItem.OnCollect += Heal;
        SpeedBoostItem.OnCollect += SpeedUp;
        DamageBoostItem.OnCollect += IncreaseDamage;

        originalGravityScale = rb.gravityScale; // Сохраняем оригинальную гравитацию
    }

    void Update()
    {
        CheckLadder(); // Проверяем, находится ли игрок на лестнице

        if (isClimbing)
        {
            // При лазании отключаем гравитацию и двигаем вертикально
            rb.gravityScale = 0;
            rb.velocity = new Vector2(horizontalMovement * movementSpeed * speedMultipluer,
                                    verticalMovement * climbSpeed * speedMultipluer);
        }
        else
        {
            // Обычное движение
            rb.gravityScale = originalGravityScale;
            rb.velocity = new Vector2(horizontalMovement * movementSpeed * speedMultipluer, rb.velocity.y);
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        Vector2 movement = context.ReadValue<Vector2>();
        horizontalMovement = movement.x;
        verticalMovement = movement.y; // Добавляем вертикальный ввод
        Flip();
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (IsGrounded()) // Не позволяем прыгать во время лазания
        {
            if (context.performed)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
            else if (context.canceled)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            }
        }
    }

    private void CheckLadder()
    {
        // Проверяем, находится ли игрок в зоне лестницы
        isOnLadder = Physics2D.OverlapBox(GroundCheckPos.position, GroundCheckSize, 0, LadderLayer);
        isClimbing = isOnLadder;
    }

    private bool IsGrounded()
    {
        if (Physics2D.OverlapBox(GroundCheckPos.position, GroundCheckSize, 0, GroundLayer))
        {
            return true;
        }
        return false;
    }

    private void Flip()
    {
        if ((isFacingRight && horizontalMovement < 0) || (!isFacingRight && horizontalMovement > 0))
        {
            isFacingRight = !isFacingRight;
            Vector3 ls = transform.localScale;
            ls.x *= -1;
            transform.localScale = ls;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawCube(GroundCheckPos.position, GroundCheckSize);

        // Визуализация зоны проверки лестницы
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(GroundCheckPos.position, GroundCheckSize);
    }

    // Остальные методы остаются без изменений
    public void Shoot()
    {

    }

    public void TakeDamage(float damage)
    {
        health -= (int)(damage);
    }

    private void Heal(int amount)
    {
        health += amount;
        if (health > maxHealth) { health = maxHealth; }
    }

    private void SpeedUp(float multiplyer) =>
        StartCoroutine(SpeedUpCorutine(multiplyer));

    private IEnumerator SpeedUpCorutine(float multiplyer)
    {
        speedMultipluer = multiplyer;
        yield return new WaitForSeconds(15f);
        speedMultipluer = 1.0f;
    }

    private void IncreaseDamage(float multiplyer) => StartCoroutine(IncreaseDamageCoroutine(multiplyer));
    private IEnumerator IncreaseDamageCoroutine(float multiplyer)
    {
        Bullet.mul = multiplyer;
        yield return new WaitForSeconds(10);
        Bullet.mul = 1.0f;
    }
}