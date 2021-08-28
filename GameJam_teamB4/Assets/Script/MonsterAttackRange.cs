using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttackRange : MonoBehaviour
{
    public Monster monster;
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
    }

    private void SetRange()
    {
        if (monster.dir.x >= 0)
        {
            transform.localPosition = new Vector3(1.25f, 0, 0);
        }
        else
        {
            transform.localPosition = new Vector3(0, 0, 0);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!attackDelayOn)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                attackDelayOn = true;

                Debug.Log("Find Player");
                monster.OnAttackRange(collision);

                StartCoroutine(DelayFinish());
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
            monster.target = null;
            //CancelInvoke("Shoot");
            monster.Think();
        }
    }
}
