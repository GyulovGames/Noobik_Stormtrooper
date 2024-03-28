using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class Piston : MonoBehaviour
{
    [SerializeField] private bool stockOut;
    [Space(10)]
    [SerializeField] private WheelJoint2D wheelJoint2D;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameObject stockTail;
    [Space(10)]
    [SerializeField] private AudioClip pistonOutSound;
    [SerializeField] private AudioClip pistonInSound;


    private void Start()
    {
        if (!YandexGame.savesData.sounds)
        {
            audioSource.volume = 0.0f;
        }


        if (stockOut)
        {
            wheelJoint2D.anchor = new Vector2(0, 0.25f);
            wheelJoint2D.connectedAnchor = new Vector2(0, -0.25f);
            stockTail.SetActive(true);            
        }
        else if (!stockOut)
        {
            wheelJoint2D.anchor = new Vector2(0, -0.25f);
            wheelJoint2D.connectedAnchor = new Vector2(0, 0.25f);
            stockTail.SetActive(false);
        }
    }



    public void PistonControll()
    {
        if (!stockOut)
        {
            stockOut = true;

            audioSource.clip = pistonOutSound;
            audioSource.Play();

            wheelJoint2D.anchor = new Vector2(0, 0.25f);
            wheelJoint2D.connectedAnchor = new Vector2(0, -0.25f);
            stockTail.SetActive(true);
        }
        else if(stockOut)
        {
            stockOut = false;

            audioSource.clip = pistonInSound;
            audioSource.Play();

            wheelJoint2D.anchor = new Vector2(0, -0.25f);
            wheelJoint2D.connectedAnchor = new Vector2(0, 0.25f);
            stockTail.SetActive(false);
        }
    }
}