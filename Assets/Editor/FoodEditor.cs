using System;
using System.IO;
using UnityEngine;
using UnityEditor;

// Declare type of Custom Editor
[CustomEditor(typeof(Food))] //1
public class FoodEditor : Editor 
{
    // Custom form for Player Preferences
    private Food Target;
    
    private void OnEnable()
    {
        Target = (Food) target;
    }
    
    Editor gameObjectEditor;
    Texture2D previewBackgroundTexture;
    
    // OnInspector GUI
    public override void OnInspectorGUI()
    {
        Header("Identity");

        EditorGUILayout.BeginHorizontal();
        {
            GUIStyle bgColor = new GUIStyle();
            bgColor.normal.background = previewBackgroundTexture;

            //display the mesh
            if (Target.MeshPrefab != null)
            {
                if (gameObjectEditor == null)
                    gameObjectEditor = Editor.CreateEditor(Target.MeshPrefab);

                gameObjectEditor.OnInteractivePreviewGUI(GUILayoutUtility.GetRect(100, 100), bgColor);
            }
        }
        EditorGUILayout.BeginVertical();
        {
            EditorGUILayout.BeginHorizontal();
            Target.Name = EditorGUILayout.TextField(new GUIContent("Name"), Target.Name);
            if (GUILayout.Button("Use file name"))
            {
                Target.Name = Path.GetFileNameWithoutExtension(AssetDatabase.GetAssetPath(Target));
            }
            EditorGUILayout.EndHorizontal();
        }
        
        {
            EditorGUI.BeginChangeCheck();
            Target.MeshPrefab = (GameObject)EditorGUILayout.ObjectField("Mesh" ,Target.MeshPrefab, typeof(GameObject), true);

            if (EditorGUI.EndChangeCheck())
            {
                gameObjectEditor = Editor.CreateEditor(Target.MeshPrefab);
            }
        }
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();

        Header("Food attributes");
        
        {
            EditorGUILayout.BeginHorizontal();
            const float max = 350f;
            Target.Energy = EditorGUILayout.Slider(new GUIContent("Energy", "Kcal"), Target.Energy, 0f, max);

            GUI.contentColor = GetColorStepOfRangedValue(Target.Energy, max);
            GUILayout.Label("Kcal");
            GUI.contentColor = Color.white;
            EditorGUILayout.EndHorizontal();
        }

        {
            EditorGUILayout.BeginHorizontal();
            const float max = 10f;
            Target.Fibers = EditorGUILayout.Slider(new GUIContent("Fiber", "g"), Target.Fibers, 0f, max);
            
            GUI.contentColor = GetColorStepOfRangedValue(Target.Fibers, max);
            GUILayout.Label("g");
            GUI.contentColor = Color.white;
            EditorGUILayout.EndHorizontal();
        }

        {
            EditorGUILayout.BeginHorizontal();
            const float max = 30f;
            Target.Protein = EditorGUILayout.Slider(new GUIContent("Protein", "g"), Target.Protein, 0f, max);
            
            GUI.contentColor = GetColorStepOfRangedValue(Target.Protein, max);
            GUILayout.Label("g");
            GUI.contentColor = Color.white;
            EditorGUILayout.EndHorizontal();
        }

        {
            EditorGUILayout.BeginHorizontal();
            const float max = 230f;
            Target.Water = EditorGUILayout.Slider(new GUIContent("Water", "mL"), Target.Water, 0f, max);
            
            GUI.contentColor = GetColorStepOfRangedValue(Target.Water, max);
            GUILayout.Label("mL");
            GUI.contentColor = Color.white;
            EditorGUILayout.EndHorizontal();
        }

        {
            EditorGUILayout.BeginHorizontal();
            const float max = 250f;
            Target.Calcium = EditorGUILayout.Slider(new GUIContent("Calcium", "mg"), Target.Calcium, 0f, max);
            
            GUI.contentColor = GetColorStepOfRangedValue(Target.Calcium, max);
            GUILayout.Label("mg");
            GUI.contentColor = Color.white;
            EditorGUILayout.EndHorizontal();
        }

        {
            EditorGUILayout.BeginHorizontal();
            const float max = 15f;
            Target.Fat = EditorGUILayout.Slider(new GUIContent("Fat", "g"), Target.Fat, 0f, max);
            
            GUI.contentColor = GetColorStepOfRangedValue(Target.Fat, max);
            GUILayout.Label("g");
            GUI.contentColor = Color.white;
            EditorGUILayout.EndHorizontal();
        }

        {
            EditorGUILayout.BeginHorizontal();
            const float max = 55f;
            Target.Sugar = EditorGUILayout.Slider(new GUIContent("Sugar", "g"), Target.Sugar, 0f, max);
            
            GUI.contentColor = GetColorStepOfRangedValue(Target.Sugar, max);
            GUILayout.Label("g");
            GUI.contentColor = Color.white;
            EditorGUILayout.EndHorizontal();
        }

        {
            if (GUILayout.Button("Save"))
            {
                EditorUtility.SetDirty(Target);
            }
        }
    }

    Color GetColorStepOfRangedValue(float value, float max)
    {
        float ratio = value / max;
        if (ratio < 0.25)
            return Color.white;
        
        if (ratio < 0.5)
            return Color.green;
        
        if (ratio < 0.75)
            return Color.yellow;
        
        return Color.red;
    }

    void Header(string name)
    {
        EditorGUILayout.Separator();
        GUILayout.Label(name, EditorStyles.boldLabel);
        EditorGUILayout.Separator();
    }
}
