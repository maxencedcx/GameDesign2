using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoSingleton<AudioManager>
{
    [SerializeField] private AudioClip Do;
    [SerializeField] private AudioClip Re;
    [SerializeField] private AudioClip Mi;
    [SerializeField] private AudioClip Fa;
    [SerializeField] private AudioClip Sol;
    [SerializeField] private AudioClip La;
    [SerializeField] private AudioClip Si;
    [SerializeField] private AudioClip Doo;
    [SerializeField] private AudioClip DoMiSolChords;
    [SerializeField] private AudioClip ReFaLaChords;
    [SerializeField] private AudioClip MiSolSiChords;
    [SerializeField] private AudioClip FaLaDoChords;
    [SerializeField] private AudioClip SolSiReChords;
    [SerializeField] private AudioClip LaDoMiChords;
    [SerializeField] private AudioClip SiReFaChords;
    [SerializeField] private AudioClip MiSiReChords;
    [SerializeField] private AudioClip FailureJingle;
    private Dictionary<string, AudioClip> clips;
    private AudioSource audioSource;

    /// <summary>
    /// Creates an instance of AudioManager if it doesn't exist.
    /// If it does, keeps it.
    /// Store the default AudioClips.
    /// </summary>
	protected override void Awake()
    {
        base.Awake();

        clips = new Dictionary<string, AudioClip>
        {
            { "A", Do },
            { "Z", Re },
            { "E", Mi },
            { "R", Fa },
            { "Q", Sol },
            { "S", La },
            { "D", Si },
            { "F", Doo },
            { "DoMiSolChords", DoMiSolChords },
            { "ReFaLaChords", ReFaLaChords },
            { "MiSolSiChords", MiSolSiChords },
            { "FaLaDoChords", FaLaDoChords },
            { "SolSiReChords", SolSiReChords },
            { "LaDoMiChords", LaDoMiChords },
            { "SiReFaChords", SiReFaChords },
            { "MiSiReChords", MiSiReChords },
            { "FailureJingle", FailureJingle },
        };

        audioSource = gameObject.GetComponent<AudioSource>();
        /*audioSource.volume = PlayerPrefs.HasKey(Constants.PlayerPrefs.ClipsVolume) ?
                                PlayerPrefs.GetFloat(Constants.PlayerPrefs.ClipsVolume) : 1f;*/
    }

    /// <summary>
    /// Plays the sound with the specified name.
    /// If it is not found in the Dictionary, it will try to load a AudioClip with the same name.
    /// </summary>
    /// <param name="name">The name of the sound you want to play.</param>
	public void PlaySound(string name)
    {
        if (clips.ContainsKey(name) && audioSource != null)
            audioSource.PlayOneShot(clips[name]);
        else
        {
            AudioClip tmp = Resources.Load<AudioClip>(name);
            if (tmp != null)
            {
                clips.Add(name, tmp);
                audioSource.PlayOneShot(clips[name]);
            }
        }
    }

    public void PlaySound(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }

    public void SetVolume(float volume)
    {
        //PlayerPrefs.SetFloat(Constants.PlayerPrefs.ClipsVolume, volume);
        audioSource.volume = volume;
    }
}
