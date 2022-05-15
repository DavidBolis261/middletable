using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuBG : MonoBehaviour
{
    private Camera cam;
    private float currentHue = 0.0f;
    public float cycleTime = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        ShiftColors();
    }

    void ShiftColors()
    {
        currentHue = (Time.timeSinceLevelLoad % cycleTime) / cycleTime;
        Color newCol = Color.HSVToRGB(currentHue, 0.5f, 0.5f);
        cam.backgroundColor = newCol;
    }
}
