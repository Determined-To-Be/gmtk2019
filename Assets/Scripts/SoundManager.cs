using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource source;

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

    void Awake()
    {
        if (_instance == null)
            _instance = GetComponent<SoundManager>();
        else if (_instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    void Update()
    {

    }

    /*
     * PlaySound(sound):
     * - death
     * - item
     *   - drop
     *   - pickup
     * - step wall
     * 
     * PlaySound(sound, true):
     * - step
     * - enemy
     *   - dopple
     *   - lurk
     *   - weep
     */
    public void PlaySound(AudioClip sound, bool different = false)
    {
        if (different)
            source.pitch = Random.Range(.95f, 1.05f);
        else
            source.pitch = 1f;
        source.clip = sound;
        source.Play();
    }
}
