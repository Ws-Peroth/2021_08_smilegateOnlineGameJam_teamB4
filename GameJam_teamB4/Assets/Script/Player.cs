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

        [SerializeField] private GameObject skillEffect;
        [SerializeField] private GameObject attackEffect;

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
                if (isGround) playerRigidbody.velocity = Vector2.zero;
            }
        }

        public void CommonAttackHit()
        {
            if (!attackEffect.activeSelf)
            {
                attackEffect.SetActive(true);

                if (playerSpriteRenderer.flipX)
                    attackEffect.transform.localPosition
                        = transform.localPosition + new Vector3(-0.87f, 0.33f, 0);
                else
                    attackEffect.transform.localPosition
                        = transform.localPosition + new Vector3(0.87f, 0.33f, 0);
            }
        }
        public void SkillAttackHit()
        {
            skillEffect.SetActive(true);

            skillEffect.transform.localPosition 
                = transform.localPosition + new Vector3(0, 0.33f, 0);

            if (playerSpriteRenderer.flipX)
                skillEffect.transform.rotation 
                    = Quaternion.Euler(new Vector3(0, 180, 0));
            else
                skillEffect.transform.rotation 
                    = Quaternion.Euler(new Vector3(0, 0, 0));
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