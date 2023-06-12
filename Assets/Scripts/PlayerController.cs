using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public FixedJoystick joystick;
    public float moveSpeed;
    public float attackRange;
    public float attackTime;

    public SpriteRenderer sr;
    private Rigidbody2D rb;
    private Animator anim;
    public BoxCollider2D swordCollider;

    private Vector2 move;
    private bool isMoving;
    private bool isAttacking = false;
    private float attackStart;
    private bool hit = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        PlayerMovement();
        PlayerAnimation();
        PlayerAttack();
    }

    private void PlayerMovement()
    {
        move.x = joystick.Horizontal;
        move.y = joystick.Vertical;
        isMoving = move.magnitude > 0;

        if (move.x < 0)
            sr.flipX = true;
        else if (move.x > 0)
            sr.flipX = false;

        rb.velocity = move.normalized * moveSpeed;
    }

    private void PlayerAttack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && !isAttacking && !hit)
        {
            attackStart = Time.time;
            isAttacking = true;
        }

        if (Time.time - attackStart > attackTime)
            isAttacking = false;

        swordCollider.enabled = isAttacking;
    }

    private void PlayerAnimation()
    {
        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isAttacking", isAttacking);
    }
}
