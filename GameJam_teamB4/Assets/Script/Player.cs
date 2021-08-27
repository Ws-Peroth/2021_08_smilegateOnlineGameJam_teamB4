using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] Slider hpBar;
    public float hp = 100;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hpBar.value = hp;
    }
}
