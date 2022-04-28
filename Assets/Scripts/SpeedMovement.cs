using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SpeedMovement : MonoBehaviour
{
    public KeyCode upKey = KeyCode.UpArrow;
    public KeyCode downKey = KeyCode.DownArrow;
    public KeyCode leftKey = KeyCode.LeftArrow;
    public KeyCode rightKey = KeyCode.RightArrow;
    public float duration = 0.25f;
    public int moveDistance = 2;

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
