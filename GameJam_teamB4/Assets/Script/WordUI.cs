using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordUI : MonoBehaviour
{
    // ½Ì±ÛÅæ
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
    public AudioSource audioSource;

    public int wordCount = 0;



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
        Debug.Log("Get Word");

        if (words[index].activeSelf)
        {
            for(int i = 0; i < words.Length; i++)
            {
                if (!(words[i].activeSelf)) index = i;
            }
        }

        words[index].SetActive(true);
        wordCount++;

        audioSource.Play();

        if (wordCount >= 5)
        {
            stageClear();
        }
    }

    private void stageClear()
    {
        Debug.Log("½ºÅ×ÀÌÁö Å¬¸®¾î");
    }
}
