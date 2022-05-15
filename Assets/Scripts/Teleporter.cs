using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Teleporter : MonoBehaviour
{
    private Vector3 teleporterExitPosition;
    private bool attemptedTele = false;
    private TeleporterExit teleporterExit;

    // Start is called before the first frame update
    void Start()
    {
        if(transform.childCount == 1)
        {
            teleporterExitPosition = transform.GetChild(0).position;
            teleporterExit = transform.GetChild(0).gameObject.GetComponent<TeleporterExit>();
        }
        else
            Debug.LogError("Please put Teleporter exit as child :)");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Player") && !DOTween.IsTweening(collider.transform))
        {
            if( !teleporterExit.objectOnTelep && !attemptedTele)
                collider.transform.position = teleporterExitPosition;
            else
                attemptedTele = true;
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Player"))
            attemptedTele = false;
    }
}
