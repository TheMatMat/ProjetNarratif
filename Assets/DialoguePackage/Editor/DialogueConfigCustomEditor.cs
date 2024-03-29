using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace TeamSeven
{

    [CustomEditor(typeof(DialogueConfig))]
    [CanEditMultipleObjects]
    public class DialogueConfigCustomEditor : Editor
    {
        public GUISkin customSkin;
        private DialogueConfig _source;

        /*private string IDInput;
        private string lastInput;
        private int enumIdCsv;

        private Vector2 scrollPosition;
        private List<Result> searchResult = new List<Result>();

        private int idResultSelected;*/
        //private int idSpeekerSelected;

        /*private bool isInCustomDialogue;
        private string customDialogue;*/

        private List<string> speekerName = new List<string>();
        //private List<string> csvName = new List<string>();

        /*public struct Result
        {
            public DialogueTable.Row resultRow;
            public int resultIdCsv;

            public Result (DialogueTable.Row _row, int _idCsv)
            {
                resultRow = _row;
                resultIdCsv = _idCsv;
            }
        }*/

        private void OnEnable()
        {
            _source = (DialogueConfig)this.target;
            Refresh();
        }

        private void OnDisable()
        {
            //searchResult.Clear();
            if (!EditorApplication.isPlaying)
                EditorUtility.SetDirty(_source);
        }

        public override void OnInspectorGUI()
        {
            GUI.skin = customSkin;

            // -------------------------------------- Header -------------------------------------- //

            #region HEADER

            GUILayout.Space(10);

            if (GUILayout.Button(new GUIContent("Refresh", "Recharge all dialogue, speekers and csv files")))
            {
                Refresh();
            }

            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            GUILayout.Label("SpeekerConfig");
            _source.speekerConfig = (SpeekerConfig)EditorGUILayout.ObjectField(_source.speekerConfig, typeof(SpeekerConfig), true, GUILayout.Width(200));
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("CSV File");
            TextAsset newCSVFile = null;
            newCSVFile = (TextAsset)EditorGUILayout.ObjectField(newCSVFile, typeof(TextAsset), true, GUILayout.Width(200));
            GUILayout.EndHorizontal();

            if (newCSVFile)
            {
                if (!_source.csvFile.Contains(newCSVFile))
                {
                    _source.csvFile.Add(newCSVFile);
                    //csvName.Add(newCSVFile.name);
                }
            }

            foreach (TextAsset file in _source.csvFile)
            {
                GUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                GUILayout.Label(file.name, GUILayout.Width(175));
                if (GUILayout.Button(new GUIContent("", "Remove csv file"), "bin", GUILayout.Width(20)))
                {
                    _source.csvFile.Remove(file);
                    //csvName.Remove(file.name);
                    return;
                }
                GUILayout.EndHorizontal();
            }

            GUILayout.Space(20);

            GUILayout.BeginHorizontal();
            GUILayout.Label("Delai Auto-Pass");
            _source.delaiAutoPass = EditorGUILayout.FloatField(_source.delaiAutoPass, GUILayout.Width(200));
            GUILayout.EndHorizontal();

            _source.startDialogue = EditorGUILayout.Toggle("Start at begin", _source.startDialogue);
            if (_source.startDialogue)
                _source.delaiStart = EditorGUILayout.FloatField(_source.delaiStart);

            #endregion

            // ---------------------------------- Search Fonction ---------------------------------- //

            #region SEARCH_FONCTION

            /*GUILayout.Space(5);

            GUILayout.BeginVertical("window");
            GUILayout.BeginHorizontal();

            GUILayout.Label("Select ID", GUILayout.ExpandWidth(true));
            IDInput = EditorGUILayout.TextField(IDInput, GUILayout.Width(100));
            enumIdCsv = EditorGUILayout.Popup(enumIdCsv, csvName.ToArray(), GUILayout.Width(100));

            *//*if (GUILayout.Button(new GUIContent("Avanced", "Look for a specific key"), GUILayout.Width(70f)))
            {
                // Ouverture Box
            }*//*

            GUILayout.EndHorizontal();

            if(IDInput != lastInput)
            {
                lastInput = IDInput;

                if (IDInput == "")
                    isInCustomDialogue = true;
                else
                {
                    SearchForSentence();
                    isInCustomDialogue = false;
                }
            }

            if (isInCustomDialogue)
                customDialogue = GUILayout.TextArea(customDialogue, GUILayout.Height(60));
            else
            {
                if (searchResult.Count > 0)
                {
                    scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Height(120));
                    for (int k = 0; k < searchResult.Count; k++)
                    {
                        if (GUILayout.Button("[ " + searchResult[k].resultRow.ID + " ]    " + searchResult[k].resultRow.FR, k == idResultSelected ? "boxselected" : "box"))
                            idResultSelected = k;
                    }
                    GUILayout.EndScrollView();
                }
                else
                    GUILayout.Label("Not Found", "box", GUILayout.Height(60));
            }

            if (GUILayout.Button(new GUIContent("Add Sentence To Active", "Add to Sentence list of the active character below")) && _source.allDialogueEvents[idSpeekerSelected].source == DialogueEvent.TYPE_EVENT.SENTENCE)
            {
                if (!isInCustomDialogue)
                {
                    if (idSpeekerSelected != -1 && searchResult.Count > 0)
                        _source.allDialogueEvents[idSpeekerSelected].sentenceConfig.talking.Add(new SentenceConfig.Sentence(searchResult[idResultSelected].resultRow, DialogueControler.TEXT_ANIMATION.CHAR_ONSET, Speaker.EMOTION.NEUTRAL, searchResult[idResultSelected].resultIdCsv));
                }
                else if (idSpeekerSelected != -1 && customDialogue.Length > 0)
                {
                    _source.allDialogueEvents[idSpeekerSelected].sentenceConfig.talking.Add(new SentenceConfig.Sentence(new DialogueTable.Row(customDialogue), DialogueControler.TEXT_ANIMATION.CHAR_ONSET, Speaker.EMOTION.NEUTRAL, -1));
                }
            }

            GUILayout.EndVertical();

            GUILayout.Space(10);*/

            #endregion

            // --------------------------------------- Body --------------------------------------- //

            #region BODY

            int currentIndex = 0;
            foreach (DialogueEvent currentDialogueEvent in _source.allDialogueEvents)
            {

                if (currentDialogueEvent == null)
                    continue;

                GUILayout.Space(5);

                string style = "box";
                /*if (currentDialogueEvent.source == DialogueEvent.TYPE_EVENT.SENTENCE && idSpeekerSelected == currentIndex)
                    style = "boxselected";
                else */
                if (currentDialogueEvent.source == DialogueEvent.TYPE_EVENT.EVENT)
                {
                    if (currentDialogueEvent.eventConfig.actionType == EventConfig.ACTION_TYPE.SPEAKER_IN)
                        style = "actionin";
                    else
                        style = "actionout";
                }
                else if (currentDialogueEvent.source == DialogueEvent.TYPE_EVENT.CHOICE)
                    style = "choice";


                GUILayout.BeginVertical(style);

                GUILayout.BeginHorizontal();

                if (GUILayout.Button(new GUIContent("", "Collapse/Show Details"), currentDialogueEvent.isColapse ? "colapse" : "detail", GUILayout.Width(20)))
                {
                    currentDialogueEvent.isColapse = !currentDialogueEvent.isColapse;
                }

                GUILayout.Space(10);
                currentDialogueEvent.idSpeeker = EditorGUILayout.Popup(currentDialogueEvent.idSpeeker, speekerName.ToArray(), GUILayout.Width(150));

                GUILayout.FlexibleSpace();

                if (GUILayout.Button(new GUIContent("", "Move the speeker up"), "up", GUILayout.Width(20f)))
                {
                    if (currentIndex != 0)
                    {
                        Swap<DialogueEvent>(_source.allDialogueEvents, currentIndex, currentIndex - 1);

                        /*if (idSpeekerSelected == currentIndex)
                            idSpeekerSelected--;*/
                        return;
                    }
                }

                if (GUILayout.Button(new GUIContent("", "Move the speeker down"), "down", GUILayout.Width(20f)))
                {
                    if (currentIndex != _source.allDialogueEvents.Count - 1)
                    {
                        Swap<DialogueEvent>(_source.allDialogueEvents, currentIndex, currentIndex + 1);

                        /*if (idSpeekerSelected == currentIndex)
                            idSpeekerSelected++;*/
                        return;
                    }
                }

                if (GUILayout.Button(new GUIContent("", "Delete this conversation"), "bin", GUILayout.Width(20f)) && EditorUtility.DisplayDialog("Caution", "Your are about to delete the entire section of this dialogue.\nAre you sure ?", "Yes, I am certain"))
                {
                    _source.allDialogueEvents.Remove(currentDialogueEvent);

                    /*if (idSpeekerSelected == currentIndex)
                        idSpeekerSelected = -1;*/
                    return;
                }


                GUILayout.EndHorizontal();

                if (!currentDialogueEvent.isColapse)
                {
                    currentDialogueEvent.autoPass = GUILayout.Toggle(currentDialogueEvent.autoPass, "Auto-Pass");

                    #region SENTENCE
                    if (currentDialogueEvent.source == DialogueEvent.TYPE_EVENT.SENTENCE)
                    {
                        SentenceConfig currentSentenceConfig = currentDialogueEvent.sentenceConfig;

                        /*if (currentIndex != idSpeekerSelected)
                            if (GUILayout.Button(new GUIContent("Active", "Select this speeker to recive sentences"), "buttoncenter")) idSpeekerSelected = currentIndex;*/

                        GUILayout.Space(10);

                        GUILayout.BeginVertical("Box");
                        GUILayout.Label("Sentence");

                        for (int y = 0; y < currentSentenceConfig.talking.Count; y++)
                        {
                            GUILayout.BeginVertical(currentSentenceConfig.talking[y].csvIndex == -1 ? "customsentence" : "sentence");
                            GUILayout.BeginHorizontal();

                            GUILayout.Label(currentSentenceConfig.talking[y].sentence.FR.Length > 100 ? currentSentenceConfig.talking[y].sentence.FR.Substring(0, 100) + " [...]" : currentSentenceConfig.talking[y].sentence.FR, "txt");
                            GUILayout.FlexibleSpace();

                            if (GUILayout.Button(new GUIContent("", "Move the sentence up"), "up", GUILayout.Width(20f)))
                            {
                                if (y != 0)
                                {
                                    Swap<SentenceConfig.Sentence>(currentSentenceConfig.talking, y, y - 1);
                                    return;
                                }
                            }
                            if (GUILayout.Button(new GUIContent("", "Move the sentence down"), "down", GUILayout.Width(20f)))
                            {
                                if (y != currentSentenceConfig.talking.Count - 1)
                                {
                                    Swap<SentenceConfig.Sentence>(currentSentenceConfig.talking, y, y + 1);
                                    return;
                                }
                            }

                            if (GUILayout.Button(new GUIContent("", "Delete the current sentence"), "bin", GUILayout.Width(20f)))
                            {
                                currentSentenceConfig.talking.Remove(currentSentenceConfig.talking[y]);
                                return;
                            }

                            GUILayout.EndHorizontal();

                            GUILayout.BeginHorizontal();
                            GUILayout.Label("Text Anim", GUILayout.Width(70));
                            DialogueControler.TEXT_ANIMATION txtAnimation = (DialogueControler.TEXT_ANIMATION)EditorGUILayout.EnumPopup(currentSentenceConfig.talking[y].animEnter, GUILayout.Width(100f));
                            GUILayout.EndHorizontal();


                            GUILayout.BeginHorizontal();
                            GUILayout.Label("Emotion", GUILayout.Width(70));
                            Speaker.EMOTION spEmotion = (Speaker.EMOTION)EditorGUILayout.EnumPopup(currentSentenceConfig.talking[y].emotion, GUILayout.Width(100f));
                            GUILayout.EndHorizontal();

                            currentSentenceConfig.talking[y] = new SentenceConfig.Sentence(currentSentenceConfig.talking[y].sentence, txtAnimation, spEmotion, currentSentenceConfig.talking[y].csvIndex);

                            GUILayout.BeginHorizontal();
                            if (currentSentenceConfig.talking[y].csvIndex == -1)
                                GUILayout.Label("* Custom, may not be complet !", "warning");
                            GUILayout.FlexibleSpace();
                            if (GUILayout.Button(new GUIContent("Modify", "Edit the current sentence. Will be pass in custome sentence."), GUILayout.Width(100)))
                                ModifierWindow.ShowWindow(_source.csvFile, currentSentenceConfig, y);
                            GUILayout.EndHorizontal();

                            GUILayout.EndVertical();
                        }

                        if (GUILayout.Button(new GUIContent("Add Sentence", "Select this speeker to recive sentences"), "buttoncenter"))
                        {
                            ModifierWindow.ShowWindow(_source.csvFile, currentSentenceConfig);
                        }

                        GUILayout.EndVertical();
                    }
                    #endregion
                    #region EVENT
                    else if (currentDialogueEvent.source == DialogueEvent.TYPE_EVENT.EVENT)
                    {
                        currentDialogueEvent.eventConfig.actionType = (EventConfig.ACTION_TYPE)EditorGUILayout.EnumPopup(currentDialogueEvent.eventConfig.actionType);

                        if (currentDialogueEvent.eventConfig.actionType == EventConfig.ACTION_TYPE.SPEAKER_IN)
                            currentDialogueEvent.eventConfig.screenPos = (EventConfig.POSITION)EditorGUILayout.EnumPopup(currentDialogueEvent.eventConfig.screenPos);
                        else if (currentDialogueEvent.eventConfig.actionType == EventConfig.ACTION_TYPE.CUSTOM_EVENT)
                        {
                            SerializedProperty m_event = serializedObject.FindProperty("allDialogueEvents").GetArrayElementAtIndex(currentIndex).FindPropertyRelative("eventConfig").FindPropertyRelative("OnCustomEvent");
                            EditorGUILayout.PropertyField(m_event);
                            serializedObject.ApplyModifiedProperties();
                        }
                        else if(currentDialogueEvent.eventConfig.actionType == EventConfig.ACTION_TYPE.CHANGE_MUSIC)
                        {
                            currentDialogueEvent.eventConfig.idSnapshot = GUILayout.TextField(currentDialogueEvent.eventConfig.idSnapshot);
                            currentDialogueEvent.eventConfig.snapShotTime = EditorGUILayout.FloatField(currentDialogueEvent.eventConfig.snapShotTime);
                        }
                    }
                    #endregion
                    #region CHOICE
                    else if (currentDialogueEvent.source == DialogueEvent.TYPE_EVENT.CHOICE)
                    {
                        for (int it = 0; it < currentDialogueEvent.choiceConfig.allChoices.Count; it++)
                        {
                            GUILayout.BeginVertical("actionin");
                            string a = GUILayout.TextArea(currentDialogueEvent.choiceConfig.allChoices[it].sentence.FR);
                            string b = GUILayout.TextArea(currentDialogueEvent.choiceConfig.allChoices[it].sentence.EN);

                            currentDialogueEvent.choiceConfig.allChoices[it] = new ChoiceConfig.Choice(new DialogueTable.Row(a, b), currentDialogueEvent.choiceConfig.allChoices[it].OnClick);

                            /*if (currentDialogueEvent.choiceConfig.allChoices[it].OnClick == null)
                            {
                                DialogueConfig newDialogue = null;
                                newDialogue = (DialogueConfig)EditorGUILayout.ObjectField(newDialogue, typeof(DialogueConfig), true);

                                if (newDialogue != null)
                                    currentDialogueEvent.choiceConfig.allChoices[it] = new ChoiceConfig.Choice(new DialogueTable.Row(a, b), newDialogue.gameObject);
                            }
                            else
                                GUILayout.Label("Have a StartDialogue");*/

                            SerializedProperty m_event = serializedObject.FindProperty("allDialogueEvents").GetArrayElementAtIndex(currentIndex).FindPropertyRelative("choiceConfig").FindPropertyRelative("allChoices").GetArrayElementAtIndex(it).FindPropertyRelative("OnClick");
                            EditorGUILayout.PropertyField(m_event);
                            serializedObject.ApplyModifiedProperties();

                            if (GUILayout.Button("Delete", "buttoncenter"))
                                currentDialogueEvent.choiceConfig.allChoices.RemoveAt(it);

                            GUILayout.EndVertical();
                        }


                        if (currentDialogueEvent.choiceConfig.allChoices.Count < 4)
                            if (GUILayout.Button(new GUIContent("Add Answer", "Add a new answer")))
                                currentDialogueEvent.choiceConfig.allChoices.Add(new ChoiceConfig.Choice());
                    }
                    #endregion
                }

                GUILayout.EndVertical();
                currentIndex++;
            }

            #endregion

            // -------------------------------------- Footer -------------------------------------- //

            GUILayout.Space(20);
            if (GUILayout.Button(new GUIContent("Add Speeker", "Add a new speeker")))
                _source.allDialogueEvents.Add(new DialogueEvent(new SentenceConfig()));

            GUILayout.Space(5);

            if (GUILayout.Button(new GUIContent("Add Event", "Add a new event")))
                _source.allDialogueEvents.Add(new DialogueEvent(new EventConfig()));

            GUILayout.Space(5);

            if (GUILayout.Button(new GUIContent("Add Choice", "Add a new choice")))
                _source.allDialogueEvents.Add(new DialogueEvent(new ChoiceConfig()));

            GUILayout.Space(20);
        }

        private void Refresh()
        {
            /*scrollPosition = Vector2.zero;

            searchResult.Clear();
            IDInput = "";
            isInCustomDialogue = true;

            idResultSelected = 0;
            idSpeekerSelected = -1;

            csvName.Clear();
            csvName.Add("In all CSV");
            foreach (TextAsset other in _source.csvFile)
                csvName.Add(other.name);*/

            speekerName.Clear();
            if (_source.speekerConfig)
                foreach (Speaker other in _source.speekerConfig.allSpeekers)
                    speekerName.Add(other.name);
        }

        public void Swap<T>(IList<T> list, int indexA, int indexB)
        {
            T tmp = list[indexA];
            list[indexA] = list[indexB];
            list[indexB] = tmp;
        }

        /*private void SearchForSentence()
        {
            if (_source.csvFile.Count == 0) return;
            searchResult.Clear();
            idResultSelected = 0;

            if(enumIdCsv == 0)
            {
                for (int j = 0; j < csvName.Count - 1; j++)
                {
                    LoadCSVFile(j);
                }
            }
            else
                LoadCSVFile(enumIdCsv - 1);
        }

        private void LoadCSVFile(int _idCSV)
        {
            DialogueTable table = new DialogueTable();
            table.Load(_source.csvFile[_idCSV]);
            if(table.IsLoaded())
                foreach (DialogueTable.Row row in table.FindAll_ID(IDInput))
                    searchResult.Add(new Result(row, _idCSV));
        }*/
    }
}