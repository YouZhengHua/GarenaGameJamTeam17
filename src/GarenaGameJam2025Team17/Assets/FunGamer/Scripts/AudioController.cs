using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class AudioController : MonoBehaviour
{
    public Action OnTrigger;

    public Action OnAudioStart;
    public Action OnAudioEnd;
    
    public AudioSource audioSource;
    public int sampleDataLength = 1024;
    private float[] clipSampleData;

    private bool isTrigger = false;
    [SerializeField] private float border = 0.5f;

    private float totalTime = 0f;
    private float singleTime = 0f;

    private bool isPlay = false;
    private float _dealyTime { get => 60f / _BPM; }
    [FormerlySerializedAs("BPM")] [SerializeField] private int _BPM = 120;
    public int BPM { get => _BPM; }
    
    private void Start() {
        clipSampleData = new float[sampleDataLength];
        OnAudioStart += AudioStartEvent;
    }

    private void AudioStartEvent()
    {
        if (audioSource.isPlaying)
            return;
        
        audioSource.Play();
        totalTime = 0f;
        if (isCheckVolume)
        {
            isTrigger = false;
            singleTime = 0f;
        }
        else
        {
            isPlay = true;
            WhileLoop();
        }
    }

    private void AudioEndEvent()
    {
        isPlay = false;
        audioSource.Stop();
        isCheckVolume = false;
    }

    private bool isCheckVolume = false;

    private void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Z) && !audioSource.isPlaying)
        {
            audioSource.Play();
            totalTime = 0f;
            singleTime = 0f;
            isTrigger = false;
            isCheckVolume = true;
        }
        else if (Input.GetKeyDown(KeyCode.X) && !audioSource.isPlaying)
        {
            audioSource.Play();
            totalTime = 0f;
            isPlay = true;
            WhileLoop();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && audioSource.isPlaying)
        {
            isPlay = false;
            audioSource.Stop();
            isCheckVolume = false;
        }

        
        if (!audioSource.isPlaying )
            return;
        if(isCheckVolume)
            CheckVolume();
    }

    private float GetLoudness
    {
        get
        {
            audioSource.clip.GetData(clipSampleData, audioSource.timeSamples);
            float loudness = 0f;
            foreach (var sample in clipSampleData) {
                loudness += Mathf.Abs(sample);
            }
        
            return loudness / sampleDataLength; // 計算平均振幅
        }
    }

    private void CheckVolume()
    {
        float loudness = GetLoudness;
        bool result = loudness >= border;
        totalTime += Time.deltaTime;
        if (result == isTrigger)
        {
            singleTime += Time.deltaTime;
            return;
        }
        
        isTrigger = result;
        if (isTrigger)
        {
            Debug.Log($"隨音量觸發，音量強度: {loudness}, 拍子間隔: {singleTime}, 總時間: {totalTime}" );
            singleTime = 0f;
            OnTrigger?.Invoke();
        }
    }

    private void WhileLoop()
    {
        StartCoroutine(DelayTrigger());
    }

    private IEnumerator DelayTrigger()
    {
        while (audioSource.isPlaying && isPlay)
        {
            totalTime += _dealyTime;
            Debug.Log($"隨延遲觸發，音量強度: {GetLoudness}, 拍子間隔: {_dealyTime}, 總時間: {totalTime}" );
            OnTrigger?.Invoke();
            yield return new WaitForSecondsRealtime(_dealyTime);
        }
    }
}
