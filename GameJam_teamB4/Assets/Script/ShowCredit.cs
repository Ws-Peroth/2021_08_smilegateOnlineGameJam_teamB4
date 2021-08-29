using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace lws
{
    public class ShowCredit : MonoBehaviour
    {
        public TalkSystem talkSystem;
        public Image bg;
        public Image covi;
        public GameObject cov;
        public GameObject credit;
        public bool isShow;

        private void Start()
        {
            covi.color = new Color(0, 0, 0, 0);
            cov.SetActive(false);
            bg.color = new Color(1, 1, 1, 0.7843137f);
            credit.SetActive(false);
            isShow = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (!isShow && talkSystem.isEnd )
            {
                End();
            }
        }

        public void End()
        {
            isShow = true;
            StartCoroutine(Credit());
        }

        public IEnumerator Credit()
        {
            while(bg.color.a < 1)
            {
                Color c = bg.color;
                c.a += 0.001f;
                bg.color = c;
                yield return null;
            }
            yield return new WaitForSeconds(1f);

            credit.SetActive(true);
        }

        public void Close()
        {
            StartCoroutine(CloseAnim());   
        }

        public IEnumerator CloseAnim()
        {
            yield return new WaitForSeconds(0.5f);

            cov.SetActive(true);


            while (covi.color.a < 1)
            {
                Color c = covi.color;
                c.a += 0.0003f;
                covi.color = c;
                yield return null;
            }

            SceneController.instance.SceneChange(Scenes.Openning);
        }
    }
}