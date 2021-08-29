using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FoodList", menuName = "ScriptableObjects/Food list", order = 1)]
public class FoodTypeList : ScriptableObject
{
    public List<Food> fruitsVegetables;
    public List<Food> hamFishEggs;
    public List<Food> dairy;
    public List<Food> starchy;
    public List<Food> sugar;
    public List<Food> beverage;
}
