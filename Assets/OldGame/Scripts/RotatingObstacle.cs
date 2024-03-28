using UnityEngine;

public class RotatingObstacle : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Transform objectTransform;

    private void Update()
    {
        objectTransform.Rotate(new Vector3(0, 0, rotationSpeed * Time.deltaTime));
    }
}