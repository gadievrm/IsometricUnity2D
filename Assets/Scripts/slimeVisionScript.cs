using UnityEngine;

public class SlimeVisionScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public SpriteRenderer sr;

    [HideInInspector]
    public bool detected = false;

    public float moveSpeed = 3f; // Скорость движения
    public float moveInterval = 1f; // Интервал перемещения
    public int health = 100;

    public SlimeScript mainScript;

    public float patrolRadius = 5f; // Радиус патрулирования
    public int numPatrolPositions = 5; // Количество позиций для патрулирования
    private Vector2[] patrolPositions; // Массив позиций для патрулирования
    private int currentPatrolIndex = 0; // Индекс текущей позиции для патрулирования
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
                // Если достигли текущей позиции патрулирования, выбираем следующую
                if (timer >= moveInterval)
                {
                    currentPatrolIndex = Random.Range(0, numPatrolPositions);
                    Debug.Log(currentPatrolIndex);
                }


                // Направляемся к текущей позиции патрулирования
                Vector2 direction = (patrolPositions[currentPatrolIndex] - (Vector2)transform.position).normalized;
                rb.AddForce(direction * moveSpeed);
            }

            timer = 0f; // Сбрасываем таймер
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
            float angle = Random.Range(0f, Mathf.PI * 2f); // Рандомный угол
            float x = transform.position.x + patrolRadius * Mathf.Cos(angle); // Расчет координаты x
            float y = transform.position.y + patrolRadius * Mathf.Sin(angle); // Расчет координаты y
            Vector2 position = new Vector2(x, y); // Создание позиции
            patrolPositions[i] = position; // Сохранение позиции в массив
        }
    }
}
