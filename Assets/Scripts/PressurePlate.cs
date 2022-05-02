using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public bool isPressed = false;
    public bool keepPressed = false;

    [SerializeField]
    private Gate gate;
    // Start is called before the first frame update
    void Start()
    {
        gate = transform.parent.gameObject.GetComponent<Gate>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider collider)
    {
        Debug.Log("Pressure Plate pressed");
        if((collider.gameObject.tag == "Player" || collider.gameObject.tag == "Block") && !isPressed)
        {
            isPressed = true;
            gate.Check();
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if(keepPressed)
        {
            isPressed = false;
            gate.Check();
        }
    }
}
