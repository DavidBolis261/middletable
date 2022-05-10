using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Exit : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> requiredItems = new List<GameObject>();
    [SerializeField]
    private List<GameObject> players = new List<GameObject>();
    private BoxCollider2D bc;
    // Start is called before the first frame update
    void Start()
    {
        bc = GetComponent<BoxCollider2D>();
        bc.enabled = false;
        for(int i = 0; i < transform.childCount; i++) 
        {
            GameObject child = transform.GetChild(i).gameObject;
            if(child.tag == "Required Item")
                requiredItems.Add(child);
        }
        GameObject[] p = GameObject.FindGameObjectsWithTag("Player");
        for(int i = 0; i < p.Length; i++)
        {
            players.Add(p[i]);
        }
        Check();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Check()
    {
        bool allObtained = true;
        foreach(GameObject gO in requiredItems)
        {
            if(gO.activeSelf)
            {
                allObtained = false;
                break;
            }
        }
        if(allObtained)
        {
            //Debug.Log("Required Items has been obtained");
            bc.enabled = true;
        }
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        if(collider.gameObject.tag == "Player" && !DOTween.IsTweening(collider.transform))
        {
            Debug.Log("Player has exited");
            players.Remove(collider.gameObject);
            collider.gameObject.GetComponent<Movement>().moveTimer = 999.0f;
            collider.gameObject.SetActive(false);
            if(players.Count == 0)
            {
                Debug.Log("Level Completed");
                StatusManager.instance.LevelCompleted();
            }
        }
    }
}
