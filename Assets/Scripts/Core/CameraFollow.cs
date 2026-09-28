using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset = new Vector3(0.0f, 0.0f, -10.0f);
    [SerializeField] private float followSpeed = 5.0f;

    private void LateUpdate()
    {
        Vector3 nextPos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, nextPos, followSpeed * Time.deltaTime);
    }
}
