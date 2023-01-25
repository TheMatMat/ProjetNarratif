using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace TeamSeven
{

    public class ModifierWindow : EditorWindow
    {
        private static List<TextAsset> allCsvFiles;
        private static SentenceConfig source;

        private static bool editWindow;

        public GUISkin customSkin;

        private bool Init = false;
        private bool isInCustomDialogue;

        private string IDInput;
        private string lastInput;
        private string customDialogue;
        private Result infoSentence;

        private int enumIdCsv;
        private int idResultSelected;

        private Vector2 scrollPosition;
        private List<Result> searchResult = new List<Result>();
        private List<string> csvName = new List<string>();

        public struct Result
        {
            public DialogueTable.Row resultRow;
            public int resultIdCsv;

            public Result(DialogueTable.Row _row, int _idCsv)
            {
                resultRow.ID = _row.ID;
                resultRow.FR = _row.FR;
                resultRow.EN = _row.EN;
                resultIdCsv = _idCsv;
            }
        }

        public static void ShowWindow(List<TextAsset> CSV, SentenceConfig config, int idSentenceToEdit = -1)
        {
            EditorWindow window = EditorWindow.GetWindow(typeof(ModifierWindow));
            window.maxSize = new Vector2(500, 600);
            window.minSize = window.maxSize;

            allCsvFiles = CSV;
            source = config;

            if (idSentenceToEdit != -1)
                editWindow = true;

            window.Show();
        }

        void OnGUI()
        {
            if (!Init) Initialize();
            GUI.skin = customSkin;

            GUILayout.Space(10);
            GUILayout.Label("THE CURRENT SENTENCE CONFIGUE NAME");
            GUILayout.Space(5);

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Search"))
            {
                editWindow = false;
                Init = false;
            }

            if (GUILayout.Button("Edit"))
                editWindow = true;
            GUILayout.EndHorizontal();

            GUILayout.Space(5);

            if (!editWindow)
            {
                GUILayout.BeginVertical("window", GUILayout.ExpandHeight(true));
                GUILayout.BeginHorizontal();

                GUILayout.Label("Select ID", GUILayout.ExpandWidth(true));
                IDInput = EditorGUILayout.TextField(IDInput, GUILayout.Width(100));
                enumIdCsv = EditorGUILayout.Popup(enumIdCsv, csvName.ToArray(), GUILayout.Width(100));

                GUILayout.EndHorizontal();

                if (IDInput != lastInput)
                {
                    lastInput = IDInput;

                    if (IDInput == "")
                    {
                        infoSentence = new Result(new DialogueTable.Row(), -1);
                        isInCustomDialogue = true;
                    }
                    else
                    {
                        SearchForSentence();
                        isInCustomDialogue = false;
                    }
                }

                if (isInCustomDialogue)
                {
                    customDialogue = GUILayout.TextArea(customDialogue, GUILayout.Height(60));
                    if (customDialogue != "")
                    {
                        GUILayout.FlexibleSpace();
                        GUILayout.Label("Warning You are creating a custom dialogue. It may not be comblet !", "warning");
                        infoSentence = new Result(new DialogueTable.Row(customDialogue), -1);
                    }
                    else
                        infoSentence = new Result(new DialogueTable.Row(), -1);
                }
                else
                {
                    if (searchResult.Count > 0)
                    {
                        scrollPosition = GUILayout.BeginScrollView(scrollPosition);
                        for (int k = 0; k < searchResult.Count; k++)
                        {
                            if (GUILayout.Button("[ " + searchResult[k].resultRow.ID + " ]    " + searchResult[k].resultRow.FR, k == idResultSelected ? "boxselected" : "box"))
                            {
                                infoSentence = new Result(searchResult[k].resultRow, searchResult[k].resultIdCsv);
                                idResultSelected = k;
                            }
                        }
                        GUILayout.EndScrollView();
                    }
                    else
                    {
                        GUILayout.Label("Not Found", "title", GUILayout.Height(60));
                        infoSentence = new Result(new DialogueTable.Row(), -1);
                    }
                }

                GUILayout.EndVertical();

                GUILayout.Space(10);
                GUILayout.Label("[     INFORMATIONS     ]", "title");
                GUILayout.Space(5);

                GUILayout.BeginHorizontal();
                GUILayout.Label("Sentence ID : " + (isInCustomDialogue && customDialogue == "" ? "???" : infoSentence.resultRow.ID));
                GUILayout.FlexibleSpace();
                GUILayout.Label("CSV File : " + (isInCustomDialogue && customDialogue == "" ? "???" : (infoSentence.resultIdCsv == -1 ? "custom" : allCsvFiles[infoSentence.resultIdCsv].name)), GUILayout.MinWidth(150));
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal("box2", GUILayout.Height(50));
                GUILayout.Label("FR : ", GUILayout.ExpandWidth(false));
                GUILayout.Label(infoSentence.resultRow.FR, GUILayout.ExpandWidth(false));
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal("box2", GUILayout.Height(50));
                GUILayout.Label("EN :", GUILayout.ExpandWidth(false));
                GUILayout.Label(infoSentence.resultRow.EN, GUILayout.ExpandWidth(false));
                GUILayout.EndHorizontal();

                if (GUILayout.Button("Add Active Sentence"))
                {
                    if (!isInCustomDialogue)
                    {
                        if (searchResult.Count > 0)
                            source.talking.Add(new SentenceConfig.Sentence(searchResult[idResultSelected].resultRow, DialogueControler.TEXT_ANIMATION.CHAR_ONSET, Speaker.EMOTION.NEUTRAL, searchResult[idResultSelected].resultIdCsv));
                    }
                    else if (customDialogue.Length > 0)
                    {
                        source.talking.Add(new SentenceConfig.Sentence(new DialogueTable.Row(customDialogue), DialogueControler.TEXT_ANIMATION.CHAR_ONSET, Speaker.EMOTION.NEUTRAL, -1));
                    }
                }
            }
            else
            {

            }
        }

        /*private void OnLostFocus()
        {
            //Close();
            Debug.Log("FOCUS EVENT");
        }*/

        private void Initialize()
        {
            scrollPosition = Vector2.zero;
            infoSentence = new Result(new DialogueTable.Row(), -1);

            searchResult.Clear();
            IDInput = "";
            customDialogue = "";
            isInCustomDialogue = true;

            idResultSelected = 0;

            csvName.Clear();
            csvName.Add("In all CSV");
            foreach (TextAsset other in allCsvFiles)
                csvName.Add(other.name);

            Init = true;
        }

        private void SearchForSentence()
        {
            if (allCsvFiles.Count == 0) return;
            searchResult.Clear();
            idResultSelected = 0;

            if (enumIdCsv == 0)
            {
                for (int j = 0; j < csvName.Count - 1; j++)
                {
                    LoadCSVFile(j);
                }
            }
            else
                LoadCSVFile(enumIdCsv - 1);

            if (searchResult.Count > 0)
                infoSentence = new Result(searchResult[0].resultRow, searchResult[0].resultIdCsv);
        }

        private void LoadCSVFile(int _idCSV)
        {
            DialogueTable table = new DialogueTable();
            table.Load(allCsvFiles[_idCSV]);
            if (table.IsLoaded())
                foreach (DialogueTable.Row row in table.FindAll_ID(IDInput))
                    searchResult.Add(new Result(row, _idCSV));
        }
    }
}