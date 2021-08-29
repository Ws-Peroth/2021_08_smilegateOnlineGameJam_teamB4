using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace lws
{
    public class Player : PlayerMove
    {
        [SerializeField] Slider hpBar;
        [SerializeField] private PlayerAttackRange range;
        [SerializeField] private bool isAttack;
        [SerializeField] private bool isSkill;
        [SerializeField] private GameObject skillEffect;
        [SerializeField] private GameObject attackEffect;
        [SerializeField] private AudioSource audioSource;

        public int skillDmg;
        public int attackDmg;
        public bool isHit;
        public float hp = 100;

        public float attackDelay = 1f;

        public override void Start()
        {
            base.Start();
            hpBar.gameObject.SetActive(true);

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

                Debug.Log("Call Effect : Attack");
                playerAnimator.SetBool("isAttack", true);
                audioSource.Play();
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
                Debug.Log("Call Effect : Skill");
                playerAnimator.SetBool("isSkill", true);
                audioSource.Play();
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
            if (!attackEffect.activeSelf && !range.attackDelayOn)
            {
                range.attackDelayOn = true;

                attackEffect.SetActive(true);

                if (playerSpriteRenderer.flipX)
                    attackEffect.transform.rotation
                        = Quaternion.Euler(new Vector3(0, 0, -90));
                else
                    attackEffect.transform.rotation
                        = Quaternion.Euler(new Vector3(0, 180, -90));


                SetDamage(monster, attackDmg);
            }

        }

        public void SkillAttackHit(Collider2D monster)
        {
            if (!skillEffect.activeSelf && !range.attackDelayOn)
            {

                range.attackDelayOn = true;

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
        }

        private void SetDamage(Collider2D enemy, int dmg)
        {
            if (enemy.GetComponent<Monster>() != null)
                enemy.GetComponent<Monster>().getDamage(dmg);
        }

        public void AttackEnd()
        {
            isAttack = false;
            range.attackDelayOn = false;
            playerAnimator.SetBool("isAttack", false);
        }
        public void SkillEnd()
        {
            isSkill = false;
            range.attackDelayOn = false;
            playerAnimator.SetBool("isSkill", false);
        }

        public void AttackDelayEnd()
        {
            Debug.Log("Delay End");
            range.attackDelayOn = false;
            Debug.Log($"DelayON : {range.attackDelayOn }");
        }

        public void GetDamage(float dmg)
        {
            hp -= dmg;

            if(hp <= 0)
            {
                Die();
            }
        }

        public void Die()
        {
            gameObject.SetActive(false);
            hpBar.gameObject.SetActive(false);
            SceneController.instance.SceneChange(SceneController.currentScene);
        }
    }
}