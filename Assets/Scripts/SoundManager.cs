using UnityEngine;

public class SoundManager : MonoSingleton<SoundManager>
{
    [SerializeField]
    private AudioClip[] sounds;

    public AudioClip[] Sounds
    {
        get { return sounds; }
        private set { sounds = value; }
    }

    public AudioClip GetAudioClipByName(string audioClipName)
    {
        foreach (AudioClip sound in Sounds)
        {
            if (sound.name == audioClipName) return sound;
        }

        return null;
    }
}
