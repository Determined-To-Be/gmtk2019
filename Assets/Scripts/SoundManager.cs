using System.Collections;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]

public class SoundManager : MonoBehaviour
{
    AudioSource[] player; // 0 = player, 1 = ambient bg, 2 = ambient sfx

    public enum PlayerSound { death, dopple, lurk, weep, drop, pickup, step, wall };
    public float variety;
    public AudioClip[] playerClips;
    public AudioClip[] ambientTracks;

    static SoundManager _instance;
    public static SoundManager instance
    {
        get
        {
            if (_instance != null)
            {
                return _instance;
            }

            GameObject go = new GameObject();
            _instance = go.AddComponent<SoundManager>();
            return _instance;
        }
    }

    void Start()
    {
        player = GetComponents<AudioSource>();
        PlayAmbientTracks();
    }

    void Awake()
    {
        if (_instance == null)
            _instance = GetComponent<SoundManager>();
        else if (_instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    IEnumerator doAudioClip(PlayerSound sound, bool different)
    {
        player[0].pitch = different ? Random.Range(1f - variety, 1f + variety) : 1f;
        player[0].clip = playerClips[(int)sound];
        player[0].Play();
        yield return new WaitWhile(() => player[0].isPlaying);
    }

    public void PlaySound(PlayerSound sound, bool different = false)
    {
        StartCoroutine(doAudioClip(sound, different));
    }

    IEnumerator doAmbientTracks()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(45f, 105f));
            player[1].clip = ambientTracks[Mathf.FloorToInt(Random.Range(0f, (ambientTracks.Length - Mathf.Epsilon)))];
            player[1].Play();
            yield return new WaitWhile(() => player[1].isPlaying);
        }
    }

    void PlayAmbientTracks()
    {
        StartCoroutine(doAmbientTracks());
    }
}
