using UnityEngine;

public class SlimeScript : MonoBehaviour
{
    public float moveSpeed = 3f; // Скорость движения
    public float moveInterval = 1f; // Интервал перемещения
    public int health = 100;
    public float knockbackForce = 10f;
    [HideInInspector]
    public bool damaged;

    private Vector2 currentDirection; // Текущее направление движения
    [HideInInspector]
    public bool playerDetected; // Флаг обнаружения игрока

    private float timer; // Таймер для отслеживания интервала перемещения
    private float hitTimer;
    private float deathTimer;

    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private Animator anim;
    public Transform player;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("playerSword"))
        {
            damaged = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("playerSword"))
        {
            damaged = false;
        }
    }

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (rb.velocity.x < 0)
            sr.flipX = true;
        else if (rb.velocity.x > 0)
            sr.flipX = false;
        timer += Time.deltaTime;
        if (timer >= moveInterval)
        {
            if (playerDetected)
            {
                currentDirection = (player.position - transform.position).normalized;
            }
            rb.AddForce(currentDirection * moveSpeed);

            timer = 0f; // Сбрасываем таймер
        }

        if (damaged)
        {
            Hit();
            Debug.Log("Hit");
        }
    }

    private void Hit()
    {
        if (health <= 0)
        {
            anim.SetBool("Dead", true);
        }
        else if (health > 0 && Time.time - hitTimer > 1f)
        {
            health -= 20;
            Vector2 knockbackDirection = transform.position - player.position;
            knockbackDirection = knockbackDirection.normalized;
            rb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
            hitTimer = Time.time;
            anim.SetBool("Hit", true);
        }

        anim.SetBool("Hit", false);
    }

    public void Death()
    {
        Destroy(gameObject);
    }
}
