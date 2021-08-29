using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public enum EGameState
{
    CHARACTER_INTRODUCTION,
    MARKET,
    SCORE,
    COUNT
}

public struct ItemRotationSetting
{
    public float rotationSpeed;
    public Quaternion itemRotationAngle;
};

public class GameManager : MonoBehaviour
{
    [Header("Character intro settings")]
    public GameObject UICharachterIntro;
    
    
    [Header("Market settings")]
    public GameObject UIMarket;
    [Tooltip("in second")]
    public float MaxMarketTimer = 30f;
    private float MarketTimer = 0f;
    public Slider TimerSilder;
    public Character GameCharacter;
    
    
    [Header("Score setting")]
    public GameObject UIScore;
    private ItemRotationSetting[] ItemRotationSettings;
    public Transform[] UIItemPlace;
    public Slider FibersNeedSliders;   
    public Slider EnergyNeedSliders;   
    public Slider ProteinNeedSliders;   
    public Slider WaterNeedSliders;   
    public Slider CalciumNeedSliders;   
    public Slider FatNeedSliders;
    public Slider SugarNeedSliders;
    
    [Tooltip("In second")]
    public float itemGiveScoreDuration = 3f;
    private float tLerpScore = 0f;
    private uint currentItem = 0;
    
    [Header("Game setting")]
    private EGameState GameState = EGameState.CHARACTER_INTRODUCTION;

    
    // Start is called before the first frame update
    void Start()
    {
        SwitchUIWithGameState();
        TimerSilder.maxValue = MaxMarketTimer;
        ItemRotationSettings = new ItemRotationSetting[GameCharacter.maxItems];
    }

