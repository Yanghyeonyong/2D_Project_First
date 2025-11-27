using System.Collections;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    //enum Stage
    //{
    //    Title, Village, Tower, Ending
    //}
    //[SerializeField] Stage currentStage = Stage.Title;

    //0 : 타이틀  1 : 마을 2 : 타워 3 : 엔딩
    public int curStage = 0;

    public PlayerModel playerModel;
    //이건 던전에서 레벨업 스탯(나오면 초기화됨)
    public PlayerModel_Dongeon playerModel_Dongeon;
    public PlayerView playerView;
    public PlayerData initialPlayerData;
    public PlayerData_JSON playerData;

    [SerializeField] GameObject player;
    [SerializeField] Vector3[] playerSpawnPos;
    [SerializeField] GameObject playerCamera;
    private bool isInvincible = false;
    public bool IsInvincible
    {
        get
        {
            return isInvincible;
        }
        set
        {
            isInvincible = value;
        }
    }

    [SerializeField] AudioClip[] backGroundAudios;
    public AudioClip[] BackGroundAudios => backGroundAudios;

    void Start()
    {
        GetGameData();

    }

    public IEnumerator MoveScene(int move, int indexMove=-1)
    {
        if (indexMove == -1)
        {
            curStage += move;
        }
        else
        {
            curStage = indexMove;
        }
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(curStage);

        SoundManager.Instance.PlayBGM(BackGroundAudios[curStage]);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        if (curStage > 0 && curStage < 3)
        {
            GameObject myPlayer = Instantiate(player, playerSpawnPos[curStage - 1], Quaternion.identity);
            GameObject myCamera = Instantiate(playerCamera);
            myCamera.GetComponent<CinemachineCamera>().Target.TrackingTarget = myPlayer.transform;
            UIManager.Instance.OnOffUI(UIManager.Instance.title, true);
        }
        else if (curStage == 0)
        {
            UIManager.Instance.OnOffUI(UIManager.Instance.title, false);
        }

        if (curStage == 2)
        {
            playerModel_Dongeon = new PlayerModel_Dongeon(playerModel);
        }
        else
        {
            playerModel_Dongeon = null;
        }

    }

    public void TitleScene()
    {
        curStage = 0;
        SceneManager.LoadScene(curStage);
        SoundManager.Instance.PlayBGM(BackGroundAudios[curStage]);
        UIManager.Instance.OnOffUI(UIManager.Instance.title, false);
    }


    public void GameExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            // 빌드된 애플리케이션에서는 종료합니다.
            Application.Quit();
#endif
    }
    private void OnApplicationQuit()
    {
        SetGameData();
    }



    private void GetGameData()
    {
        playerModel = playerData.LoadData();
        if (playerModel != null)
        {
            return;
        }
        else
        {
            Debug.Log("저장된 데이터가 없어서 새로 제작");
            playerModel = new PlayerModel(initialPlayerData.hp, initialPlayerData.mp, initialPlayerData.defence, initialPlayerData.damage,
        initialPlayerData.attackRange, initialPlayerData.moveSpeed, initialPlayerData.jumpForce, initialPlayerData.gold);
            playerData.SaveData(playerModel);
        }
    }

    private void SetGameData()
    {
        Debug.Log("데이터를 저장");
        playerData.SaveData(playerModel);
    }
}
