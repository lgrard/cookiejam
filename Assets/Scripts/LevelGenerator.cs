using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private const float CELL_SIZE = 12;
    private const string RESSOURCE_PATH = "Prefabs/Sections/section_";
    private const int SECTION_COUNT = 4;

    private Transform levelContainer;
    private System.Action onEndGeneration;

    private void Start()
    {
        InitLevelContainer();
        GenerateLevel(3);
    }

    private void InitLevelContainer() => levelContainer = new GameObject("LevelContainer").transform;

    private void GenerateLevel(int pSize)
    {
        List<int> lRots = new List<int>() { 0, 90, -90, 180 };
        Vector3 lPos = Vector3.zero;

        for(int i = 0; i < pSize; i++)
        {
            for (int j = 0; j < pSize; j++)
            {
                int lSectionIndex = Random.Range(0, SECTION_COUNT);

                Transform lSection =
                    Instantiate(Resources.Load<Transform>(RESSOURCE_PATH + lSectionIndex),
                    levelContainer);

                lSection.localPosition = lPos;
                lPos.x += CELL_SIZE;

                int lRandRot = Random.Range(0, 4);
                lSection.Rotate(0,lRots[lRandRot],0);
            }
            
            lPos.x = 0;
            lPos.z += CELL_SIZE;
        }

        onEndGeneration?.Invoke();
    }
}
