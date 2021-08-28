using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace lws
{
    public class Player : PlayerMove
    {
        [SerializeField] Slider hpBar;
        public float hp = 100;
        [SerializeField] private bool isAttack;
        [SerializeField] private bool isSkill;

        public override void Start()
        {
            base.Start();

            hp = 100;
            isAttack = false;
            isSkill = false;
        }

        public void Update()
        {
            if (!isSkill) Attack();
            if (!isAttack) Skill();

            GroundCheck();

            if (!isAttack && !isSkill)
            {
                Move();
                Jump();
            }

            hpBar.value = hp;
        }

        void Attack()
        {
            if (!isAttack && Input.GetKey(KeyCode.Z))
            {
                isAttack = true;

                playerAnimator.SetBool("isAttack", true);
            }
            if (isAttack)
            {
                // 기존의 힘을 유지
                if (isGround) playerRigidbody.velocity = Vector2.zero;
            }
        }

        void Skill()
        {
            if (!isSkill && Input.GetKey(KeyCode.X))
            {
                isSkill = true;

                playerAnimator.SetBool("isSkill", true);
            }
            if (isSkill)
            {
                // 기존의 힘을 유지
                if (isGround) playerRigidbody.velocity = Vector2.zero;
            }
        }

        public void AttackEnd()
        {
            isAttack = false;
            playerAnimator.SetBool("isAttack", false);
        }
        public void SkillEnd()
        {
            isSkill = false;
            playerAnimator.SetBool("isSkill", false);
        }
    }
}