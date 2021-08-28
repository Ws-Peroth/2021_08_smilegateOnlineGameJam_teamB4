using lws;
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
    public SpriteRenderer monsterSpriteRenderer;
    public bool attack = false;
    public float power = 10f;
    public float attackDelay = 1f;

    public bool isAttack;

    public GameObject HpObj = null;
    private int nowHp;
    private float HpObjLength;

    public AudioSource audioSource;

    public MonsterRespawn respawn;

    // Start is called before the first frame update
    void Start()
    {
        isAttack = false;
        Think();

        nowHp = hp;
        HpObjLength = HpObj.transform.localScale.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //checkFront();

        if (!attack)
        {
            Move();
        }
        else
        {
            Vector3 distance = this.transform.position - target.transform.position;

            if ( distance.x < 0)
            {
                monsterSpriteRenderer.flipX = true;
            }
            else
            {
                monsterSpriteRenderer.flipX = false;
            }

            target.GetComponent<Player>().GetDamage(power);
            Debug.Log("Attack Player!");
            animator.SetTrigger("tryAttack");

            attack = false;
        }
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

    public void Think()
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

        Debug.Log($"{gameObject.name} : Get Dmg");

        nowHp -= damage;
        Vector3 scale = HpObj.transform.localScale;
        scale.x -= (HpObjLength / hp) * damage;
        if (scale.x <= 0)
            scale.x = 0.1f;
        HpObj.transform.localScale = scale;

        if (hp <= 0)
        {
            // 50% È®·ü·Î ±ÛÀÚ Å‰µæ.
            if (Random.Range(0, 2) == 1)
            {
                WordUI.Instance.getWord(Random.Range(0, 5));
            }

            respawn.respawnMonster(this.transform.parent);
            Destroy(this.gameObject);
        }
    }

    public void OnAttackRange(Collider2D player)
    {
        target = player.gameObject;
        CancelInvoke("Think");
        attack = true;

        audioSource.Play();

        Debug.Log("attack to true");
    }

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.tag == "Player")
    //    {
    //        target = other.gameObject;
    //        CancelInvoke("Think");
    //        attack = true;
    //        //Attack();
    //    }
    //}


}
