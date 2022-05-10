using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public bool isPressed = false;
    public bool keepPressed = false;

    [SerializeField]
    private Gate gate;
    private SpriteRenderer sr;
    private Color defaultColor;

    // Start is called before the first frame update
    void Start()
    {
        gate = transform.parent.gameObject.GetComponent<Gate>();
        sr = GetComponent<SpriteRenderer>();
        if(keepPressed)
        {
            sr.color = sr.color * new Color(1.0f, 0.75f, 0.65f, 1.0f);  
        }
            defaultColor = sr.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if((collider.gameObject.tag == "Player" || collider.gameObject.tag == "Block") && !isPressed)
        {
            //Debug.Log("Pressure Plate pressed");
            sr.color = defaultColor * new Color(0.75f, 0.75f, 0.75f, 1.0f);
            isPressed = true;
            gate.Check();
        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if(keepPressed)
        {
            sr.color = defaultColor;
            isPressed = false;
            gate.Check();
        }
    }
}
