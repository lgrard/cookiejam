using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LevelGenerator : MonoBehaviour
{
    private const string GROUND_MASK_NAME = "Default";
    private const string RESSOURCE_PATH = "Prefabs/Sections/section_";
    private const float CELL_SIZE = 12;
    private const int SECTION_COUNT = 4;

    private Transform levelContainer;
    private System.Action onEndGeneration;
    private System.Action onEndNavMeshBuild;

    private NavMeshDataInstance navMeshDataInstance;

    private void Start()
    {        
        InitLevelContainer();
        GenerateLevel(3);
        UpdateNavigation();
    }

    private void InitLevelContainer() => levelContainer = new GameObject("LevelContainer").transform;


    /// Generate a random square disposition for a given size
    /// <param name="pSize">Number of side to generate</param>
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

    /// Update Nav mesh data
    private void UpdateNavigation()
    {
        navMeshDataInstance.Remove();

        List<NavMeshBuildSource> buildSources = new List<NavMeshBuildSource>();

        NavMeshBuilder.CollectSources(
            transform.parent,
            LayerMask.GetMask(GROUND_MASK_NAME),
            NavMeshCollectGeometry.PhysicsColliders,
            0,
            new List<NavMeshBuildMarkup>(),
            buildSources);

        NavMeshData navData = NavMeshBuilder.BuildNavMeshData(
            NavMesh.GetSettingsByID(0),
            buildSources,
            new Bounds(Vector3.zero, new Vector3(10000, 10000, 10000)),
            Vector3.down,
            Quaternion.Euler(Vector3.up));

        navMeshDataInstance = NavMesh.AddNavMeshData(navData);

        onEndNavMeshBuild?.Invoke();
    }
}
