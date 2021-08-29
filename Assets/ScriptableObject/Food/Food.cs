using System.IO;
using UnityEditor;
using UnityEditor.MemoryProfiler;
using UnityEngine;

[CreateAssetMenu(fileName = "FoodType", menuName = "ScriptableObjects/Food", order = 1)]
public class Food : ScriptableObject
{
    public string Name;
    public GameObject MeshPrefab;

    public float Fibers;
    public float Energy;
    public float Protein;
    public float Water;
    public float Calcium;
    public float Fat;
    public float Sugar;
    
    public float SpawnRate;


    //[ContextMenu("Use file name as name")]
    //public void FileNameToNameField()
    //{
    //    Name = Path.GetFileNameWithoutExtension( AssetDatabase.GetAssetPath(this));
    //}
}