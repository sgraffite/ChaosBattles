using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    private static int currentLevel = 0;

    private Scene[] scenes;

    void Start(){
        if (GetCurrentLevelName().StartsWith("Level_"))
        {
            //InvokeRepeating("CheckForBreakableBricks", 1.0f, 0.2f);
            currentLevel = SceneManager.GetActiveScene().buildIndex;
        }
	}

    public string GetCurrentLevelName()
    {
        return SceneManager.GetActiveScene().name;
    }

	public void LoadLevel(string name){
		//Debug.Log("Level load requested " + name);
        SceneManager.LoadScene(name);
        if (GetCurrentLevelName().StartsWith("Level_"))
        {
            currentLevel = SceneManager.GetActiveScene().buildIndex;
        }
    }

    public void ReloadCurrentLevel()
    {
        LoadSceneByIndex(currentLevel);
    }

	public void LoadNextLevel(){
        var nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        //Debug.Log(UnityEditor.EditorBuildSettings.scenes.Length);
        LoadSceneByIndex(nextSceneIndex);

    }

    private void LoadSceneByIndex(int sceneIndex)
    {
        if (sceneIndex >= UnityEditor.EditorBuildSettings.scenes.Length)
        {
            Debug.LogError("Already at last scene!");
            return;
        }

        Debug.Log("Currrent Level Index = " + currentLevel);
        SceneManager.LoadScene(UnityEditor.EditorBuildSettings.scenes[sceneIndex].path);
        if (GetCurrentLevelName().StartsWith("Level_"))
        {
            currentLevel = SceneManager.GetActiveScene().buildIndex;
        }
        Debug.Log("New Level Index = " + currentLevel);
    }

	public void LoseLevel(){
		LoadLevel("Lose screen");
	}

	public void QuitRequest(){
		Debug.Log("Quit requested.");
	}
}
