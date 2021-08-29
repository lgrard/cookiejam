using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aisle : MonoBehaviour
{
    public Food food;
    public List<Transform> shelfPoints;

    private bool hasBeenPicked = false;

    [SerializeField] private ParticleSystem collectionParticles;

    public Food PickFood()
    {
        if (hasBeenPicked)
            return null;

        hasBeenPicked = true;
        foreach (Transform lPoints in shelfPoints)
            lPoints.gameObject.SetActive(false);
        collectionParticles.Play();

        return food;
    }

    private void OnDrawGizmos()
    {
        if(shelfPoints != null)
        {
            foreach(Transform lPoints in shelfPoints)
                Gizmos.DrawSphere(lPoints.position, 0.3f);
        }
    }
}
