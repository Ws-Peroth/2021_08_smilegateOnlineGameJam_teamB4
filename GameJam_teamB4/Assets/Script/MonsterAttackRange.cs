using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttackRange : MonoBehaviour
{
    public Monster monster;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SetRange();
    }

    private void SetRange()
    {
        if (monster.monsterSpriteRenderer.flipX)
        {
            transform.localPosition = new Vector3(-0.5f, 0, 0);
        }
        else
        {
            transform.localPosition = new Vector3(0.5f, 0, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            monster.OnAttackRange(collision);
        }
    }
}
