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
    // To avoid collision issues
    private float moveCoolDown = 0.25f;
    public float moveTimer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
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
        Vector3 contact = collision.GetContact(0).point;
        Debug.Log("Contact point: " + contact);
        //Debug.Log("Collision Enter");
        // On collision
        if(DOTween.IsTweening(transform) && !DOTween.IsTweening(otherPlayer.transform))
        {
            Debug.Log("Collision detected");
            if(!((canPush && collision.gameObject.tag == "Strength Only Access") || (!canPush && collision.gameObject.tag == "Speed Only Access")))
            {
                Debug.Log("Collision detected confirmed");
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
                        if(transform.position.x > contact.x && b.transform.position.x < transform.position.x)
                        {
                            blockDoesntMove = b.Move(Vector3.left);
                        }
                        else if(transform.position.x < contact.x && b.transform.position.x > transform.position.x)
                        {
                            blockDoesntMove = b.Move(Vector3.right);
                        }

                        if(transform.position.y > contact.y && b.transform.position.y < transform.position.y)
                        {
                            blockDoesntMove = b.Move(Vector3.down);
                        }
                        else if(transform.position.y < contact.y && b.transform.position.y > transform.position.y)
                        {
                            blockDoesntMove = b.Move(Vector3.up);
                        }
                        Debug.Log("B" + blockDoesntMove);
                    }
                }
                // Everything else
                if (blockDoesntMove)
                {
                    transform.DOPause();
                    Vector2 positionSnapping = transform.position;
                    Debug.Log("PrevPos " + previousPosition.x + " " + previousPosition.y);
                    Debug.Log("Snapping " + positionSnapping.x + " " + positionSnapping.y);
                    if(transform.position.x > contact.x)
                    {
                        positionSnapping.x = Mathf.Ceil(transform.position.x);
                    }
                    else if(transform.position.x < contact.x)
                    {
                        positionSnapping.x = Mathf.Floor(transform.position.x);
                    }

                    if(transform.position.y > contact.y)
                    {
                        positionSnapping.y = Mathf.Ceil(transform.position.y);
                    }
                    else if(transform.position.y < contact.y)
                    {
                        positionSnapping.y = Mathf.Floor(transform.position.y);
                    }
                    transform.position = positionSnapping;
                    transform.DOKill(false);
                }
            }
        }
    }

    
}
