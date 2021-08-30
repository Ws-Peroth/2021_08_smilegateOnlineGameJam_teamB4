using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace lws
{
    public class FloorText : MonoBehaviour
    {
        [SerializeField] private Text text;
        private void Start()
        {
            text.text = $"Floor {SceneController.clearLevel}";
        }
    }
}