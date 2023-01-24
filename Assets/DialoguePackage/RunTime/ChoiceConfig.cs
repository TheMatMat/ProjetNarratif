using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ChoiceConfig
{
    [System.Serializable]
    public struct Choice
    {
        public DialogueTable.Row sentence;
        public OnClickHandle OnClick;

        public Choice(DialogueTable.Row _row, OnClickHandle _event)
        {
            sentence = new DialogueTable.Row();
            sentence.FR = _row.FR;
            sentence.EN = _row.EN;
            OnClick = _event;
        }
    }

    public delegate void OnClickHandle();

    //SentenceConfig.Sentence question;
    public List<Choice> allChoices = new List<Choice>();
}
