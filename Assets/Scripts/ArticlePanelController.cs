using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ArticlePanelController : MonoBehaviour
{
    private Food ArticleToBuy;
    public GameObject Panel;
    public Text ItemName;
    public Button ValidateButton;
    public Button CancelButton;
    public UnityEvent<Food> OnBuyItem;
    public UnityEvent OnClose;
    
    public void RegisterArticleToBuyAndOpenPanel(Food NewArticleToBuy)
    {
        OpenPanel();
        ArticleToBuy = NewArticleToBuy;
        ItemName.text = ArticleToBuy.Name;
    }

    public void BuyItem()
    {
        OnBuyItem?.Invoke(ArticleToBuy);
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
