using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    enum Stage
    {
        Title, Village, Tower, Ending
    }
    [SerializeField] Stage currentStage = Stage.Title;

    public PlayerModel playerModel;
    public PlayerData playerData;



    void Start()
    {
        GetGameData();

    }


    void Update()
    {
        
    }

    public int NextScene()
    {
        currentStage++;
        SceneManager.LoadScene((int)currentStage);
        return (int) currentStage;
    }
    public int PrevScene()
    {
        currentStage--;
        SceneManager.LoadScene((int)currentStage);
        return (int)currentStage;
    }
    public void TitleScene()
    {
        currentStage=Stage.Title;
        SceneManager.LoadScene((int)currentStage);
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
