using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BankWhiteFade : MonoBehaviour {

    [SerializeField] public Image whiteTexture;
    [SerializeField] public bool faded = false;
    [SerializeField] public bool doOnce = false;
    [SerializeField] public static float duration = 2.5f;

    [SerializeField] public ParticleSystem bankExplosions;

	// Use this for initialization
	void Start ()
    {
        duration = 2.5f;
        bankExplosions.Stop();
        whiteTexture = GameObject.FindGameObjectWithTag("WhiteBlastTexture").GetComponent<Image>();
        whiteTexture.enabled = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (LevelManager.bankDestroyed)
        {
            whiteTexture.enabled = true;
            bankExplosions.Play();

            if (!doOnce)
            {
                whiteTexture.canvasRenderer.SetAlpha(0.01f);
                whiteTexture.CrossFadeAlpha(1f, 1.5f, false);

                doOnce = true;
            }

            if (duration > 0)
            {
                duration -= Time.deltaTime;
            }
            else if (duration <= 0)
            {
                faded = true;
                GameObject.Find("Bank").GetComponent<BankAnimator>().animator.SetBool("BankDestroyed", true);
            }

            if (faded)
            {
                whiteTexture.CrossFadeAlpha(0.01f, 1.5f, false);
                bankExplosions.gameObject.SetActive(false);
                faded = false;


            }
        }
    }
}
