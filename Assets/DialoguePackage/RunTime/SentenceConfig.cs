using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamSeven
{

    [System.Serializable]
    public class SentenceConfig
    {
        [System.Serializable]
        public struct Sentence
        {
            public DialogueTable.Row sentence;
            public int csvIndex;
            public DialogueControler.TEXT_ANIMATION animEnter;
            public Speaker.EMOTION emotion;

            public Sentence(DialogueTable.Row _sentence, DialogueControler.TEXT_ANIMATION _animation, Speaker.EMOTION _emotion, int _csvIndex)
            {
                sentence = _sentence;
                animEnter = _animation;
                emotion = _emotion;
                csvIndex = _csvIndex;
            }

            public Sentence(bool b)
            {
                sentence = new DialogueTable.Row();
                animEnter = DialogueControler.TEXT_ANIMATION.CHAR_ONSET;
                emotion = Speaker.EMOTION.NEUTRAL;
                csvIndex = -1;
            }
        }

        public List<Sentence> talking = new List<Sentence>();
    }
}
