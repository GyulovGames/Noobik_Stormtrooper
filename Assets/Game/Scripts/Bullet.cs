using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    [SerializeField] private Rigidbody2D rigidBody;

    private void OnEnable()
    {
        rigidBody.velocity = transform.right * bulletSpeed;
    }
}