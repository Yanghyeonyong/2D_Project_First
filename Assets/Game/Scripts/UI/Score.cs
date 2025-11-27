using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    [SerializeField] GameObject scoreObject;
    [SerializeField] TextMeshProUGUI scoreText;
    public void OpenScoreObject()
    {
        scoreText.text ="Best Score : " + GameManager.Instance.playerModel.Score;
        UIManager.Instance.OnOffUI(scoreObject, false);
    }
    public void ExitScoreObject()
    {
        UIManager.Instance.OnOffUI(scoreObject, true);
    }
}
