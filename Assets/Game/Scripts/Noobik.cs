using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using YG;

public class Noobik : MonoBehaviour
{
    [SerializeField][Range(-45f, 45f)] private float minNechRotationAngle;
    [SerializeField][Range(-45f, 45f)] private float maxNechRotationAngle;
    [SerializeField][Range(-45f, 45f)] private float minSpineRotationAngle;
    [SerializeField][Range(-45f, 45f)] private float maxSpineRotationAngle;
    [Space(10)]
    [SerializeField] private Transform handTransform;
    [SerializeField] private Transform neckhTransform;
    [SerializeField] private Transform spineTransform;
    [SerializeField] private Transform leftSholderTransform;
    [SerializeField] private Transform rightSholderTransform;
    public GameObject bullet;
    public Transform gunTransform;

    private Vector3 mousePosition;
    public bool isFacingRight = true;

    private void Update()
    {
        GetMousePosition();

        BodyFlip();
        NeckhRotation();
        SpineRotation();
        LeftSholderRotation();
        RightSholderTransform();
    }

    private void GetMousePosition()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }


    private void NoobikShooting()
    {
        Instantiate(bullet, gunTransform.position, gunTransform.rotation);
    }

    private void NeckhRotation()
    {
        Vector3 direction = mousePosition - neckhTransform.position;
        float angle = Mathf.Atan2(direction.y * transform.localScale.y, direction.x * transform.localScale.x ) * Mathf.Rad2Deg;
        float clampedAngle = Mathf.Clamp(angle, minNechRotationAngle, maxNechRotationAngle);
        neckhTransform.localRotation = Quaternion.Euler(0, 0, clampedAngle);
    }
    private void SpineRotation()
    {
        Vector3 direction = mousePosition - spineTransform.position;
        float angle = Mathf.Atan2(direction.y * transform.localScale.y, direction.x * transform.localScale.x) * Mathf.Rad2Deg;
        float clampedAngle = Mathf.Clamp(angle, minSpineRotationAngle, maxSpineRotationAngle);
        spineTransform.localRotation = Quaternion.Euler(0, 0, clampedAngle);
    }
    private void LeftSholderRotation()
    {
        Vector3 direction = leftSholderTransform.position - handTransform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        angle -= 90f;
        leftSholderTransform.rotation = Quaternion.Euler(0, 0, angle);
    }
    private void RightSholderTransform()
    {
        Vector3 direction;
        float angle;

        if (isFacingRight)
        {
            direction = mousePosition - rightSholderTransform.position;
            angle = Mathf.Atan2(direction.y, direction.x) * Mathf .Rad2Deg;
            angle += 60f;
        }
        else
        {
            direction = rightSholderTransform.position - mousePosition;
            angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            angle -= 60f;
        }
       
        rightSholderTransform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void BodyFlip()
    {
        if(isFacingRight && mousePosition.x < transform.position.x || !isFacingRight && mousePosition.x > transform.position.x)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}