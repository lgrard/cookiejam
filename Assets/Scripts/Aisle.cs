using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aisle : MonoBehaviour
{
    public Food food;
    public List<Transform> shelfPoints;

    private void OnDrawGizmos()
    {
        if(shelfPoints != null)
        {
            foreach(Transform lPoints in shelfPoints)
                Gizmos.DrawSphere(lPoints.position, 0.3f);
        }
    }
}
