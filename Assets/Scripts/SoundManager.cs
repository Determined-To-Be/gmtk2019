using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource source;
    public static SoundManager instance = null;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
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
