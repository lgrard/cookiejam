using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "ScriptableObjects/Character", order = 1)]
public class CharacterData : ScriptableObject
{
    [Header("Identity")]
    public string Name;
    public GameObject MeshPrefab;

    [Header("Daily need")]
    public float FibersNeed;
    public float EnergyNeed;
    public float ProteinNeed;
    public float WaterNeed;
    public float CalciumNeed;
    public float FatNeed;
    public float SugarNeed;
    
    [Header("Current need")]
    public float CurrentFibers;
    public float CurrentEnergy;
    public float CurrentProtein;
    public float CurrentWater;
    public float CurrentCalcium;
    public float CurrentFat;
    public float CurrentSugar;


    // [ContextMenu("SetIdealValues")]
    // public void FileNameToNameField()
    // {
    //     Name = Path.GetFileNameWithoutExtension( AssetDatabase.GetAssetPath(this));
    // }
}