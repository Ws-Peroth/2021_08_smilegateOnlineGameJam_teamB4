using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace lws
{
    public class Menu : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            SceneController.instance.Init(Scenes.Openning, 0);
        }

        public void StartButton()
        {
            SceneController.instance.SceneChange(Scenes.Floor1);
        }
        public void OptionButton()
        {

        }
        public void ExitButton()
        {

        }



    }
}