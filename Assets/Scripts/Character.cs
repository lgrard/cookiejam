using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class Character : MonoBehaviour
{
    public Camera cam;

    public uint maxItems = 4;
    private uint itemCount = 0;
    public Food[] foodItems; 
    private NavMeshAgent navAgent;
    private Food foodToBuy = null;
    private bool shouldEscape = false;
    public UnityEvent OnEscape;

#if UNITY_EDITOR
    [Header("Debug")]
    [SerializeField] protected bool m_useMobileInput = false;
#endif

    void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        foodItems = new Food[maxItems];
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
                foodToBuy = null;
                shouldEscape = false;
                
                if (hit.transform.tag == "aisle")
                {
                    if (itemCount < maxItems)
                    {
                        foodToBuy = hit.transform.gameObject.GetComponent<Aisle>().food;
                        Debug.Log("Food select : " + foodToBuy.ToString());
                    }
                    else
                    {
                        Debug.Log("Inventory full"); 
                    }
                }
                else if (hit.transform.tag == "escape")
                {
                    Debug.Log("Should escape request"); 
                    shouldEscape = true;
                }
                
                navAgent.SetDestination(hit.point);
                Debug.DrawLine(cam.transform.position, hit.point, Color.green, 1f);
            }
        }
        
        // Check if we've reached the destination
        if (!navAgent.pathPending &&
            navAgent.remainingDistance <= navAgent.stoppingDistance &&
            (!navAgent.hasPath || navAgent.velocity.sqrMagnitude == 0f))
        {
            if (foodToBuy)
            {
                Debug.Log("Try to buy food : " + foodToBuy.ToString());
                foodItems[itemCount] = foodToBuy;
                itemCount++;
                foodToBuy = null;
            }
            else if(shouldEscape)
            {
                Debug.Log("Escape"); 
                OnEscape?.Invoke();
                shouldEscape = false;
            }
        }
    }
}
