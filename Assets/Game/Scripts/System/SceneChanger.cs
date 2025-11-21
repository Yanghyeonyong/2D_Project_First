using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    int sceneNum;
    public void NextScene()
    {
        sceneNum=GameManager.Instance.NextScene();
        if(sceneNum==0 )
            UIManager.Instance.OnOffTitle(true);
        else
            UIManager.Instance.OnOffTitle(false);
    }
    public void PrevScene()
    {
        GameManager.Instance.PrevScene();
        if (sceneNum == 0)
            UIManager.Instance.OnOffTitle(true);
        else
            UIManager.Instance.OnOffTitle(false);
    }
    public void TitleScene()
    {
        GameManager.Instance.TitleScene();
        UIManager.Instance.OnOffTitle(true);
    }

    public void GameExit()
    {
        //Application.Quit();

        //에디터 내에서 종료 테스트용
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }
}
