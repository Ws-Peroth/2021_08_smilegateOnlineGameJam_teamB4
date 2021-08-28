using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    float thinkTime = 2f;
    public Vector3 dir = Vector3.left;
    public int hp = 100;
    public Animator animator;
    public GameObject target;
    public bool attack = false;

    // Start is called before the first frame update
    void Start()
    {
        Think();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 scale = new Vector3(-dir.x, 1, 1);

        if (scale.x == 0)
            scale.x = this.transform.localScale.x;
        this.transform.localScale = scale;

        checkFront();
        if (!attack)
        {
            Move();
        }
        else
        {
            Vector3 distance = this.transform.position - target.transform.position;
            if ( distance.x < 0)
            {
                scale.x = -1;
            }
            else
            {
                scale.x = 1;
            }

            animator.SetTrigger("tryAttack");
        }

        this.transform.localScale = scale;
    }

    private void checkFront()
    {
        Vector2 ray = new Vector2(this.transform.position.x + (dir.x * 0.5f), this.transform.position.y);
        Debug.DrawRay(ray, Vector3.down, Color.green);
        RaycastHit2D raycast = Physics2D.Raycast(ray, Vector3.down, 1, LayerMask.GetMask("Ground"));

        if (raycast.collider == null)
        {
            dir.x *= -1;
        }
    }

    private void Move()
    {
        this.transform.position += dir * Time.deltaTime;

        // set animation
        if (dir.x == 0)
        {
            animator.SetBool("isMoving", false);
        }
        else
        {
            animator.SetBool("isMoving", true);
        }

    }

    private void Think()
    {
        thinkTime = Random.Range(1f, 3f);

        dir.x = Random.Range(-1, 2);
        dir.y = 0;
        dir.z = 0;

        Invoke("Think", thinkTime);
    }

    public void getDamage(int damage)
    {
        hp -= damage;

        if (hp <= 0)
        {
            // 50% È®·ü·Î ±ÛÀÚ Å‰µæ.
            if (Random.Range(0, 2) == 1)
            {
                WordUI.Instance.getWord(Random.Range(0, 6));
            }

            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            target = other.gameObject;
            CancelInvoke("Think");
            attack = true;
            //Attack();
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            target = null;
            //CancelInvoke("Shoot");
            attack = false;
            Think();
        }
    }
}