    // Update is called once per frame
    void Update()
    {
        switch (GameState)
        {
            case EGameState.CHARACTER_INTRODUCTION:
                break;
            case EGameState.MARKET:
                MarketTimer += Time.deltaTime;
                if (MarketTimer > MaxMarketTimer)
                {
                    SetScoreGameState();
                    MarketTimer = 0;
                }
                TimerSilder.value = MarketTimer;
                break;
            case EGameState.SCORE:
                for (uint i = currentItem; i < GameCharacter.itemCount; i++)
                {
                    UIItemPlace[i].rotation = Quaternion.Lerp(UIItemPlace[i].rotation, UIItemPlace[i].rotation * ItemRotationSettings[i].itemRotationAngle, Time.deltaTime * ItemRotationSettings[i].rotationSpeed);
                }
                
                tLerpScore += Time.deltaTime / itemGiveScoreDuration;
                if (tLerpScore > 1f)
                    tLerpScore = 1f;
                
                FibersNeedSliders.value = Mathf.Lerp(GameCharacter.characterData.CurrentFibers, GameCharacter.characterData.CurrentFibers + GameCharacter.foodItems[currentItem].Fibers, tLerpScore);
                EnergyNeedSliders.value = Mathf.Lerp(GameCharacter.characterData.CurrentEnergy, GameCharacter.characterData.CurrentEnergy + GameCharacter.foodItems[currentItem].Energy, tLerpScore);
                ProteinNeedSliders.value = Mathf.Lerp(GameCharacter.characterData.CurrentProtein, GameCharacter.characterData.CurrentProtein + GameCharacter.foodItems[currentItem].Protein, tLerpScore);
                WaterNeedSliders.value =Mathf.Lerp(GameCharacter.characterData.CurrentWater, GameCharacter.characterData.CurrentWater + GameCharacter.foodItems[currentItem].Water, tLerpScore);
                CalciumNeedSliders.value = Mathf.Lerp(GameCharacter.characterData.CurrentCalcium, GameCharacter.characterData.CurrentCalcium + GameCharacter.foodItems[currentItem].Calcium, tLerpScore);
                FatNeedSliders.value = Mathf.Lerp(GameCharacter.characterData.CurrentFat, GameCharacter.characterData.CurrentFat + GameCharacter.foodItems[currentItem].Fat, tLerpScore);
                SugarNeedSliders.value = Mathf.Lerp(GameCharacter.characterData.CurrentSugar, GameCharacter.characterData.CurrentSugar + GameCharacter.foodItems[currentItem].Sugar, tLerpScore);

                if (tLerpScore == 1f)
                {
                    GameCharacter.characterData.CurrentFibers = FibersNeedSliders.value;
                    GameCharacter.characterData.CurrentEnergy = EnergyNeedSliders.value;
                    GameCharacter.characterData.CurrentProtein = ProteinNeedSliders.value;
                    GameCharacter.characterData.CurrentWater = WaterNeedSliders.value;
                    GameCharacter.characterData.CurrentCalcium = CalciumNeedSliders.value;
                    GameCharacter.characterData.CurrentFat = FatNeedSliders.value;
                    GameCharacter.characterData.CurrentSugar = SugarNeedSliders.value;
                        
                    tLerpScore = 0f;
                    if (++currentItem >= GameCharacter.itemCount)
                    {
                        //TODO: Remove for smooth anim
                        SetCharacterIntroGameState();
                    }
                }
                
                break;
            case EGameState.COUNT:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void SetMarketGameState()
    {
        GameCharacter.enabled = true;
        SetGameState(EGameState.MARKET);
    }
    
    public void SetCharacterIntroGameState()
    {
        GameCharacter.enabled = false;
        SetGameState(EGameState.CHARACTER_INTRODUCTION);
    }
    
    public void SetScoreGameState()
    {
        for (int i = 0; i < GameCharacter.itemCount; i++)
        {
            ItemRotationSettings[i].rotationSpeed = Random.Range(0.4f, 0.8f);
            ItemRotationSettings[i].itemRotationAngle = Random.rotation;
        }

        UpdateScoresSliders();
        
        GameCharacter.enabled = false;
        SetGameState(EGameState.SCORE);
        AddItemToScore();
    }

    void UpdateScoresSliders()
    {
        FibersNeedSliders.maxValue = GameCharacter.characterData.FibersNeed;
        FibersNeedSliders.value = GameCharacter.characterData.CurrentFibers;
        
        EnergyNeedSliders.maxValue = GameCharacter.characterData.EnergyNeed;
        EnergyNeedSliders.value = GameCharacter.characterData.CurrentEnergy;
        
        ProteinNeedSliders.maxValue = GameCharacter.characterData.ProteinNeed;
        ProteinNeedSliders.value = GameCharacter.characterData.CurrentProtein;
        
        WaterNeedSliders.maxValue = GameCharacter.characterData.WaterNeed;
        WaterNeedSliders.value = GameCharacter.characterData.CurrentWater;
        
        CalciumNeedSliders.maxValue = GameCharacter.characterData.CalciumNeed;
        CalciumNeedSliders.value = GameCharacter.characterData.CurrentCalcium;
                
        FatNeedSliders.maxValue = GameCharacter.characterData.FatNeed;
        FatNeedSliders.value = GameCharacter.characterData.CurrentFat;
                
        SugarNeedSliders.maxValue = GameCharacter.characterData.SugarNeed;
        SugarNeedSliders.value = GameCharacter.characterData.CurrentSugar;
    }

    void SetGameState(EGameState NewGameState)
    {
        GameState = NewGameState;
        SwitchUIWithGameState();
    }

    void AddItemToScore()
    {
        for (int i = 0; i < GameCharacter.itemCount; i++)
        {
             Instantiate(GameCharacter.foodItems[i].MeshPrefab, UIItemPlace[i]);
        }
    }
    
    void SwitchUIWithGameState()
    {
        UICharachterIntro.SetActive(GameState == EGameState.CHARACTER_INTRODUCTION);
        UIMarket.SetActive(GameState == EGameState.MARKET);
        UIScore.SetActive(GameState == EGameState.SCORE);
    }
}