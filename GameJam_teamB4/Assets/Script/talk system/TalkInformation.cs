using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace lws
{
    public class TalkInformation
    {
        public string txt;
        public int member;

        public TalkInformation(string txt, int member)
        {
            this.txt = txt;
            this.member = member;
        }
    }
}