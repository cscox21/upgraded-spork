using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = 0.7f;
    [Range(0.5f, 1.5f)]
    public float pitch = 1f;

    [Range(0f, 0.5f)]
    public float randVolume = 0.1f;
    [Range(0f, 0.5f)]
    public float randPitch = 0.1f;

    private AudioSource source;

    public void SetSource(AudioSource _source)
    {
        source = _source;
        source.clip = clip;
    }
    public void Play()
    {
        source.volume = volume * (1 +Random.Range(-randVolume/2f, randVolume/2f));
        source.pitch = pitch *(1 + Random.Range(-randPitch / 2f, randPitch / 2f)); ;
        source.Play();
    }
}

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;
    [SerializeField]
    Sound[] sounds;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one AudioManager in the scene.");
        }
        else
        {
            instance = this;
        }
        
    }

    void Start()
    {
        for (int i = 0; i <sounds.Length; i++)
        {
            GameObject _go = new GameObject("Sound_" + i + "_" + sounds[i].name);
            _go.transform.SetParent(transform);
            sounds[i].SetSource (_go.AddComponent<AudioSource>());
        }

        //PlaySound("GraveyardMusic");
        //Can get rid of later and put somewhere else
    }

    public void PlaySound(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if(sounds[i].name == _name)
            {
                sounds[i].Play();
                return;
            }
        }

        //no sounds with _name
        Debug.LogWarning("AudioManager: Sound not found not found in list, " + _name);
    }

}
