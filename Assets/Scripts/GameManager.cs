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
                for (int i = 0; i < GameCharacter.itemCount; i++)
                {
                    UIItemPlace[i].rotation = Quaternion.Lerp(UIItemPlace[i].rotation, UIItemPlace[i].rotation * ItemRotationSettings[i].itemRotationAngle, Time.deltaTime * ItemRotationSettings[i].rotationSpeed);
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

        GameCharacter.enabled = false;
        SetGameState(EGameState.SCORE);
        AddItemToScore();
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
