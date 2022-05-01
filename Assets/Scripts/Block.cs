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

    public void Move(Vector3 direction)
    {
        previousPosition = transform.position;
        transform.DOMove(transform.position + direction, duration, false);
    }
}
