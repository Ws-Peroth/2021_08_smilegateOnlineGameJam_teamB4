using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace lws
{
    public class PlayerAttackRange : MonoBehaviour
    {
        [SerializeField] private Player player;

        public bool attackDelayOn;
        [SerializeField] private bool monsterInRange;

        private void Start()
        {
            attackDelayOn = false;
        }

        void Update()
        {
            SetRange();
        }

        private void SetRange()
        {
            if (player.playerSpriteRenderer.flipX)
            {
                transform.localPosition = new Vector3(-0.6f, 0, 0);
            }
            else
            {
                transform.localPosition = new Vector3(0.6f, 0, 0);
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            Debug.Log("Trigger On");

            if (collision.gameObject.CompareTag("Enemy"))
            {
                monsterInRange = true;
                Debug.Log("Effect On");
                player.attackEnemy = collision;
                // player.OnAttackRange(collision);
            }
            else
            {
                monsterInRange = false;
            }

        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            player.attackEnemy = null;
        }
    }
}