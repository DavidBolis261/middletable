using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Gate : MonoBehaviour
{
    [SerializeField]
    private List<PressurePlate> pressurePlates = new List<PressurePlate>();
    private BoxCollider bc;
    private SpriteRenderer sr;
    private float gateSpriteAlpha = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        bc = GetComponent<BoxCollider>();
        sr = GetComponent<SpriteRenderer>();
        for(int i = 0; i < transform.childCount; i++) 
        {
            PressurePlate p = transform.GetChild(i).gameObject.GetComponent<PressurePlate>();
            pressurePlates.Add(p);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Check()
    {
        bool allTriggered = true;
        foreach(PressurePlate p in pressurePlates)
        {
            if(!p.isPressed)
            {
                allTriggered = false;
                break;
            }
        }
        if(allTriggered)
        {
            Debug.Log("ALL TRIGGERS PRESSED, GATE OPENED");
            bc.enabled = false;
            gateSpriteAlpha = 0.0f;
        }
        else
        {
            Debug.Log("GATE NOT OPENED");
            bc.enabled = true;
            gateSpriteAlpha = 1.0f;
        }
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, gateSpriteAlpha);
    }
}
