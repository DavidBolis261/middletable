using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Movement : MonoBehaviour
{
    public KeyCode upKey = KeyCode.UpArrow;
    public KeyCode downKey = KeyCode.DownArrow;
    public KeyCode leftKey = KeyCode.LeftArrow;
    public KeyCode rightKey = KeyCode.RightArrow;
    public float duration = 0.25f;
    public int moveDistance = 2;
    public bool canPush = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        Vector3 previousPosition = transform.position;
        if(!DOTween.IsTweening(transform))
        {
                if (Input.GetKeyDown(upKey))
            {   // Up movement
               MoveHelper(Vector2.up);
            }
            else if (Input.GetKeyDown(downKey))
            {   // Down movement
                MoveHelper(Vector2.down);
            }
            else if (Input.GetKeyDown(leftKey))
            {   // Left movement
                MoveHelper(Vector2.left);
            }
            else if (Input.GetKeyDown(rightKey))
            {   // Right movement
                MoveHelper(Vector2.right);
            }
        }
    }

    void MoveHelper(Vector2 direction) {
        Vector2 previousPosition = transform.position;
        transform.DOMove((previousPosition + (direction * moveDistance)), duration, false).SetEase(Ease.OutSine);
    }

    
}
