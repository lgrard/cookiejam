using System.IO;
using UnityEngine;
using UnityEditor;

// Declare type of Custom Editor
[CustomEditor(typeof(CharacterData))] //1
public class CharacterDataEditor : Editor 
{
    // Custom form for Player Preferences
    private CharacterData Target;
    
    private void OnEnable()
    {
        Target = (CharacterData) target;
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
        // {
        //     EditorGUILayout.BeginHorizontal();
        //     Target.Name = EditorGUILayout.TextField(new GUIContent("Name"), Target.Name);
        //     if (GUILayout.Button("Use file name"))
        //     {
        //         Target.Name = Path.GetFileNameWithoutExtension(AssetDatabase.GetAssetPath(Target));
        //     }
        //     EditorGUILayout.EndHorizontal();
        // }
        
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

        Header("Daily need");
        
        {
            EditorGUILayout.BeginHorizontal();
            const float max = 3500f;
            Target.EnergyNeed = EditorGUILayout.Slider(new GUIContent("EnergyNeed", "Kcal"), Target.EnergyNeed, 0f, max);

            GUI.contentColor = GetColorStepOfRangedValue(Target.EnergyNeed, max);
            GUILayout.Label("Kcal");
            GUI.contentColor = Color.white;
            EditorGUILayout.EndHorizontal();
        }

        {
            EditorGUILayout.BeginHorizontal();
            const float max = 100f;
            Target.FibersNeed = EditorGUILayout.Slider(new GUIContent("FibersNeed", "g"), Target.FibersNeed, 0f, max);
            
            GUI.contentColor = GetColorStepOfRangedValue(Target.FibersNeed, max);
            GUILayout.Label("g");
            GUI.contentColor = Color.white;
            EditorGUILayout.EndHorizontal();
        }

        {
            EditorGUILayout.BeginHorizontal();
            const float max = 300f;
            Target.ProteinNeed = EditorGUILayout.Slider(new GUIContent("ProteinNeed", "g"), Target.ProteinNeed, 0f, max);
            
            GUI.contentColor = GetColorStepOfRangedValue(Target.ProteinNeed, max);
            GUILayout.Label("g");
            GUI.contentColor = Color.white;
            EditorGUILayout.EndHorizontal();
        }

        {
            EditorGUILayout.BeginHorizontal();
            const float max = 3000f;
            Target.WaterNeed = EditorGUILayout.Slider(new GUIContent("WaterNeed", "mL"), Target.WaterNeed, 0f, max);
            
            GUI.contentColor = GetColorStepOfRangedValue(Target.WaterNeed, max);
            GUILayout.Label("mL");
            GUI.contentColor = Color.white;
            EditorGUILayout.EndHorizontal();
        }

        {
            EditorGUILayout.BeginHorizontal();
            const float max = 2500f;
            Target.CalciumNeed = EditorGUILayout.Slider(new GUIContent("CalciumNeed", "mg"), Target.CalciumNeed, 0f, max);
            
            GUI.contentColor = GetColorStepOfRangedValue(Target.CalciumNeed, max);
            GUILayout.Label("mg");
            GUI.contentColor = Color.white;
            EditorGUILayout.EndHorizontal();
        }

        {
            EditorGUILayout.BeginHorizontal();
            const float max = 150f;
            Target.FatNeed = EditorGUILayout.Slider(new GUIContent("FatNeed", "g"), Target.FatNeed, 0f, max);
            
            GUI.contentColor = GetColorStepOfRangedValue(Target.FatNeed, max);
            GUILayout.Label("g");
            GUI.contentColor = Color.white;
            EditorGUILayout.EndHorizontal();
        }

        {
            EditorGUILayout.BeginHorizontal();
            const float max = 550f;
            Target.SugarNeed = EditorGUILayout.Slider(new GUIContent("SugarNeed", "g"), Target.SugarNeed, 0f, max);
            
            GUI.contentColor = GetColorStepOfRangedValue(Target.SugarNeed, max);
            GUILayout.Label("g");
            GUI.contentColor = Color.white;
            EditorGUILayout.EndHorizontal();
        }
        
        if (GUILayout.Button("Set ideal values"))
        {
            Target.ProteinNeed = 60;
            Target.FatNeed = 75;
            Target.SugarNeed = 275;
            Target.FibersNeed = 45;
            Target.WaterNeed = 2500;
            Target.CalciumNeed = 1200;
            Target.EnergyNeed = 2000;
        }

        Header("Current need");
        {
            EditorGUILayout.BeginHorizontal();
            float max = Target.EnergyNeed;
            Target.CurrentEnergy = EditorGUILayout.Slider(new GUIContent("CurrentEnergy", "Kcal"), Target.CurrentEnergy, 0f, max);

            GUI.contentColor = GetColorStepOfRangedValue(Target.CurrentEnergy, max);
            GUILayout.Label("Kcal");
            GUI.contentColor = Color.white;
            EditorGUILayout.EndHorizontal();
        }

        {
            EditorGUILayout.BeginHorizontal();
            float max = Target.FibersNeed;
            Target.CurrentFibers = EditorGUILayout.Slider(new GUIContent("CurrentFibers", "g"), Target.CurrentFibers, 0f, max);
            
            GUI.contentColor = GetColorStepOfRangedValue(Target.CurrentFibers, max);
            GUILayout.Label("g");
            GUI.contentColor = Color.white;
            EditorGUILayout.EndHorizontal();
        }

        {
            EditorGUILayout.BeginHorizontal();
            float max = Target.ProteinNeed;
            Target.CurrentProtein = EditorGUILayout.Slider(new GUIContent("CurrentProtein", "g"), Target.CurrentProtein, 0f, max);
            
            GUI.contentColor = GetColorStepOfRangedValue(Target.CurrentProtein, max);
            GUILayout.Label("g");
            GUI.contentColor = Color.white;
            EditorGUILayout.EndHorizontal();
        }

        {
            EditorGUILayout.BeginHorizontal();
            float max = Target.WaterNeed;
            Target.CurrentWater = EditorGUILayout.Slider(new GUIContent("CurrentWater", "mL"), Target.CurrentWater, 0f, max);
            
            GUI.contentColor = GetColorStepOfRangedValue(Target.CurrentWater, max);
            GUILayout.Label("mL");
            GUI.contentColor = Color.white;
            EditorGUILayout.EndHorizontal();
        }

        {
            EditorGUILayout.BeginHorizontal();
            float max = Target.CalciumNeed;
            Target.CurrentCalcium = EditorGUILayout.Slider(new GUIContent("CurrentCalcium", "mg"), Target.CurrentCalcium, 0f, max);
            
            GUI.contentColor = GetColorStepOfRangedValue(Target.CurrentCalcium, max);
            GUILayout.Label("mg");
            GUI.contentColor = Color.white;
            EditorGUILayout.EndHorizontal();
        }

        {
            EditorGUILayout.BeginHorizontal();
            float max = Target.FatNeed;
            Target.CurrentFat = EditorGUILayout.Slider(new GUIContent("CurrentFat", "g"), Target.CurrentFat, 0f, max);
            
            GUI.contentColor = GetColorStepOfRangedValue(Target.CurrentFat, max);
            GUILayout.Label("g");
            GUI.contentColor = Color.white;
            EditorGUILayout.EndHorizontal();
        }

        {
            EditorGUILayout.BeginHorizontal();
            float max = Target.SugarNeed;
            Target.CurrentSugar = EditorGUILayout.Slider(new GUIContent("CurrentSugar", "g"), Target.CurrentSugar, 0f, max);
            
            GUI.contentColor = GetColorStepOfRangedValue(Target.CurrentSugar, max);
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
