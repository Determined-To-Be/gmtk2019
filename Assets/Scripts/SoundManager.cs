using UnityEngine;
[RequireComponent (typeof(AudioSource))]

public class SoundManager : MonoBehaviour
{
    private AudioSource player;

    public enum PlayerSound { death, dopple, lurk, weep, drop, pickup, step, wall };
    public AudioClip[] clips;

    private static SoundManager _instance;
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
        player = GetComponent<AudioSource>();
    }

    void Awake()
    {
        if (_instance == null)
            _instance = GetComponent<SoundManager>();
        else if (_instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    public void PlaySound(PlayerSound sound, bool different = false)
    {
        if (different)
            player.pitch = Random.Range(.95f, 1.05f);
        else
            player.pitch = 1f;
        player.clip = clips[(int)sound];
        player.Play();
    }
}
