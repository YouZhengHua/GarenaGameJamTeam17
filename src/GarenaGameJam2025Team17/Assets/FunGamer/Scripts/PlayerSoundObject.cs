using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSoundData", menuName = "Player/SoundData")]
public class PlayerSoundObject : ScriptableObject
{
    public SoundData[] soundDatas;
}

public class SoundData : ScriptableObject
{
    public string soundName;
    public float volume = 1f;
    public AudioClip audioClip;
}