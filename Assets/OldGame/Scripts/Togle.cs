using UnityEngine;
using UnityEngine.Events;

public class Togle : MonoBehaviour
{
    [SerializeField] private BoxCollider2D boxcollider2D;

    public UnityEvent action;

    private bool isOpen = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        action.Invoke();
        Transform childTransform = transform.GetChild(0).transform;

        if (!isOpen)
        {
            childTransform.localEulerAngles = new Vector3(0, 0, 30);
            boxcollider2D.offset = new Vector2(-0.21f, 0.27f);
            isOpen = true;
        }
        else if (isOpen)
        {
            childTransform.localEulerAngles = new Vector3(0, 0, -30);
            boxcollider2D.offset = new Vector2(0.21f, 0.27f);

            isOpen = false;
        }
    }
}