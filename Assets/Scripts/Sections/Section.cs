using System.Collections.Generic;
using UnityEngine;

class Section : MonoBehaviour
{
    private const float SECTION_TYPE_COUNT = 6;
    private const string  FOOD_LIST_PATH = "Prefabs/Sections/foodList";

    private static Dictionary<SectionType, int> sectionCount;
    private static FoodTypeList foodList;

    [SerializeField] private List<Aisle> aisleList;
    private SectionType type;

    private void Start()
    {
        if (sectionCount == null)
        {
            sectionCount = new Dictionary<SectionType, int>();
            sectionCount.Add(SectionType.fruitsVegetables, 0);
            sectionCount.Add(SectionType.hamFishEggs, 0);
            sectionCount.Add(SectionType.dairy, 0);
            sectionCount.Add(SectionType.starchy, 0);
            sectionCount.Add(SectionType.sugar, 0);
            sectionCount.Add(SectionType.beverage, 0);

            foodList = Resources.Load<FoodTypeList>(FOOD_LIST_PATH);
        }

        SetSectionType();
        PopulateAisle();
    }

    private void SetSectionType()
    {
        float lRand = Random.Range(0f, 1f);
        float lOddBase = 1f / SECTION_TYPE_COUNT;

        for (int i= 1; i <= SECTION_TYPE_COUNT; i++)
        {
            if (lRand < i * lOddBase && sectionCount[type] < 3)
            {
                type = (SectionType)i - 1;
                sectionCount[type] += 1;
                break;
            }
        }
    }

    private void PopulateAisle()
    {
        List<Food> lList;

        switch (type)
        {
            case SectionType.fruitsVegetables:  lList = foodList.fruitsVegetables; break;
            case SectionType.hamFishEggs:       lList = foodList.hamFishEggs; break;
            case SectionType.dairy:             lList = foodList.dairy; break;
            case SectionType.starchy:           lList = foodList.starchy; break;
            case SectionType.sugar:             lList = foodList.sugar; break;
            default:                            lList = foodList.beverage; break;
        }

        for (int i = 0 ; i < aisleList.Count; i++)
        {
            int lRand = Random.Range(0, lList.Count);
            aisleList[i].food = lList[lRand];
        }
    }
}