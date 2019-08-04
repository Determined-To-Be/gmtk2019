using System.Collections;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]

public class SoundManager : MonoBehaviour
{
    AudioSource[] player; // 0 = player, 1 = ambient bg, 2 = ambient sfx

    public enum PlayerSound { death, dopple, lurk, weep, drop, pickup, step, wall };
    public float variety = .075f;
    public AudioClip[] playerClips, ambientClips, ambientTracks;

    static SoundManager _Instance;
    public static SoundManager Instance
    {
        get
        {
            if (_Instance != null)
            {
                return _Instance;
            }

            GameObject go = new GameObject();
            _Instance = go.AddComponent<SoundManager>();
            return _Instance;
        }
    }

    void Start()
    {
        player = GetComponents<AudioSource>();
        PlayAmbientTracks();
        PlayAmbientClips();
    }

    void Awake()
    {
        if (_Instance == null)
            _Instance = GetComponent<SoundManager>();
        else if (_Instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    IEnumerator DoAudioClip(PlayerSound sound, bool different)
    {
        player[0].pitch = different ? Random.Range(1f - variety, 1f + variety) : 1f;
        player[0].clip = playerClips[(int)sound];
        player[0].Play();
        yield return new WaitWhile(() => player[0].isPlaying);
    }

    public void PlaySound(PlayerSound sound, bool different = false)
    {
        StartCoroutine(DoAudioClip(sound, different));
    }

    IEnumerator DoAmbientTracks()
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
        StartCoroutine(DoAmbientTracks());
    }

    IEnumerator DoAmbientClips()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(10f, 30f));
            player[2].clip = ambientClips[Mathf.FloorToInt(Random.Range(0f, (ambientClips.Length - Mathf.Epsilon)))];
            player[2].Play();
            yield return new WaitWhile(() => player[2].isPlaying);
        }
    }

    void PlayAmbientClips()
    {
        StartCoroutine(DoAmbientClips());
    }
}
