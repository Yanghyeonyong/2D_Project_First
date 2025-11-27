using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    //씬 이동 스크립트
    public void NextScene()
    {
        GameManager.Instance.StartCoroutine(GameManager.Instance.MoveScene(1));
    }
    public void PrevScene()
    {
        GameManager.Instance.StartCoroutine(GameManager.Instance.MoveScene(-1));
    }
    public void TitleScene()
    {
        GameManager.Instance.TitleScene();
    }
    public void VillageScene()
    {
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
