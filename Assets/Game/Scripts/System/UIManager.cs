using UnityEngine;
using UnityEngine.InputSystem;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] GameObject optionPanel;
    public GameObject title;
    bool optionOpen = false;
    
    //옵션 창 활성화, 비활성화
    public void OnOffOptionPanel()
    {
        optionOpen= !optionOpen;
        optionPanel.SetActive(optionOpen);
    }

    //키 입력을 통한 활성화 비활성화
    public void onOffOptionPanel(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            optionPanel.SetActive(!optionPanel.activeSelf);
        }
    }
    //UI창 활성화
    public bool OnOffUI(GameObject ui, bool active)
    {
        active = !active;
        ui.SetActive(active);
        return active;
    }
}
