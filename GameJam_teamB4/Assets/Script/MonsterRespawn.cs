using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterRespawn : MonoBehaviour
{
    public Transform[] respawnPos;
    public GameObject[] monsters;

    // Start is called before the first frame update
    void Start()
    {
        for(int index = 0; index < respawnPos.Length;index++)
        {
            StartCoroutine(RespawnMonster(respawnPos[index], 0f));
        }
        
    }

    //// Update is called once per frame
    //void Update()
    //{
        
    //}


    public void respawnMonster(Transform pos)
    {
        StartCoroutine(RespawnMonster(pos, 3f));
    }

    IEnumerator RespawnMonster(Transform pos, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        if (pos.childCount > 1)
            yield break;

        GameObject newMonster = Instantiate(monsters[getMonsterIndex()]);
        newMonster.transform.parent = pos;
        newMonster.transform.position = pos.position;
        newMonster.SetActive(true);
    }

    int getMonsterIndex()
    {
        return Random.Range(0, monsters.Length);
    }
}
