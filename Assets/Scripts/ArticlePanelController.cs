using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ArticlePanelController : MonoBehaviour
{
    private Aisle AisleToBuy;
    public GameObject Panel;
    public Text ItemName;
    public Button ValidateButton;
    public Button CancelButton;
    public UnityEvent<Aisle> OnBuyItem;
    public UnityEvent OnClose;
    
    public void RegisterArticleToBuyAndOpenPanel(Aisle NewAisle)
    {
        OpenPanel();
        AisleToBuy = NewAisle;
        ItemName.text = AisleToBuy.food.Name;
    }

    public void BuyItem()
    {
        OnBuyItem?.Invoke(AisleToBuy);
        ClosePanel();
    }
    
    public void OpenPanel()
    {
        Panel.SetActive(true);
    }

    public void ClosePanel()
    {
        OnClose?.Invoke();
        Panel.SetActive(false);
    }
}
