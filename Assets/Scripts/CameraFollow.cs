using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Ссылка на трансформ игрока
    public float smoothTime = 0.3f; // Время сглаживания движения камеры
    public Vector3 offset; // Смещение камеры относительно игрока

    private Vector3 velocity = Vector3.zero;
    private bool shouldFollow = false;

    private void Update()
    {
        if (target != null && target.position != transform.position && !shouldFollow)
        {
            shouldFollow = true;
            Invoke("StartFollowing", 1.0f); // Начинаем следовать за игроком через 1 секунду
        }
    }

    private void LateUpdate()
    {
        if (shouldFollow)
        {
            Vector3 targetPosition = target.position + offset;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
    }

    private void StartFollowing()
    {
        shouldFollow = true;
    }
}
