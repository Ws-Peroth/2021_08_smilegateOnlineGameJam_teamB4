using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace lws
{
    public class GoalPoint : MonoBehaviour
    {
        [SerializeField] WordUI wordCondition;
        private void OnTriggerEnter2D(Collider2D collision)
        {

            if (collision.gameObject.CompareTag("Player"))
            {
                if ( SceneController.IsPassedLevel() || wordCondition.wordCount >= 5)
                {
                    SceneController.instance.SceneChange(SceneController.currentScene + 1);
                }
            }
        }
    }
}