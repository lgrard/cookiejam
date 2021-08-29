using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum EGameState
{
    CHARACTER_INTRODUCTION,
    MARKET,
    SCORE,
    COUNT
}

public class GameManager : MonoBehaviour
{
    [Header("Market timer")]
    [Tooltip("in second")]
    public float MaxMarketTimer = 30f;
    private float MarketTimer = 0f;
    public Slider TimerSilder;


    [Header("Game setting")]
    private EGameState GameState = EGameState.CHARACTER_INTRODUCTION;

    public GameObject UICharachterIntro;
    public GameObject UIMarket;
    public GameObject UIScore;

    
    
    // Start is called before the first frame update
    void Start()
    {
        SwitchUIWithGameState();
        TimerSilder.maxValue = MaxMarketTimer;
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
                break;
            case EGameState.COUNT:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void SetMarketGameState()
    {
        SetGameState(EGameState.MARKET);
    }
    
    public void SetCharacterIntroGameState()
    {
        SetGameState(EGameState.CHARACTER_INTRODUCTION);
    }
    
    public void SetScoreGameState()
    {
        SetGameState(EGameState.SCORE);
    }
    
    void SetGameState(EGameState NewGameState)
    {
        GameState = NewGameState;
        SwitchUIWithGameState();
    }
    
    
    void SwitchUIWithGameState()
    {
        UICharachterIntro.SetActive(GameState == EGameState.CHARACTER_INTRODUCTION);
        UIMarket.SetActive(GameState == EGameState.MARKET);
        UIScore.SetActive(GameState == EGameState.SCORE);
    }
}
