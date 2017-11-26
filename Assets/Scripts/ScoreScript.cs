using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour {

    private static int score = 0;
    private Text textComponent;

    public void Start()
    {
        textComponent = GetComponent<Text>();
    }

    public void Add(int points)
    {
        score += points;
        textComponent.text = GetScore();
    }

    public static string GetScore()
    {
        return score.ToString("000000000");
    }

    public static void Reset()
    {
        score = 0;
    }
}
