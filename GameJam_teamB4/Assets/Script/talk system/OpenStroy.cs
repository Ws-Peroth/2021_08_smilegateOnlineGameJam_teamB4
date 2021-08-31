using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using Newtonsoft.Json;

namespace lws {

    public class OpenStroy : MonoBehaviour
    {
        [SerializeField] private GameObject obj;
        [SerializeField] private Text speakerName;
        [SerializeField] private Text dialogue;
        [SerializeField] private Sprite[] speakerSprites;
        [SerializeField] private SpriteRenderer speaker;
        [SerializeField] private float charDelay;

        private Queue<TalkInformation> talks = new Queue<TalkInformation>();
        private TalkInformation temp;
        private IEnumerator showCharWithDelayCoroutine;
        private bool isShowing;
        private string[] names = { "플레이어", "그녀", "???", " " };
        public bool isEnd;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                GetSkipSignal();
            }
        }

        private void Start()
        {
            isEnd = false;
            isShowing = false;
            charDelay = 0.05f;
            
            var loadJson = Resources.Load("Stroy") as TextAsset;
            talks = JsonConvert.DeserializeObject<Queue<TalkInformation>>(loadJson.ToString());
            
        }

        public void NextButtonDown()
        {
            if (isShowing)
                GetSkipSignal();
            else
                GetStartSignal();
        }

        public void GetStartSignal()
        {
            obj.SetActive(true);
            if (talks.Count > 0)
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
            isEnd = true;
            gameObject.SetActive(false);
        }

        private void GetSkipSignal()
        {
            Debug.Log("Clicked");
            isShowing = false;
            dialogue.text = temp.txt;
            StopCoroutine(showCharWithDelayCoroutine);
        }

        private IEnumerator ShowChar(string txt)
        {
            isShowing = true;

            dialogue.text = "";

            for(int i = 0; i < txt.Length; i++)
            {
                if (!isShowing)
                {
                    yield break;
                }
                dialogue.text += txt[i];
                yield return new WaitForSeconds(charDelay);
            }

            isShowing = false;
        }
    }
}