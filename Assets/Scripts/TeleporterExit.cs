using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleporterExit : MonoBehaviour
{
    public bool objectOnTelep = false;
    // Start is called before the first frame update
    void OnTriggerStay2D(Collider2D collider)
    {
        //Debug.Log(collider.transform);
        objectOnTelep = true;
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        objectOnTelep = false;
    }
}
