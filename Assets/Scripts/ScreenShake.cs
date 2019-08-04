using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{

	private static ScreenShake _instance;
	public static ScreenShake instance
	{
		get
		{
			if (_instance != null)
			{
				return _instance;
			}

			GameObject go = new GameObject();
			_instance = go.AddComponent<ScreenShake>();
			return _instance;
		}

	}

	public Vector3 initPos;

	// Start is called before the first frame update
	void Start()
    {
		initPos = this.transform.position;
    }

	public void Shake(float intensity, float duration) {
		StartCoroutine(shakeScreen(intensity, duration));
	}

	IEnumerator shakeScreen(float intensity, float duration) {
		float x = Random.Range(0, 100), y = Random.Range(0, 100);
		while (duration > 0) {
			this.transform.position = initPos + intensity * new Vector3(Mathf.PerlinNoise(x + duration, y + duration), Mathf.PerlinNoise(x + duration, y + duration));
			duration -= Time.deltaTime;
			yield return null;
		}
		this.transform.position = initPos;
	}
}
