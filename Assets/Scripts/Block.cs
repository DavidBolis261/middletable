using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Block : MonoBehaviour
{
    public float duration = 0.1f;
    private Vector2 previousPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool Move(Vector3 direction)
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, direction, out hit, 1.0f);
        Debug.Log(hit);
        // Hits nothing
        if(hit.collider == null || hit.collider.isTrigger)
        {
            previousPosition = transform.position;
            transform.DOMove(transform.position + direction, duration, false);
            return false;
        }
        return true;
    }
}
