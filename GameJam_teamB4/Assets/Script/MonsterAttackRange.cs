using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttackRange : MonoBehaviour
{
    public Monster monster;
    public Collider2D target;
    private bool attackDelayOn;

    // Start is called before the first frame update
    void Start()
    {
        attackDelayOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        SetRange();

        if (!attackDelayOn && target != null)
        {
            Debug.Log("Find Player");
            attackDelayOn = true;
            monster.OnAttackRange(target);

            StartCoroutine(DelayFinish());
        }
    }

    private void SetRange()
    {
        if (monster.dir.x >= 0)
        {
            transform.localPosition = new Vector3(0.33f, 0, 0);
        }
        else
        {
            transform.localPosition = new Vector3(-0.33f, 0, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!attackDelayOn)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                target = collision;
            }
        }
    }

    private IEnumerator DelayFinish()
    {
        yield return new WaitForSeconds(monster.attackDelay);
        attackDelayOn = false;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            target = null;
            monster.target = null;
            //CancelInvoke("Shoot");
            monster.Think();
        }
    }
}
