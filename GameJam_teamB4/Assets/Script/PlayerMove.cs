using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace lws
{
    public class PlayerMove : MonoBehaviour
    {
        public Rigidbody2D playerRigidbody;
        public SpriteRenderer playerSpriteRenderer;

        [SerializeField] private float speed;
        [SerializeField] private float jumpPower;
        [SerializeField] private float downSpeed;
        [SerializeField] private bool isGround = true;

        // Start is called before the first frame update
        void Start()
        {

            speed = 10;
            jumpPower = 400;
            downSpeed = 3f;
        }

        // Update is called once per frame
        void Update()
        {
            Move();
            Jump();
        }

        void Jump()
        {
            playerRigidbody.gravityScale = downSpeed;

            // ↑ 점프, 바닥체크
            isGround = Physics2D.OverlapCircle((Vector2)transform.position
                + new Vector2(0, -0.5f), 0.07f, 1 << LayerMask.NameToLayer("Ground"));


            if (Input.GetKeyDown(KeyCode.UpArrow) && isGround)
            {
                playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, 0);
                playerRigidbody.AddForce(Vector2.up * jumpPower);
            }
        }

        private void Move()
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                playerRigidbody.velocity = new Vector2(speed, playerRigidbody.velocity.y);
                playerSpriteRenderer.flipX = false;

            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                playerRigidbody.velocity = new Vector2(-speed, playerRigidbody.velocity.y);
                playerSpriteRenderer.flipX = true;
            }
            else
            {
                playerRigidbody.velocity = new Vector2(0, playerRigidbody.velocity.y);
            }
        }
    }
}