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
    [SerializeField] int curStage = 0;

    public PlayerModel playerModel;
    public PlayerView playerView;
    public PlayerData initialPlayerData;
    public PlayerData_JSON playerData;

    [SerializeField] GameObject player;
    [SerializeField] Transform playerSpawnPos;
    [SerializeField] GameObject playerCamera;

    void Start()
    {
        GetGameData();

    }


    void Update()
    {

    }

    public IEnumerator MoveScene(int move)
    {
        curStage += move;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(curStage);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        //yield return new WaitUntil(() => asyncLoad.isDone);


        if (curStage > 0 || curStage < 3)
        {
            GameObject myPlayer = Instantiate(player, new Vector3(0, 1, 0), Quaternion.identity);
            GameObject myCamera = Instantiate(playerCamera);
            myCamera.GetComponent<CinemachineCamera>().Target.TrackingTarget = myPlayer.transform;
            UIManager.Instance.OnOffUI(UIManager.Instance.title, true);
            //UIManager.Instance.OnOffTitle(false);
        }
        else if (curStage == 0)
        {
            UIManager.Instance.OnOffUI(UIManager.Instance.title, false);
        }

    }

    public void TitleScene()
    {
        curStage = 0;
        SceneManager.LoadScene(curStage);
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
            Debug.Log("데이터 있어서 쓴다");
            return;
        }
        else
        {
            Debug.Log("데이터 없어서 새로 만든다");
            playerModel = new PlayerModel(initialPlayerData.hp, initialPlayerData.mp, initialPlayerData.defence, initialPlayerData.damage,
        initialPlayerData.attackRange, initialPlayerData.moveSpeed, initialPlayerData.jumpForce, initialPlayerData.gold);
            playerData.SaveData(playerModel);
        }
    }

    private void SetGameData()
    {
        playerData.SaveData(playerModel);
        //        playerData.hp = playerModel.MaxHp;
        //        playerData.hp = playerModel.MaxMp;
        //        playerData.defence = playerModel.Defence;
        //        playerData.damage = playerModel.Damage;
        //        playerData.attackRange = playerModel.AttackRange;
        //        playerData.moveSpeed = playerModel.MoveSpeed;
        //        playerData.jumpForce = playerModel.JumpForce;

        //        playerData.gold = playerModel.Gold;
        //#if UNITY_EDITOR
        //        EditorUtility.SetDirty(playerData);
        //#endif
    }
}
