using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace lws
{
    public class GotoUnderFloor : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {

            if (collision.gameObject.CompareTag("Player"))
            {
                if (SceneController.currentScene > Scenes.Floor1)
                    SceneController.instance.SceneChange(SceneController.currentScene - 1);
                else
                    SceneController.instance.SceneChange(Scenes.Floor1);
            }
        }
    }
}