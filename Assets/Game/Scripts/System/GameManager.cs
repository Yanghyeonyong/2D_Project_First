using System.Collections;
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
    public PlayerData playerData;

    [SerializeField] GameObject player;
    [SerializeField] Transform playerSpawnPos;

    void Start()
    {
        GetGameData();

    }


    void Update()
    {
        
    }

    public IEnumerator MoveScene(int move)
    {
        curStage+=move;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(curStage);
        Debug.Log("플레이어 소환시도");
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        //yield return new WaitUntil(() => asyncLoad.isDone);


        if (curStage > 0 || curStage < 3)
        {
            Instantiate(player, new Vector3(0, 1, 0), Quaternion.identity);
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
        Application.Quit();
    }
    private void OnApplicationQuit()
    {
        SetGameData();
    }



    private void GetGameData()
    {
        playerModel = new PlayerModel(playerData.hp, playerData.mp, playerData.defence, playerData.damage,
    playerData.attackRange, playerData.moveSpeed, playerData.jumpForce);
    }

    private void SetGameData()
    {
        playerData.hp = playerModel.MaxHp;
        playerData.hp = playerModel.MaxMp;
        playerData.defence = playerModel.Defence;
        playerData.damage = playerModel.Damage;
        playerData.attackRange = playerModel.AttackRange;
        playerData.moveSpeed = playerModel.MoveSpeed;
        playerData.jumpForce = playerModel.JumpForce;
    }
}
