using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] GameObject scoreObject;
    [SerializeField] TextMeshProUGUI scoreText;

    //점수 페이지 열람 및 점수 기반으로 텍스트 조정
    public void OpenScoreObject()
    {
        scoreText.text ="Best Score : " + GameManager.Instance.playerModel.Score;
        UIManager.Instance.OnOffUI(scoreObject, false);
    }
    //점수 페이지 비활성화
    public void ExitScoreObject()
    {
        UIManager.Instance.OnOffUI(scoreObject, true);
    }
}
