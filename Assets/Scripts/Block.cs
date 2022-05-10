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

    public bool Move(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1.0f);
        Debug.Log(hit);
        // Hits nothing
        if(hit.collider == null || hit.collider.isTrigger)
        {
            previousPosition = transform.position;
            Vector2 v2Pos = transform.position;
            Vector2 target = v2Pos + direction;
            transform.DOMove(target, duration, false);
            return false;
        }
        return true;
    }
}
