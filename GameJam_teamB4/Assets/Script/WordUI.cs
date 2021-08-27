using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordUI : MonoBehaviour
{
    // �̱���
    private static WordUI instance;
    public static WordUI Instance
    {
        get
        {
            if (instance == null)
            {
                var obj = FindObjectOfType<WordUI>();
                if (obj == null)
                {
                    obj = new GameObject("UIMgr").AddComponent<WordUI>();
                }
                instance = obj;
            }
            return instance;
        }

        private set
        {
            Instance = value;
        }
    }

    public GameObject[] words;

    int wordCount = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void getWord(int index)
    {
        if (words[index].activeSelf)
            return;

        words[index].SetActive(true);
        wordCount++;

        if (wordCount >= 5)
        {
            stageClear();
        }
    }

    private void stageClear()
    {
        Debug.Log("�������� Ŭ����");
    }
}