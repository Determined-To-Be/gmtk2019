using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{

	public static ScreenShake _instance;
	public static ScreenShake instance
	{
		get
		{
			if (_instance != null)
			{
				return _instance;
			}

			GameObject go = Instantiate(new GameObject());
			go.AddComponent<TileTime>();
			return _instance;
		}

	}

	public Vector3 initPos;

	// Start is called before the first frame update
	void Start()
    {
		initPos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	void Shake(float intensity, float duration) {
		StartCoroutine(shakeScreen(intensity, duration));
	}

	IEnumerator shakeScreen(float intensity, float duration) {

		

		this.transform.position = initPos;
		yield return null;
	}
}
