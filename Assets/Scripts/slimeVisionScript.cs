using UnityEngine;

public class SlimeVisionScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public SpriteRenderer sr;

    [HideInInspector]
    public bool detected = false;

    public float moveSpeed = 3f; // �������� ��������
    public float moveInterval = 1f; // �������� �����������
    public int health = 100;

    public SlimeScript mainScript;

    public float patrolRadius = 5f; // ������ ��������������
    public int numPatrolPositions = 5; // ���������� ������� ��� ��������������
    private Vector2[] patrolPositions; // ������ ������� ��� ��������������
    private int currentPatrolIndex = 0; // ������ ������� ������� ��� ��������������
    private float timer;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            detected = true;
        }
    }

    private void Start()
    {
        GeneratePatrolPositions();
    }

    private void Update()
    {
        Debug.Log(currentPatrolIndex);
        if (rb.velocity.x < 0)
            sr.flipX = true;
        else if (rb.velocity.x > 0)
            sr.flipX = false;

        timer += Time.deltaTime;
        if (timer >= 2 * moveInterval)
        {
            if (!detected)
            {
                // ���� �������� ������� ������� ��������������, �������� ���������
                if (timer >= moveInterval)
                {
                    currentPatrolIndex = Random.Range(0, numPatrolPositions);
                    Debug.Log(currentPatrolIndex);
                }


                // ������������ � ������� ������� ��������������
                Vector2 direction = (patrolPositions[currentPatrolIndex] - (Vector2)transform.position).normalized;
                rb.AddForce(direction * moveSpeed);
            }

            timer = 0f; // ���������� ������
        }

        if (detected)
        {
            mainScript.playerDetected = true;
            Destroy(gameObject);
        }
    }

    private void GeneratePatrolPositions()
    {
        patrolPositions = new Vector2[numPatrolPositions];

        for (int i = 0; i < numPatrolPositions; i++)
        {
            float angle = Random.Range(0f, Mathf.PI * 2f); // ��������� ����
            float x = transform.position.x + patrolRadius * Mathf.Cos(angle); // ������ ���������� x
            float y = transform.position.y + patrolRadius * Mathf.Sin(angle); // ������ ���������� y
            Vector2 position = new Vector2(x, y); // �������� �������
            patrolPositions[i] = position; // ���������� ������� � ������
        }
    }
}
