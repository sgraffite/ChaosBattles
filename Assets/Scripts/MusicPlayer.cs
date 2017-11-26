using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour {

	private static MusicPlayer musicPlayerInstance = null;

    public List<string> musicCollectionKeys;
    public List<AudioClip> musicCollectionValues;

    private Dictionary<string, AudioClip> musicCollection;

    private AudioSource music;

    void OnEnable()
    {
        //Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
        Debug.Log("enable");
    }

    void OnDisable()
    {
        //Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    void Awake(){
		if(musicPlayerInstance != null && musicPlayerInstance != this)
        {
			Destroy(gameObject);
			Debug.Log("Duplicate music player self-destructing!");
			return;
		}

        musicPlayerInstance = this;
		GameObject.DontDestroyOnLoad(gameObject);
        music = GetComponent<AudioSource>();

        InitMusicCollection();
    }

    private void InitMusicCollection()
    {
        if (musicCollectionKeys.Count != musicCollectionValues.Count)
        {
            throw new System.Exception("Music collection must have the same number of keys as values!");
        }

        musicCollection = new Dictionary<string, AudioClip>();
        for (int i = 0; i < musicCollectionKeys.Count; i++)
        {
            musicCollection[musicCollectionKeys[i]] = musicCollectionValues[i];
        }
    }

    /*
    private void OnLevelWasLoaded(int level)
    {
        Debug.Log("Music player: loaded level " + level);

    }*/

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        var levelName = scene.name;
        Debug.Log("Music player: loaded level: " + levelName);
        Debug.Log(mode);
        music.Stop();
        LoadNewMusicByLevelName(levelName);
        music.loop = true;
        music.Play();
    }

    private void LoadNewMusicByLevelName(string levelName)
    {
        if (!musicCollection.ContainsKey(levelName))
        {
            Debug.Log("no music loaded");
            return;
        }

        music.clip = musicCollection[levelName];
    }
}
