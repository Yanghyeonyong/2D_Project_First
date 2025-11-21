using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] GameObject optionPanel;
    public GameObject title;
    bool optionOpen = false;
    //bool shopOpen=false;

    //나중에 게임에서 esc 누르면 켜지도록 설정하자
    public void OnOffOptionPanel()
    {
        optionOpen= !optionOpen;
        optionPanel.SetActive(optionOpen);
    }

    public bool OnOffUI(GameObject ui, bool active)
    {
        active = !active;
        ui.SetActive(active);
        return active;
    }
}
