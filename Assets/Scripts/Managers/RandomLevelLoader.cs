using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RandomLevelLoader : MonoBehaviour {

    [SerializeField] private string[] levels;
    public float timer = 0;
    public float timerStart = 4.66f;

	// Use this for initialization
	void Start ()
    {
        timer = timerStart;
        print(LevelManager.currentLevel);
    }

    // Update is called once per frame
    void Update ()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else if (timer <= 0)
        {
            string newLevel;
            do
            {
                newLevel = levels[Random.Range(0, levels.Length)];
            }
            while (newLevel == LevelManager.currentLevel);

            SceneManager.LoadScene(newLevel);
        }
    }
}
