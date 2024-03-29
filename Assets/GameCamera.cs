using UnityEngine;
using YG;

public class GameCamera : MonoBehaviour
{
    [SerializeField] private float folowSpeed;
    [SerializeField] private Vector3 cameraOffset;

    public  Transform cameraTransform;
    public Transform carTransform;


    private void FixedUpdate()
    {
        Vector3 desiredPosition = carTransform.position + cameraOffset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, folowSpeed);
        transform.position = smoothPosition;
    }
}