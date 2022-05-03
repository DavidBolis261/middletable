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
    private BoxCollider bc;
    // Start is called before the first frame update
    void Start()
    {
        bc = GetComponent<BoxCollider>();
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
            Debug.Log("Required Items has been obtained");
            bc.enabled = true;
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Player")
        {
            Debug.Log("Player has exited");
            players.Remove(collider.gameObject);
            collider.transform.DOPause();
            collider.transform.DOKill(false);
            collider.gameObject.GetComponent<Movement>().moveTimer = 999.0f;
            collider.gameObject.SetActive(false);
            if(players.Count == 0)
            {
                Debug.Log("Level Completed");
            }
        }
    }
}
