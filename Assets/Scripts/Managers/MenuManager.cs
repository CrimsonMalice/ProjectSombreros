using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{
    [SerializeField] private bool isPlaying;

	// Use this for initialization
	void Start ()
    {
		((MovieTexture)GetComponent<Renderer>().material.mainTexture).Play();
        Renderer renderer = GetComponent<Renderer>();
        MovieTexture movie = (MovieTexture)renderer.material.mainTexture;
        isPlaying = true;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButtonDown("Enter"))
        {
            LoadLevel();
        }
	}

    void OnAwake()
    {

    }

    public void LoadLevel()
    {
        SceneManager.LoadScene("Level_1");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
