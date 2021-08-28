using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using Newtonsoft.Json;

namespace lws {

    public class TalkSystem : MonoBehaviour
    {
        [SerializeField] private Text speakerName;
        [SerializeField] private Text dialogue;
        [SerializeField] private Sprite[] speakerSprites;
        [SerializeField] private SpriteRenderer speaker;
        [SerializeField] private float charDelay;

        private Queue<TalkInformation> talks = new Queue<TalkInformation>();
        private TalkInformation temp;
        private IEnumerator showCharWithDelayCoroutine;
        private bool isShowing;
        private string[] names = { "AAA", "BBB"};
        private void Start()
        {
            isShowing = false;
            charDelay = 0.05f;
            
            var loadJson = Resources.Load("Dialogue") as TextAsset;
            talks = JsonConvert.DeserializeObject<Queue<TalkInformation>>(loadJson.ToString());

            GetStartSignal();
            
        }

        public void NextButtonDown()
        {
            if (isShowing)
                GetSkipSignal();
            else
                GetStartSignal();
        }

        private void GetStartSignal()
        {
            if(talks.Count > 0)
            {
                temp = talks.Dequeue();
                speakerName.text = names[temp.member];
                SetSpeaker(temp.member);
                showCharWithDelayCoroutine = ShowChar(temp.txt);
                StartCoroutine(showCharWithDelayCoroutine);
            }
            else TalkEnd();
        }

        private void SetSpeaker(int member)
        {
            speaker.sprite = speakerSprites[member];
        }

        public void TalkEnd()
        {
            Debug.Log("Talk End");
            gameObject.SetActive(false);
        }

        private void GetSkipSignal()
        {
            StopCoroutine(showCharWithDelayCoroutine);
            dialogue.text = temp.txt;
            isShowing = false;
        }

        private IEnumerator ShowChar(string txt)
        {
            isShowing = true;

            dialogue.text = "";

            for(int i = 0; i < txt.Length; i++)
            {
                dialogue.text += txt[i];
                yield return new WaitForSeconds(charDelay);
            }

            isShowing = false;
        }
    }
}