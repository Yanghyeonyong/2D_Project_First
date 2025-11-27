using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    int sceneNum;
    public void NextScene()
    {
        //StartCoroutine(GameManager.Instance.MoveScene(1));
        GameManager.Instance.StartCoroutine(GameManager.Instance.MoveScene(1));
    }
    public void PrevScene()
    {
        //StartCoroutine(GameManager.Instance.MoveScene(-1));
        GameManager.Instance.StartCoroutine(GameManager.Instance.MoveScene(-1));
    }
    public void TitleScene()
    {
        GameManager.Instance.TitleScene();
    }
    public void VillageScene()
    {
        //StartCoroutine(GameManager.Instance.MoveScene(0,1));
        GameManager.Instance.StartCoroutine(GameManager.Instance.MoveScene(0, 1));
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
