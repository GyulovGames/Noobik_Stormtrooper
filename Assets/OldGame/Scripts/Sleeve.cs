using UnityEngine;

public class Sleeve : MonoBehaviour
{
    

    private void OnEnable()
    {
        Vector3 forceDirection;
        forceDirection.y = Random.Range(4.5f, 5.5f);
        forceDirection.x = Random.Range(-1f, -2f);
        forceDirection.z = Random.Range(0.01f, 0.05f);

        Rigidbody2D rigidBody2D = GetComponent<Rigidbody2D>();
        rigidBody2D.AddForce(forceDirection, ForceMode2D.Impulse);
        rigidBody2D.AddTorque(forceDirection.z, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = 1;
        Sleeve sleeve = GetComponent<Sleeve>();
        sleeve.enabled = false;
    }
}