using UnityEngine;

public enum MusicType { title, menu, underground, inGame }

public class SoundManager : MonoBehaviour {

    public AudioSource sfxSource1;
    public AudioSource sfxSource2;
    public AudioSource sfxSource3;

    public static AudioSource sfxSourceStatic1;
    public static AudioSource sfxSourceStatic2;
    public static AudioSource sfxSourceStatic3;

    public AudioSource musicSource;
    public AudioSource musicSource2;
    public static AudioSource musicSourceStatic;

    public AudioClip[] atmos;

    public AudioClip titleMusic;
    public AudioClip menuMusic;
    public AudioClip inGameMusic;

    public AudioClip menuWooshClip;

    public AudioClip musicClip;

    AudioClip nextMusicClip;
    float musicVol;
    public float maxMusicVol;
    public float ambienceVolume;
    float targMusicVol;

    public static SoundManager instance;

    public 

    void Awake() {
        //DontDestroyOnLoad(transform.gameObject);
        if (instance != null) {
            Destroy(gameObject);
            return;
        }
        else {
            instance = this;
        }
    }

    // Use this for initialization
    void Start() {

        sfxSourceStatic1 = sfxSource1;
        sfxSourceStatic2 = sfxSource2;
        sfxSourceStatic3 = sfxSource3;
        musicSourceStatic = musicSource;

        musicVol = maxMusicVol;
        targMusicVol = maxMusicVol;

        musicSource.clip = atmos[Random.Range(0, atmos.Length)];
        musicSource.loop = true;
        musicSource.volume = ambienceVolume;
        musicSource.Play();

        if (musicClip != null) {
            musicSource2.clip = musicClip;
            musicSource2.volume = targMusicVol;
            musicSource2.Play();
        }
    }

    private void Update() {

        //musicVol += (targMusicVol - musicVol) * Time.deltaTime * 5f;
        //musicSource.volume = musicVol;
    }

    public static void PlaySfx(params AudioClip[] clips) {
        PlaySfx(1f, clips);
    }

    public static void PlaySfx(float vol, params AudioClip[] clips) {
        int index = Random.Range(0, clips.Length);
        PlaySingleSfx(clips[index], vol);
    }

    public static void PlaySingleSfx(AudioClip clip, float vol = 1f, int pitchIndex = -1) {
        if (GameManager.Instance.settings == null) return;
        if (!GameManager.Instance.settings.soundOn) return;
        pitchIndex = pitchIndex >= 0 ? pitchIndex : Random.Range(0, 3);
        switch (pitchIndex) {
            case 0: if (sfxSourceStatic1 != null) sfxSourceStatic1.PlayOneShot(clip, vol); break;
            case 1: if (sfxSourceStatic2 != null) sfxSourceStatic2.PlayOneShot(clip, vol); break;
            case 2: if (sfxSourceStatic3 != null) sfxSourceStatic3.PlayOneShot(clip, vol); break;
        }
    }

    public void ChangeMusic(MusicType type) {
        switch (type) {
            case MusicType.title: nextMusicClip = titleMusic; break;
            case MusicType.menu: nextMusicClip = menuMusic; break;
            case MusicType.inGame: nextMusicClip = inGameMusic; break;

        }
        targMusicVol = 0;
        CancelInvoke("SwapMusic");
        Invoke("SwapMusic", 0.6f);

    }

    void SwapMusic() {
        targMusicVol = GameManager.Instance.settings.musicOn ? maxMusicVol : 0;
        musicSource.clip = nextMusicClip;
        musicSource.Play();
    }

    public void UpdateMusicOnOff() {
        targMusicVol = GameManager.Instance.settings.musicOn ? maxMusicVol : 0;
        musicVol = targMusicVol;
    }


}
