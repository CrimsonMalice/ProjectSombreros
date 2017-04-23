using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RandomLevelLoader : MonoBehaviour {

    [SerializeField] private string[] levels;
    float timer = 0;
    float timerStart = 4.66f;

	// Use this for initialization
	void Start ()
    {
        timer = timerStart;
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
            SceneManager.LoadScene(levels[Random.Range(0, levels.Length)]);
        }
	}
}
