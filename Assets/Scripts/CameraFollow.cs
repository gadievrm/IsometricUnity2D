using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // ������ �� ��������� ������
    public float smoothTime = 0.3f; // ����� ����������� �������� ������
    public Vector3 offset; // �������� ������ ������������ ������

    private Vector3 velocity = Vector3.zero;
    private bool shouldFollow = false;

    private void Update()
    {
        if (target != null && target.position != transform.position && !shouldFollow)
        {
            shouldFollow = true;
            Invoke("StartFollowing", 1.0f); // �������� ��������� �� ������� ����� 1 �������
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
