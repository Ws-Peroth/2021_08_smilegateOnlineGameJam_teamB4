using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace lws
{
    public class PlayerMove : MonoBehaviour
    {
        public Animator playerAnimator;
        public Rigidbody2D playerRigidbody;
        public SpriteRenderer playerSpriteRenderer;
        public bool isGround = true;

        [SerializeField] private float speed;
        [SerializeField] private float jumpPower;
        [SerializeField] private float downSpeed;

        // Start is called before the first frame update
        public virtual void Start()
        {
            speed = 10;
            jumpPower = 600;
            downSpeed = 3f;
        }

        public void GroundCheck()
        {
            // ↑ 점프, 바닥체크
            isGround = Physics2D.OverlapCircle((Vector2)transform.position
                + new Vector2(0, -0.9f), 0.07f, 1 << LayerMask.NameToLayer("Ground"));
        }

        public void Jump()
        {
            playerRigidbody.gravityScale = downSpeed;

            if (Input.GetKeyDown(KeyCode.UpArrow) && isGround)
            {
                playerAnimator.SetBool("isMove", false);

                playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, 0);
                playerRigidbody.AddForce(Vector2.up * jumpPower);
            }
        }

        public void Move()
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                playerAnimator.SetBool("isMove", true);

                playerRigidbody.velocity = new Vector2(speed, playerRigidbody.velocity.y);
                playerSpriteRenderer.flipX = false;

            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                playerAnimator.SetBool("isMove", true);

                playerRigidbody.velocity = new Vector2(-speed, playerRigidbody.velocity.y);
                playerSpriteRenderer.flipX = true;
            }
            else
            {
                playerAnimator.SetBool("isMove", false);

                playerRigidbody.velocity = new Vector2(0, playerRigidbody.velocity.y);
            }
        }
    }
}