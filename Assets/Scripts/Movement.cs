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
    public int maxDistance = 2;
    public bool canPush = false;

    public GameObject otherPlayer;   // Other player's transform so that only one character can move at a time (god unity is pain)
    private Vector2 previousPosition;
    private Vector2 positionSnapping;
    // To avoid collision issues
    private float moveCoolDown = 0.25f;
    public float moveTimer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        positionSnapping = transform.position;
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
        if(!DOTween.IsTweening(transform) && !DOTween.IsTweening(otherPlayer.transform) && moveTimer >= moveCoolDown && otherPlayer.GetComponent<Movement>().moveTimer >= otherPlayer.GetComponent<Movement>().moveCoolDown)
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

    void MoveHelper(Vector2 direction)
    {
        transform.DOMove((previousPosition + (direction * maxDistance)), duration, false).SetEase(Ease.OutSine);
        moveTimer = 0.0f;
    }

    void OnCollisionEnter(Collision collision) 
    {
        //Debug.Log("Collision Enter");
        // On collision
        if(DOTween.IsTweening(transform) && !DOTween.IsTweening(otherPlayer.transform))
        {
            if(!((canPush && collision.gameObject.tag == "Strength Only Access") || (!canPush && collision.gameObject.tag == "Speed Only Access")))
            {
                
                bool blockDoesntMove = true;
                // Collision on blocks and can push
                if(canPush && collision.gameObject.tag == "Block")
                {
                    //Debug.Log("Block");
                    GameObject blockGO = collision.gameObject;
                    if(!DOTween.IsTweening(blockGO.transform))
                    {
                        Block b = blockGO.GetComponent<Block>();
                        if(b == null) return; 
                        if(transform.position.x < previousPosition.x)
                        {
                            blockDoesntMove = b.Move(Vector3.left);
                        }
                        else if(transform.position.x > previousPosition.x)
                        {
                            blockDoesntMove = b.Move(Vector3.right);
                        }

                        if(transform.position.y < previousPosition.y)
                        {
                            blockDoesntMove = b.Move(Vector3.down);
                        }
                        else if(transform.position.y > previousPosition.y)
                        {
                            blockDoesntMove = b.Move(Vector3.up);
                        }
                    }
                }
                // Everything else
                if (blockDoesntMove)
                {
                    positionSnapping = transform.position;
                    transform.DOPause();
                    transform.DOKill(false);
                    if(transform.position.x < previousPosition.x)
                    {
                        Debug.Log("Snapped");
                        positionSnapping.x = Mathf.Ceil(transform.position.x);
                    }
                    else if(transform.position.x > previousPosition.x)
                    {
                        Debug.Log("Snapped");
                        positionSnapping.x = Mathf.Floor(transform.position.x);
                    }

                    if(transform.position.y < previousPosition.y)
                    {
                        Debug.Log("Snapped");
                        positionSnapping.y = Mathf.Ceil(transform.position.y);
                    }
                    else if(transform.position.y > previousPosition.y)
                    {
                        Debug.Log("Snapped");
                        positionSnapping.y = Mathf.Floor(transform.position.y);
                    }
                    transform.position = positionSnapping;
                    
                }
            }
        }
    }

    
}
