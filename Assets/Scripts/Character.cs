using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class Character : MonoBehaviour
{
    public Camera cam;
    
    const float MIN_ENERGY_NEED = 200f;
    const float MAX_ENERGY_NEED = 300f;

    const float MIN_FIBER_NEED = 50f;
    const float MAX_FIBER_NEED = 100f;

    const float MIN_PROTEIN_NEED = 200f;
    const float MAX_PROTEIN_NEED = 300f;

    const float MIN_WATER_NEED = 2000f;
    const float MAX_WATER_NEED = 2300f;

    const float MIN_CALCIUM_NEED = 2100f;
    const float MAX_CALCIUM_NEED = 2500f;
                    
    const float MIN_FAT_NEED = 100f;
    const float MAX_FAT_NEED = 150f;

    const float MIN_SUGAR_NEED = 375f;
    const float MAX_SUGAR_NEED = 550f;
    
    public uint maxItems = 4;
    public uint itemCount = 0;
    public Food[] foodItems;
    
    private NavMeshAgent m_navAgent;
    private Food m_foodToBuy = null;
    private bool m_shouldEscape = false;
    public CharacterData characterData;
    
    public UnityEvent OnEscape;
    public UnityEvent<Food> OnTryBuyArticle;
    public bool CanMove = false;
    

#if UNITY_EDITOR
    [Header("Debug")]
    [SerializeField] protected bool m_useMobileInput = false;
#endif

    void Awake()
    {
        m_navAgent = GetComponent<NavMeshAgent>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Reset();
    }
    
    void GenerateRandomCharaterData()
    {
        characterData = ScriptableObject.CreateInstance<CharacterData>();
        characterData.EnergyNeed = MAX_ENERGY_NEED;
        characterData.CurrentEnergy = Random.Range(MIN_ENERGY_NEED, characterData.EnergyNeed);
    
        characterData.FibersNeed = MAX_FIBER_NEED;
        characterData.CurrentFibers = Random.Range(MIN_FIBER_NEED, characterData.FibersNeed);
        
        characterData.ProteinNeed = MAX_PROTEIN_NEED;
        characterData.CurrentProtein = Random.Range(MIN_PROTEIN_NEED, characterData.ProteinNeed);

        characterData.WaterNeed = MAX_WATER_NEED;
        characterData.CurrentWater = Random.Range(MIN_WATER_NEED, characterData.WaterNeed);
    
        characterData.CalciumNeed = MAX_CALCIUM_NEED;
        characterData.CurrentCalcium = Random.Range(MIN_CALCIUM_NEED, characterData.CalciumNeed);
    
        characterData.FatNeed = MAX_FAT_NEED;
        characterData.CurrentFat = Random.Range(MIN_FAT_NEED, characterData.FatNeed);
    
        characterData.SugarNeed = MAX_SUGAR_NEED;
        characterData.CurrentSugar = Random.Range(MIN_SUGAR_NEED, characterData.SugarNeed);
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

        if (isClic && CanMove)
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                m_foodToBuy = null;
                m_shouldEscape = false;
                
                if (hit.transform.tag == "aisle")
                {
                    if (itemCount < maxItems)
                    {
                        m_foodToBuy = hit.transform.gameObject.GetComponent<Aisle>().food;
                        Debug.Log("Food select : " + m_foodToBuy.ToString());
                    }
                    else
                    {
                        Debug.Log("Inventory full"); 
                    }
                }
                else if (hit.transform.tag == "escape")
                {
                    Debug.Log("Should escape request"); 
                    m_shouldEscape = true;
                }
                
                m_navAgent.SetDestination(hit.point);
                Debug.DrawLine(cam.transform.position, hit.point, Color.green, 1f);
            }
        }
        
        // Check if we've reached the destination
        if (!m_navAgent.pathPending &&
            m_navAgent.remainingDistance <= m_navAgent.stoppingDistance &&
            (!m_navAgent.hasPath || m_navAgent.velocity.sqrMagnitude == 0f))
        {
            if (m_foodToBuy)
            {
                Debug.Log("Try to buy food : " + m_foodToBuy.ToString());
                OnTryBuyArticle?.Invoke(m_foodToBuy);
                m_foodToBuy = null;
            }
            else if(m_shouldEscape)
            {
                Debug.Log("Escape"); 
                OnEscape?.Invoke();
                m_shouldEscape = false;
            }
        }
    }

    public void Reset()
    {
        foodItems = new Food[maxItems];
        itemCount = 0;
        m_foodToBuy = null;
        m_shouldEscape = false;
    }

    public void BuyArticle(Food ArticleToBuy)
    {
        Debug.Log(ArticleToBuy.ToString() +  itemCount + " " + foodItems.Length);
        foodItems[itemCount] = ArticleToBuy;
        itemCount++;
    }
}
