using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamSeven
{

    [System.Serializable]
    public class Speaker
    {
        [System.Serializable]
        public struct SpeekerStatu
        {
            public EMOTION emotion;
            public AnimationClip animation;
        }

        [System.Serializable]
        public enum EMOTION
        {
            NEUTRAL,
            FEAR,
            SAD,
            CONFUSED,
            FURIOUS,
        }

        public string name;
        public Sprite sprite;
        public List<AudioClip> audioClip = new List<AudioClip>();

        public List<SpeekerStatu> status = new List<SpeekerStatu>();
    }
}
