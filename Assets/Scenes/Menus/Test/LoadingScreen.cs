using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    private bool mloadScene = false;
    [SerializeField]
    private int mScene;
    [SerializeField]
    private Text mLoadingText;


    void Update()
    {
        if(GetComponent<StageTransit>() && mloadScene == false)
        {
            mloadScene = true;
            mLoadingText.text = "Loading... ";
            StartCoroutine(LoadNewScene());
        }

        if(mloadScene == true)
        {
            mLoadingText.color = new Color(mLoadingText.color.r, mLoadingText.color.g, mLoadingText.color.b, Mathf.PingPong(Time.time, 1));

        }
    }

    IEnumerator LoadNewScene()
    {
        yield return new WaitForSeconds(3);
        AsyncOperation async = Application.LoadLevelAsync(mScene);

        while (!async.isDone)
        {
            yield return null;
        }
    }


	
}
