using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace TeamSeven
{

    [CustomEditor(typeof(IndiceController))]
    [CanEditMultipleObjects]
    public class IndiceControllerEditor : Editor
    {
        public GUIStyle _titles;
        public GUIStyle _redText;

        private IndiceController _source;

        private int _indiceIndex;

        public void OnEnable()
        {
            _source = (IndiceController)target;
        }

        /*public override void OnInspectorGUI()
        {
            InitStyle();

            // database selection
            _source.indiceDB = EditorGUILayout.ObjectField(_source.indiceDB, typeof(IndiceDataBase), true) as IndiceDataBase;

            if(_source.indiceDB == null)
            {
                EditorGUILayout.LabelField("no evidence data base selected yet");
            }
            else
            {
                //evidence selection
                DrawEvidenceSelection();

                //evidence details
                DrawEvidencePreview();
            }

            void DrawEvidenceSelection()
            {
                if (_source.indiceDB == null || _source.indiceDB.indiceDatas.Count == 0)
                    return;

                List<string> indiceNames = new List<string>();
                foreach (IndiceData oneData in _source.indiceDB.indiceDatas)
                    indiceNames.Add(oneData.name);

                GUILayout.Space(15);
                EditorGUILayout.BeginHorizontal("box");
                GUILayout.Label("Select the evidence you want:", _titles);
                GUILayout.Space(10);
                _indiceIndex = EditorGUILayout.Popup(_indiceIndex == 0 ? 0 : _indiceIndex, indiceNames.ToArray());
                _source.data = _source.indiceDB.indiceDatas[_indiceIndex];

                Debug.Log(_indiceIndex);
                EditorGUILayout.EndHorizontal();
            }

            void DrawEvidencePreview()
            {
                EditorGUILayout.BeginVertical("box");

                GUILayout.Label("Evidence data preview", _titles);
                GUILayout.Space(10);

                EditorGUILayout.BeginHorizontal();
                _source.data.name = EditorGUILayout.TextField("Evidence Name", _source.data.name);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                _source.data.sceneID = EditorGUILayout.IntField("Scene Index", _source.data.sceneID);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                _source.data.sceneSprite = EditorGUILayout.ObjectField("Scene Sprite", _source.data.sceneSprite, typeof(Sprite), true) as Sprite;
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                _source.data.detailSprite = EditorGUILayout.ObjectField("Detailed Sprite", _source.data.detailSprite, typeof(Sprite), true) as Sprite;
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                _source.data.inventorySprite = EditorGUILayout.ObjectField("Inventory Sprite", _source.data.inventorySprite, typeof(Sprite), true) as Sprite;
                EditorGUILayout.EndHorizontal();


                EditorGUILayout.EndVertical();
            }
        }*/

        public void InitStyle()
        {
            _titles = GUI.skin.label;
            _titles.alignment = TextAnchor.MiddleCenter;
            _titles.fontStyle = FontStyle.Bold;
        }
    }
}
