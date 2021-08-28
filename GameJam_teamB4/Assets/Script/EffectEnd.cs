using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace lws
{
    public class EffectEnd : MonoBehaviour
    {
        public void EndEffect()
        {
            gameObject.SetActive(false);
        }
    }
}