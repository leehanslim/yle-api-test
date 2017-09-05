using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleScreenFadeToLoad : MonoBehaviour 
{
	[SerializeField]
	private float fadeInDelay = 3.0f;

	[SerializeField]
	private float fadeOutDelay = 2.0f;

	[SerializeField]
	private string sceneToLoad = "";

	[SerializeField]
	private UISimpleFade simpleFade;

	void Start()
	{
		StartCoroutine(Wait(fadeInDelay, FadeIn));
	}

	private IEnumerator Wait(float seconds, System.Action onComplete)
	{
		yield return new WaitForSeconds(seconds);
		if (onComplete != null) onComplete();
	}

	private void FadeIn()
	{
		simpleFade.FadeIn( () => {
			StartCoroutine(Wait(fadeOutDelay, FadeOut));
		});
	}

	private void FadeOut()
	{
		simpleFade.FadeOut( () => {
			StartCoroutine(Wait(1.5f, LoadScene));
		});
	}

	private void LoadScene()
	{
		SceneManager.LoadScene(sceneToLoad);
	}
}
