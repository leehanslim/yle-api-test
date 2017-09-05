using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISimpleFade : MonoBehaviour 
{
	[SerializeField]
	private Image imageRef;

	[SerializeField]
	private float maxFadeDuration = 1.0f;

	public void FadeIn(System.Action onComplete)
	{
		StartCoroutine(FadeInRoutine(onComplete));
	}

	public void FadeOut(System.Action onComplete)
	{
		StartCoroutine(FadeOutRoutine(onComplete));
	}

	private IEnumerator FadeInRoutine(System.Action onComplete)
	{
		yield return null;

		float fadeTime = 0.0f;
		while (imageRef.color.a > 0) {
			imageRef.color = new Color(imageRef.color.r, imageRef.color.g, imageRef.color.b, Mathf.Lerp(1, 0, Mathf.InverseLerp(0, maxFadeDuration, fadeTime)));

			if (fadeTime >= maxFadeDuration)
				break;
			
			fadeTime += Time.deltaTime;
			yield return null;
		}

		if (onComplete != null) onComplete();
	}

	private IEnumerator FadeOutRoutine(System.Action onComplete)
	{
		yield return null;

		float fadeTime = 0.0f;
		while (imageRef.color.a < 1) {
			imageRef.color = new Color(imageRef.color.r, imageRef.color.g, imageRef.color.b, Mathf.Lerp(0, 1, Mathf.InverseLerp(0, maxFadeDuration, fadeTime)));

			if (fadeTime >= maxFadeDuration)
				break;

			fadeTime += Time.deltaTime;
			yield return null;
		}

		if (onComplete != null) onComplete();
	}
}
