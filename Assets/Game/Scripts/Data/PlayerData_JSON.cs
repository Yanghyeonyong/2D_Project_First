using System.IO;
using UnityEngine;
[System.Serializable]

public class PlayerData_JSON : MonoBehaviour
{
    //public GameObject player;
    private string filePath;
    public PlayerModel playerModel;

    private void Awake()
    {
        filePath = Path.Combine(Application.persistentDataPath, "playerData.json");
    }

    public void SaveData(PlayerModel savePlayerModel)
    {
        playerModel=savePlayerModel;
        string json = JsonUtility.ToJson(playerModel, true);

        File.WriteAllText(filePath, json);
    }

    public PlayerModel LoadData()
    {
        //해당 파일이 존재하는지 경로 확인해보고 참 거짓 판단
        if (File.Exists(filePath))
        {
            Debug.Log(filePath);
            //파일에서 문자열을 읽어옴
            string json = File.ReadAllText(filePath);

            //문자열을 다시 객체의 형태로 변환
            PlayerModel player = JsonUtility.FromJson<PlayerModel>(json);
            return player;
        }
        return null;
    }
}
