using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class MainGame : MonoBehaviour {


    private static MainGame instance = null;
    public static string errorMessage = "";
    private float load_on_delay = -1f;
    private string to_load = "BlueScreen";
    public bool autoplay = false;
    public static AudioClip win;
    public AudioClip win_sound;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            GameObject.DontDestroyOnLoad(gameObject);
            loadLevel("Start");
        }
        else
        {
            errorMessage = "Duplicate MainGame object created.";
            Debug.LogError(errorMessage);
            loadLevel("BlueScreen");
        }
    }

    public static MainGame getInstance()
    {
        return instance;
    }

    public static void loadLevel(string name)
    {
        Debug.Log("Loading scene " + name);
        if (name.Equals("Start"))
        {
            instance.autoplay = true;
        }
        SceneManager.LoadScene(name);
    }

    public static void noMoreBricks()
    {
        string current_scene = SceneManager.GetActiveScene().name;
        if (current_scene.StartsWith("Level_") || current_scene.Equals("Game"))
        {
            AudioSource.PlayClipAtPoint(win, Vector3.zero, 2.0f);

        }
        if (current_scene.StartsWith("Level_"))
        {
            string next_scene = "Level_" + (int.Parse(current_scene.Substring(6)) + 1);
            if (!Application.CanStreamedLevelBeLoaded(next_scene))
            {
                next_scene = "Win";
            }
            loadLevel(next_scene, 1.5f);
            return;
        }
        switch (current_scene)
        {
            case "Lose":
                loadLevel("Start", 1.5f);
                break;
            case "Win":
                loadLevel("Start", 1.5f);
                break;
            case "Game":
                loadLevel("Level_2", 1.5f);
                break;
            default:
                loadLevel(current_scene, 1.5f);
                break;
        }
    }

    public static void loadLevel(string name, float delay)
    {
        if (!instance)
        {
            errorMessage = "Attempt to load level before MainGame instance exists.";
            Debug.LogError(errorMessage);
             loadLevel("BlueScreen");
        }
        if (instance.load_on_delay >= 0)
        {
            errorMessage = "Race condition on loading scenes";
            Debug.LogError(errorMessage);
             loadLevel("BlueScreen");
        }
        
        instance.load_on_delay = delay;
        instance.to_load = name;
    }

    // Use this for initialization
    void Start () {
        win = win_sound;
	}

    // Update is called once per frame
    void Update() {
        string current_scene = SceneManager.GetActiveScene().name;
        if (load_on_delay >= 0)
        {
            load_on_delay -= Time.deltaTime;
            if (load_on_delay <= 0)
            {
                loadLevel(to_load);
                load_on_delay = -1;
                return;
            }
            if (!autoplay)
            {
                return;
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse Button Down");
            if (autoplay)
            {
                cancel_load();
                Debug.Log("Turning off autoplay");
                autoplay = false;
                if (current_scene.Equals("Start"))
                {
                    loadLevel("Game");
                }
                else
                {
                    loadLevel("Start");
                }
            }else
            {
                switch (current_scene)
                {
                    case "Lose":
                        loadLevel("Start");
                        break;
                    case "Start":
                        loadLevel("Game");
                        break;
                    default:
                        break;
                }
            }
        }
        else if (autoplay && load_on_delay < 0)
        {
            //Debug.Log("Autoplay on; current scene: " + current_scene);
            switch (current_scene)
            {
                case "Lose":
                    loadLevel("Start", 10.0f);
                    break;
                case "Win":
                    loadLevel("Start", 10.0f);
                    break;
                case "Start":
                    loadLevel("Game", 10.0f);
                    break;
                default:
                    break;
            }
        }
	}

    private void cancel_load()
    {
        load_on_delay = -1;
    }
}
