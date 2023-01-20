using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueControler : MonoBehaviour
{
    public static DialogueControler instance { get; private set; }

    private static int gameLanguage;
    public static int GetSetGameLanguage { get { return gameLanguage; } set { gameLanguage = value; } }
    public bool DialoguePanelOpen { get; private set; }

    public TextMeshProUGUI nameSpeeker;
    public TextMeshProUGUI txtSentence;

    public Transform leftSpace;
    public Transform rightSpace;
    public GameObject characterPrefab;

    private DialogueConfig _dialog;
    private SpeekerConfig _speekerConfig;

    private int eventCount = -1;

    private List<GameObject> speakerInScene = new List<GameObject>();
    private Queue<string> sentences = new Queue<string>();
    private AudioSource _audioSource;

    private bool haveFinishDisplaySentence = true;

    //private int speekerCount = -1;

    /*private int idFirstSpeeker = 0;
    private int idLastSpeeker = -1;*/


    [System.Serializable]
    public enum TEXT_ANIMATION
    {
        DEFAULT,
        GRADUAL_ONSET,
        SHAKE
    }

    
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        instance = this;
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!DialoguePanelOpen) return;

        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            DisplayNextSentence();
            Debug.Log("INPUT");
        }
    }

    public void StartDialogue(DialogueConfig dialogue, SpeekerConfig speekers)
    {
        if (dialogue.allDialogueEvents.Count == 0) return;

        // initialisation
        _dialog = dialogue;
        _speekerConfig = speekers;
        gameObject.SetActive(true);

        DialoguePanelOpen = true;

        NextDialogueEvent();
    }

    private void NextDialogueEvent()
    {
        eventCount++;

        if (eventCount >= _dialog.allDialogueEvents.Count)
        {
            DialogueEnd();
            return;
        }

        switch (_dialog.allDialogueEvents[eventCount].source)
        {
            case DialogueEvent.TYPE_EVENT.SENTENCE:

                nameSpeeker.text = _speekerConfig.allSpeekers[_dialog.allDialogueEvents[eventCount].idSpeeker].name;
                sentences.Clear();

                foreach (SentenceConfig.Sentence other in _dialog.allDialogueEvents[eventCount].sentenceConfig.talking)
                {
                    // -------------------------------------------------------- TO DO -------------------------------------------------------- //
                    // Prendre la bonne traduction
                    string whatToSay = "";

                    switch (gameLanguage)
                    {
                        case 0:
                            whatToSay = other.sentence.FR;
                            break;
                        case 1:
                            whatToSay = other.sentence.EN;
                            break;
                    }

                    sentences.Enqueue(whatToSay);
                }


                DisplayNextSentence();
                break;

            case DialogueEvent.TYPE_EVENT.EVENT:

                NewEventConfiguration(_dialog.allDialogueEvents[eventCount].eventConfig, _dialog.allDialogueEvents[eventCount].idSpeeker);
                break;

            case DialogueEvent.TYPE_EVENT.CHOICE:
                break;
        }
    }

    private void DialogueEnd()
    {
        eventCount = -1;

        _dialog = null;
        _speekerConfig = null;
        DialoguePanelOpen = false;

        gameObject.SetActive(false);
    }
    
    public void DisplayNextSentence()
    {
        if (!haveFinishDisplaySentence) return;

        if (sentences.Count == 0)
        {
            NextDialogueEvent();
            return;
        }

        string sentence = sentences.Dequeue();
        StartCoroutine(TypeSentence(sentence));
    }

    private IEnumerator TypeSentence(string sentence)
    {
        haveFinishDisplaySentence = false;
        txtSentence.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            txtSentence.text += letter;
            yield return new WaitForSeconds(0.1f);
        }

        haveFinishDisplaySentence = true;
    }


    private void NewEventConfiguration(EventConfig eventConfig, int _idSpeeker)
    {
        GameObject character = new GameObject();

        switch (eventConfig.actionType)
        {
            case EventConfig.ACTION_TYPE.SPEAKER_IN:

                Debug.Log("SPEEKER IN");
                switch (eventConfig.screenPos)
                {
                    case EventConfig.POSITION.LEFT:
                        character = Instantiate<GameObject>(characterPrefab, leftSpace);
                        break;

                    case EventConfig.POSITION.RIGHT:
                        character = Instantiate<GameObject>(characterPrefab, leftSpace);
                        break;

                    case EventConfig.POSITION.MIDDLE:
                        break;
                }
                break;

            case EventConfig.ACTION_TYPE.SPEAKER_OUT:

                Debug.Log("SPEEKER OUT");
                foreach(GameObject speaker in speakerInScene)
                {
                    if (speaker.name == _speekerConfig.allSpeekers[_idSpeeker].name)
                    {
                        speakerInScene.Remove(speaker);
                        Destroy(speaker);
                        return;
                    }
                }
                break;
        }

        character.name = _speekerConfig.allSpeekers[_idSpeeker].name;
        speakerInScene.Add(character);
        UpdateCharacterProfile(character);
    }

    private void UpdateCharacterProfile(GameObject prefab, Speeker.EMOTION _emotion = Speeker.EMOTION.NEUTRAL)
    {
        int i = 0;
        for (; i < _speekerConfig.allSpeekers.Count; i++)
        {
            if (_speekerConfig.allSpeekers[i].name == prefab.name)
                break;
        }

        if (i >= _speekerConfig.allSpeekers.Count) return;

        prefab.GetComponent<Image>().sprite = _speekerConfig.allSpeekers[i].sprite;
        foreach (Speeker.SpeekerStatu other in _speekerConfig.allSpeekers[i].status)
        {
            if(other.emotion == _emotion)
            {
                prefab.GetComponent<Animation>().clip = other.animation;
                break;
            }
        }
    }

    /*
        public void StartDialogue(DialogueConfig dialogue, SpeekerConfig speekers)
        {
            if (dialogue.sentenceConfigs.Count == 0) return;

            _dialog = dialogue;
            _speekerConfig = speekers;

            gameObject.SetActive(true);
            imgSpriteRight.gameObject.SetActive(false);

            idFirstSpeeker = _dialog.sentenceConfigs[0].idSpeeker;
            imgSpriteLeft.sprite = _speekerConfig.allSpeekers[idFirstSpeeker].sprite;
            NextSpeeker();
        }

        private void NextSpeeker()
        {
            speekerCount++;

            if (speekerCount >= _dialog.sentenceConfigs.Count)
            {
                EndDialogue();
                return;
            }

            if (!(idLastSpeeker == _dialog.sentenceConfigs[speekerCount].idSpeeker))
            {
                // affiche le nom
                nameSpeeker.text = _speekerConfig.allSpeekers[_dialog.sentenceConfigs[speekerCount].idSpeeker].name;

                if (_dialog.sentenceConfigs[speekerCount].idSpeeker == idFirstSpeeker)
                {
                    imgSpriteLeft.color = new Color(1, 1, 1);
                    imgSpriteRight.color = new Color(0.5f, 0.5f, 0.5f);
                }
                else
                {
                    imgSpriteRight.gameObject.SetActive(true);
                    imgSpriteLeft.color = new Color(0.5f, 0.5f, 0.5f);
                    imgSpriteRight.color = new Color(1, 1, 1);
                    imgSpriteRight.sprite = _speekerConfig.allSpeekers[_dialog.sentenceConfigs[speekerCount].idSpeeker].sprite;
                }

                idLastSpeeker = _dialog.sentenceConfigs[speekerCount].idSpeeker;
            }

            // efface les anciennes phrases
            sentences.Clear();

            // récupère les phrase présent dans l'array pour les mettre dans la queux
            foreach (DialogueConfig.SentenceConfig.Sentence other in _dialog.sentenceConfigs[speekerCount].speach)
            {
                sentences.Enqueue(other.sentence);
            }

            DisplayNextSentence();
        }

        private void EndDialogue()
        {
            gameObject.SetActive(false);
            speekerCount = -1;
            idLastSpeeker = -1;

            _dialog = null;
            _speekerConfig = null;

            animBordureBas.SetBool("isOpen", false);
            animBordureHaut.SetBool("isOpen", false);
            player.enabled = true;
            mouse.enabled = true;
            weapen.enabled = true;
        }*/
}
