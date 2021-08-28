using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace lws
{
    public class Player : PlayerMove
    {
        [SerializeField] Slider hpBar;
        [SerializeField] private bool isAttack;
        [SerializeField] private bool isSkill;
        [SerializeField] private GameObject skillEffect;
        [SerializeField] private GameObject attackEffect;

        public int skillDmg;
        public int attackDmg;
        public bool isHit;
        public float hp = 100;

        public override void Start()
        {
            base.Start();
            skillDmg = 30;
            attackDmg = 10;

            isAttack = false;
            isSkill = false;
            isHit = false;
            hp = 100;
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
        public void OnAttackRange(Collider2D enemy)
        {
            if (isAttack) CommonAttackHit(enemy);
            else if (isSkill) SkillAttackHit(enemy);
        }

        public void CommonAttackHit(Collider2D monster)
        {
            if (!attackEffect.activeSelf)
            {
                attackEffect.SetActive(true);

                if (playerSpriteRenderer.flipX)
                    skillEffect.transform.rotation
                        = Quaternion.Euler(new Vector3(0, 0, -90));
                else
                    skillEffect.transform.rotation
                        = Quaternion.Euler(new Vector3(0, 180, -90));
            }

            SetDamage(monster, attackDmg);
        }

        public void SkillAttackHit(Collider2D monster)
        {
            skillEffect.SetActive(true);

            skillEffect.transform.localPosition 
                = new Vector3(0, 0.33f, 0);

            if (playerSpriteRenderer.flipX)
                skillEffect.transform.rotation 
                    = Quaternion.Euler(new Vector3(0, 0, -90));
            else
                skillEffect.transform.rotation 
                    = Quaternion.Euler(new Vector3(0, 180, -90));

            SetDamage(monster, skillDmg);
        }

        private void SetDamage(Collider2D enemy, int dmg)
        {

            enemy.GetComponent<Monster>().getDamage(attackDmg);
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

        public void GetDamage(float dmg)
        {
            hp -= dmg;

            if(hp <= 0)
            {

            }
        }
    }
}