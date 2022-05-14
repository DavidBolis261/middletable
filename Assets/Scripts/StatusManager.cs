using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class StatusManager : MonoBehaviour
{
    public static StatusManager instance;

    private int movesDone = 0;

    private List<Transform> players = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        foreach(GameObject p in GameObject.FindGameObjectsWithTag("Player"))
        {
            players.Add(p.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R)) Reset();
        if(Input.GetKeyDown(KeyCode.Escape)) SceneManager.LoadScene("Main Menu");
    }

    public void LevelCompleted()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void Reset()
    {
        foreach(Transform p in players)
        {
            if(DOTween.IsTweening(p.transform))
                return;
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
