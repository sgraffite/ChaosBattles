using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {

	void Start () {
        Text scoreText = GetComponent<Text>();
        scoreText.text = ScoreScript.GetScore();
        ScoreScript.Reset();

    }
}
