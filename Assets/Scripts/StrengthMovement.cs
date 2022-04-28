using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StrengthMovement : MonoBehaviour
{
    public KeyCode upKey = KeyCode.W;
    public KeyCode downKey = KeyCode.S;
    public KeyCode leftKey = KeyCode.A;
    public KeyCode rightKey = KeyCode.D;
    public float duration = 0.25f;
    public int moveDistance = 1;

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
                transform.DOMoveY((previousPosition.y + moveDistance), duration, false).SetEase(Ease.OutSine);
            }
            else if (Input.GetKeyDown(downKey))
            {   // Down movement
                transform.DOMoveY((previousPosition.y - moveDistance), duration, false).SetEase(Ease.OutSine);
            }
            else if (Input.GetKeyDown(leftKey))
            {   // Left movement
                transform.DOMoveX(previousPosition.x - moveDistance, duration, false).SetEase(Ease.OutSine);
            }
            else if (Input.GetKeyDown(rightKey))
            {   // Right movement
                transform.DOMoveX(previousPosition.x + moveDistance, duration, false).SetEase(Ease.OutSine);
            }
        }
    }
}
