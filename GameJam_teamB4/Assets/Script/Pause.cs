using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace lws
{
    public class Pause : MonoBehaviour
    {
        public GameObject screen;
        public GameObject howto;

        private void Start()
        {
            howto.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!screen.activeSelf)
                    OpenScreen();
                else
                    CloseScreen();
            }
        }

        public void OpenScreen()
        {
            Time.timeScale = 0;
            screen.SetActive(true);
        }

        public void CloseScreen()
        {
            Time.timeScale = 1;
            howto.SetActive(false);
            screen.SetActive(false);
        }

        public void OpenHowto()
        {
            howto.SetActive(true);
        }

        public void CloseHowto()
        {
            howto.SetActive(false);
        }

        public void GotoMenu()
        {
            Debug.Log("clicked");
            Time.timeScale = 1;
            SceneController.instance.SceneChange(Scenes.Openning);
        }
    }
}