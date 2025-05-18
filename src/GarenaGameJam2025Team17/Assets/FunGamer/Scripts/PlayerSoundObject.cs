using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSoundData", menuName = "Player/SoundData")]
public class PlayerSoundObject : ScriptableObject
{
    public SoundData[] soundDatas;
    
    public bool TryGetSoundData(string soundName, out SoundData soundData)
    {
        soundData = soundDatas.FirstOrDefault(x => x.soundName == soundName);
        return soundData != null;
    }
}

[System.Serializable]
public class SoundData
{
    public string soundName;
    public float volume = 1f;
    public AudioClip audioClip;
}