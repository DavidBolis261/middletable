using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public struct KeyBind
{
    public KeyCode Code;
    public Vector2 Direction;

    public KeyBind(KeyCode Code, Vector2 Direction)
    {
        this.Code = Code;
        this.Direction = Direction;
    }
}

public class Movement : MonoBehaviour
{
    public KeyCode upKey = KeyCode.UpArrow;
    public KeyCode downKey = KeyCode.DownArrow;
    public KeyCode leftKey = KeyCode.LeftArrow;
    public KeyCode rightKey = KeyCode.RightArrow; 
    private KeyBind[] keyBinds;
    public float duration = 0.25f;
    public float moveTimer = 0.0f;

    public int maxDistance = 2;
    public bool canPush = false;

    public GameObject otherPlayer;   // Other player's transform so that only one character can move at a time (god unity is pain)
    private Vector2 previousPosition;

    // Start is called before the first frame update
    void Start()
    {
        keyBinds = new KeyBind[] {
            new KeyBind(upKey, Vector2.up),
            new KeyBind(downKey, Vector2.down),
            new KeyBind(leftKey, Vector2.left),
            new KeyBind(rightKey, Vector2.right)
        };
        previousPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        moveTimer += Time.deltaTime;
    }

    void Move()
    {
        previousPosition = transform.position;
        if(!DOTween.IsTweening(transform) && !DOTween.IsTweening(otherPlayer.transform) && moveTimer >= duration && otherPlayer.GetComponent<Movement>().moveTimer >= otherPlayer.GetComponent<Movement>().duration)
        {
            foreach(KeyBind kb in keyBinds)
            {
                if(Input.GetKeyDown(kb.Code))
                {
                    MoveHelper(kb.Direction);
                }
            }
        }
    }

    void MoveHelper(Vector2 direction)
    {
        transform.DOMove((previousPosition + ComputeMoveDistance(direction)), duration, false).SetEase(Ease.OutSine);
        moveTimer = 0.0f;
    }

    Vector2 ComputeMoveDistance(Vector2 direction)
    {
        RaycastHit hit;
        float distanceTraveled = maxDistance;
        while(distanceTraveled > 0.0f)
        {   
            // No Collision
            if(!Physics.Raycast(transform.position, direction, out hit, distanceTraveled))
            {
                return direction * distanceTraveled;
            }
            else
            {
                Debug.Log(hit.distance);
                // Collider of target is trigger
                if(hit.collider == null || hit.collider.isTrigger)
                {
                    if(hit.distance > (distanceTraveled - 1))
                        return direction * distanceTraveled;
                }

                // Collider of target is Block and player is strength cube
                if(hit.collider.gameObject.tag == "Block" && canPush)
                {
                    if(!hit.collider.gameObject.GetComponent<Block>().Move(direction))
                        if(hit.distance > (distanceTraveled - 1))
                            return direction * distanceTraveled;
                }

                // Exclusive access
                if((canPush && hit.collider.gameObject.tag == "Strength Only Access") || (!canPush && hit.collider.gameObject.tag == "Speed Only Access"))
                {
                    if(hit.distance > (distanceTraveled - 1))
                        return direction * distanceTraveled;

                    if(!Physics.Raycast(hit.collider.transform.position, direction, hit.distance + 1))
                    {
                        return direction * distanceTraveled;
                    }
                }
            }

            distanceTraveled -= 1.0f;
        }
        return Vector2.zero;
    }
}
