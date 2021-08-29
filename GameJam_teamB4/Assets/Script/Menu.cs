using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace lws
{
    public class Menu : MonoBehaviour
    {
        public GameObject howtoPanel;

        // Start is called before the first frame update
        void Start()
        {
            SceneController.instance.Init(Scenes.Openning, 0);
        }

        public void StartButton()
        {
            SceneController.instance.SceneChange(Scenes.Floor1);
        }
        public void HowtoButton()
        {
            howtoPanel.SetActive(true);
        }
        public void ExitButton()
        {

        }

        public void HowToEsc()
        {
            howtoPanel.SetActive(false);
        }

    }
}