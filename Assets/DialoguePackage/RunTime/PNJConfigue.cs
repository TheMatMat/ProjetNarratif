using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TeamSeven
{

    public class PNJConfigue : MonoBehaviour
    {
        public DialogueConfig deafaultDialogue;
        public DialogueConfig choiceDialogue;

        public List<SubDialogue> allSubDialogs;

        private bool seeForFirstTime = true;

        [System.Serializable]
        public struct SubDialogue
        {
            public bool active;
            public string id;

            public SubDialogue(string _id, bool _active)
            {
                id = _id;
                active = _active;
            }
        }

        public void SpeakToPNJ()
        {
            if (seeForFirstTime)
            {
                deafaultDialogue.StartDialogue();
                seeForFirstTime = false;
            }
            else
            {
                if (allSubDialogs.Count <= 0 || choiceDialogue == null)
                {
                    deafaultDialogue.StartDialogue();
                    return;
                }

                choiceDialogue.StartDialogue();

                for (int i = 0; i < DialogueControler.instance.choiceButtonParent.childCount; i++)
                {
                    if (allSubDialogs.Count > i && !allSubDialogs[i].active)
                        DialogueControler.instance.choiceButtonParent.GetChild(i).GetComponent<Button>().interactable = false;
                }
            }
        }

        public void EnableDialogue(string _idToActive)
        {
            for (int i = 0; i < allSubDialogs.Count; i++)
            {
                if (allSubDialogs[i].id == _idToActive)
                    allSubDialogs[i] = new SubDialogue(allSubDialogs[i].id, true);
            }
        }
    }
}
