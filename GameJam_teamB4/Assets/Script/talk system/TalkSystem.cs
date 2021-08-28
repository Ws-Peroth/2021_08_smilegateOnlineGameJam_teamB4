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
        [SerializeField] private float charDelay;

        Queue<TalkInformation> talks = new Queue<TalkInformation>();

        void Start()
        {
            charDelay = 0.1f;

            var loadJson = Resources.Load("Dialogue") as TextAsset;
            talks = JsonConvert.DeserializeObject<Queue<TalkInformation>>(loadJson.ToString());
        }

        public void GetStartSignal()
        {
            if(talks.Count > 0)
            {
                TalkInformation temp = talks.Dequeue();
                StartCoroutine(ShowChar(temp.txt));
            }
        }

        private IEnumerator ShowChar(string txt)
        {
            int index = 0;
            dialogue.text = "";

            while (txt[index] != '\0')
            {
                dialogue.text += txt[index];
                yield return new WaitForSeconds(charDelay);
            }
        }
    }
}