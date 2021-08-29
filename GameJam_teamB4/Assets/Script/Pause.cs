using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject screen;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!screen.activeSelf)
                OpenScreen();
            else 
                CloseScreen();
        }
    }

    public void OpenScreen()
    {
        Time.timeScale = 0;
        screen.SetActive(true);
    }

    public void CloseScreen()
    {
        Time.timeScale = 1;
        screen.SetActive(false);
    }
}
