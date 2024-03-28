using UnityEngine;

public class ZombiePart : MonoBehaviour
{
    [SerializeField] private float zombieSensitive;
    [SerializeField] private Zombie zombie;
    [SerializeField] private HingeJoint2D hingeJoint2D;
 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.relativeVelocity.magnitude >zombieSensitive)
        {
            zombie.Hit();
        }
    }
}