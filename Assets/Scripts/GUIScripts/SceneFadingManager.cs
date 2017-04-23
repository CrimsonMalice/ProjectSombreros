using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFadingManager : MonoBehaviour 
{
   
    public Texture2D fadeOut;
	[SerializeField]
	private float fadeSpeed = 1.5f;

	private int drawDepth = 1000;
	private float alpha = 1.0f;
	public static int fadeDir = -1;

	void OnGUI()
	{
		alpha += fadeDir * fadeSpeed * Time.deltaTime;
		alpha = Mathf.Clamp01 (alpha);
		GUI.color = new Color (GUI.color.r, GUI.color.g, GUI.color.b, alpha);
		GUI.depth = drawDepth;
		GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeOut);

	}

	public float BeginFade (int startDirection)
	{
		fadeDir = startDirection;
		return (fadeSpeed);
	}

	public void OnLevelWasLoaded()
	{
		BeginFade (-1);

	}


}
