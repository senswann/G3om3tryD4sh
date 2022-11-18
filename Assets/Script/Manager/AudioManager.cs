using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    //variable lié a la playlist de music
    public AudioClip[] playlist;
    public AudioSource audioSource;
    private int musicIndex = 0;

    //variable d'instance du AudioManager
    public static AudioManager instance;
    
    //Récupération de l'audioMix précis pour les sound effect   
    public AudioMixerGroup soundEffectMixer;

    //permet de créer une instance unique de AudioManager appelable avec AudioManager.instance
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("il y a plus d'une instance de AudioManager");
            return;
        }
        instance = this;
    }

    // lors du start de l'audio manager on joue la premiere music de la playlist
    void Start()
    {
        audioSource.clip = playlist[musicIndex];
        audioSource.Play();
    }

    // si la music se fini on lance la prochaine
    void Update()
    {
        if (!audioSource.isPlaying)
        {
            playNextSong();
        }
    }

    // on recherche la prochaine music est on la joue
    void playNextSong()
    {
        musicIndex = (musicIndex + 1) % playlist.Length;
        audioSource.clip = playlist[musicIndex];
        audioSource.Play();
    }

    //on joue un sound effect a une position données
    public AudioSource PlayClipAt(AudioClip sound, Vector3 pos)
    {
        GameObject TempGO = new GameObject("TempAudio");
        TempGO.transform.position = pos;
        AudioSource audio = TempGO.AddComponent<AudioSource>();
        audio.clip = sound;
        audio.outputAudioMixerGroup = soundEffectMixer;
        audio.Play();
        Destroy(TempGO, sound.length);
        return audio;
    }

    public void Reset()
    {
        audioSource.Stop();
        audioSource.Play();
    }
}
