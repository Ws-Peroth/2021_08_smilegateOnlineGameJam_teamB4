using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpUIFollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform player;

    void Update()
    {
        transform.localPosition = player.localPosition + new Vector3(0, 130, 0);
    }
}
