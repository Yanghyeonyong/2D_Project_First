using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] GameObject optionPanel;
    [SerializeField] GameObject titleUI;
    [SerializeField] GameObject shopPanel;
    bool optionOpen = false;
    bool shopOpen=false;
    public void OnOffOptionPanel()
    {
        optionOpen= !optionOpen;
        optionPanel.SetActive(optionOpen);
    }

    public void OnOffTitle(bool active)
    {
        titleUI.SetActive(active);
    }

    public void OnOffShop()
    {
        shopOpen = !shopOpen;
        shopPanel.SetActive(shopOpen);
    }
}
