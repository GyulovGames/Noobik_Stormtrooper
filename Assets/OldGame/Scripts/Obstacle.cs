using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float moveDistance = 1.0f; 
    [SerializeField] private float moveSpeed = 1.0f; 
    [SerializeField] private Transform objectTransform;

    private Vector3 startPos; 

    void Start()
    {
        startPos = transform.position; 
    }

    void Update()
    {
        float newYPos = startPos.y + Mathf.PingPong(Time.time * moveSpeed, moveDistance);
        objectTransform.position = new Vector3(transform.position.x, newYPos, transform.position.z);
    }
}