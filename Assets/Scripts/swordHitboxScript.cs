using UnityEngine;

public class SwordHitboxScript : MonoBehaviour
{
    private PlayerController mainPlayerScript;

    private void Start()
    {
        mainPlayerScript = GetComponentInParent<PlayerController>();
    }

    private void Update()
    {
        float rotationY = mainPlayerScript.sr.flipX ? -180f : 0f;
        transform.rotation = Quaternion.Euler(transform.rotation.x, rotationY, transform.rotation.z);
    }
}
