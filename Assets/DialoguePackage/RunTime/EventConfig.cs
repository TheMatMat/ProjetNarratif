using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TeamSeven
{

    [System.Serializable]
    public class EventConfig
    {
        public ACTION_TYPE actionType;
        public POSITION screenPos;

        public UnityEvent OnCustomEvent;
        public string idSnapshot;
        public float snapShotTime;

        [System.Serializable]
        public enum ACTION_TYPE
        {
            SPEAKER_IN,
            SPEAKER_OUT,
            CUSTOM_EVENT,
            CHANGE_MUSIC
        }

        [System.Serializable]
        public enum POSITION
        {
            LEFT,
            RIGHT,
            MIDDLE
        }
    }
}
