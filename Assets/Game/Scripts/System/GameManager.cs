using System.Collections;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{

    //0 : 타이틀  1 : 마을 2 : 타워 3 : 엔딩
    public int curStage = 0;

    //플레이어 능력치
    public PlayerModel playerModel;
    //던전용 플레이어 능력치
    public PlayerModel_Dongeon playerModel_Dongeon;
    //플레이어 UI 관련
    public PlayerView playerView;
    //데이터 없을 경우초기 능력치
    public PlayerData initialPlayerData;
    //플레이어 데이터
    public PlayerData_JSON playerData;

    //플레이어
    [SerializeField] GameObject player;
    //플레이어 생성 위치
    [SerializeField] Vector3[] playerSpawnPos;
    //플레이어 촬영 카메라
    [SerializeField] GameObject playerCamera;
    //무적 상태
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

    //BGM
    [SerializeField] AudioClip[] backGroundAudios;
    public AudioClip[] BackGroundAudios => backGroundAudios;

    //시작 시 데이터 가져오기 시도
    void Start()
    {
        GetGameData();
    }

    //씬 이동
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

        //타이틀이나 엔딩 아니면 플레이어 소환
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
        //던전용 플레이어 능력치 생성
        if (curStage == 2)
        {
            playerModel_Dongeon = new PlayerModel_Dongeon(playerModel);
        }
        else
        {
            playerModel_Dongeon = null;
        }

    }

    //타이틀 씬 이동
    public void TitleScene()
    {
        curStage = 0;
        SceneManager.LoadScene(curStage);
        SoundManager.Instance.PlayBGM(BackGroundAudios[curStage]);
        UIManager.Instance.OnOffUI(UIManager.Instance.title, false);
    }

    //게임 종료
    public void GameExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            // 빌드된 애플리케이션에서는 종료합니다.
            Application.Quit();
#endif
    }
    //종료시 데이터 저장
    private void OnApplicationQuit()
    {
        SetGameData();
    }



    private void GetGameData()
    {
        //데이터 가져오기 시도
        playerModel = playerData.LoadData();
        if (playerModel != null)
        {
            return;
        }
        //없으면 새로 제작
        else
        {
            playerModel = new PlayerModel(initialPlayerData.hp, initialPlayerData.mp, initialPlayerData.defence, initialPlayerData.damage,
        initialPlayerData.attackRange, initialPlayerData.moveSpeed, initialPlayerData.jumpForce, initialPlayerData.gold);
            playerData.SaveData(playerModel);
        }
    }

    //데이터 저장
    private void SetGameData()
    {
        playerData.SaveData(playerModel);
    }
}
