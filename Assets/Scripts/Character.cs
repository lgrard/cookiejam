using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Character : MonoBehaviour
{
    public Camera cam;
    public NavMeshAgent nav;
    
#if UNITY_EDITOR
    [Header("Debug")]
    [SerializeField] protected bool m_useMobileInput = false;
#endif
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        bool isClic = (!m_useMobileInput && Input.GetMouseButton(0)) || Input.touchCount == 1;        
#elif UNITY_STANDALONE
        bool isClic = Input.GetMouseButton(0);
#else
        bool isClic = Input.touchCount == 1;
#endif        

        if (isClic)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                nav.SetDestination(hit.point);
                Debug.DrawLine(cam.transform.position, hit.point, Color.green, 1f);
            }
        }    
    }
}
