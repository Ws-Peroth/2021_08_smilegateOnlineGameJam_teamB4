using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    float thinkTime = 2f;
    public Vector3 dir = Vector3.left;
    public int hp = 100;

    // Start is called before the first frame update
    void Start()
    {
        Think();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        checkFront();
        Move();

        if (hp <= 0)
        {
            // 50% È®·ü·Î ±ÛÀÚ Å‰µæ.
            if (Random.Range(0, 2) == 1)
            {
                WordUI.Instance.getWord(Random.Range(0, 6));
            }
        }
    }

    private void checkFront()
    {
        Vector2 ray = new Vector2(this.transform.position.x + (dir.x * 0.5f), this.transform.position.y);
        Debug.DrawRay(ray, Vector3.down, Color.green);
        RaycastHit2D raycast = Physics2D.Raycast(ray, Vector3.down, 1, LayerMask.GetMask("Platform"));

        if (raycast.collider == null)
        {
            dir.x *= -1;
        }
    }

    private void Move()
    {
        this.transform.position += dir * Time.deltaTime;
    }

    private void Think()
    {
        thinkTime = Random.Range(1f, 3f);

        dir.x = Random.Range(-1, 2);
        dir.y = 0;
        dir.z = 0;

        Invoke("Think", thinkTime);
    }
}
