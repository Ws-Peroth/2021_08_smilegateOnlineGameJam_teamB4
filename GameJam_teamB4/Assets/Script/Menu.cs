using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace lws
{
    public class Menu : MonoBehaviour
    {
        public GameObject howtoPanel;

        public OpenStroy stroy;

        public GameObject storyBG;
        public Image storyBGImage;

        public GameObject skyBG;
        public Image skyBGImage;



        // Start is called before the first frame update
        void Start()
        {

            storyBGImage.color = new Color(1, 1, 1, 0);
            skyBGImage.color = new Color(1, 1, 1, 0);

            storyBG.SetActive(false);
            skyBG.SetActive(false);

            SceneController.instance.Init(Scenes.Openning, 0);
        }

        public void StartButton()
        {
            StartCoroutine(StartStory());
        }

        IEnumerator StartStory()
        {
            storyBG.SetActive(true);
            skyBG.SetActive(true);

            while (storyBGImage.color.a < 1 && skyBGImage.color.a < 1)
            {
                Color c = storyBGImage.color;
                Color c2 = skyBGImage.color;

                c.a += 0.006f;
                c2.a += 0.006f;

                storyBGImage.color = c;
                skyBGImage.color = c2;

                yield return null;
            }

            stroy.GetStartSignal();

            while (!stroy.isEnd)
            {
                yield return null;
            }

            SceneController.instance.SceneChange(Scenes.Floor1);
        }

        public void HowtoButton()
        {
            howtoPanel.SetActive(true);
        }
        public void ExitButton()
        {
            Application.Quit();
        }

        public void HowToEsc()
        {
            howtoPanel.SetActive(false);
        }

    }
}