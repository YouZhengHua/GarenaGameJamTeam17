using UnityEngine;
using UnityEngine.Rendering;

public class EffectController : MonoBehaviour
{
    private Volume _volume;
    private bool _haveEffect = false;
    [SerializeField] private AudioController _audioController;

    private void Awake()
    {
        _volume = GetComponent<Volume>();
        if (GameObject.Find("AudioController") != null)
        {
            _audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
            _audioController.OnTrigger += OnTrigger;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (_haveEffect)
        {
            _volume.weight = Mathf.Lerp(_volume.weight, 0f, Time.deltaTime * 120f / 10f );
        }

        if (Mathf.Approximately(_volume.weight, 0f))
        {
            _volume.weight = 0f;
            _haveEffect = false;
        }
    }

    private void OnTrigger()
    {
        _haveEffect = true;
        _volume.weight = 1f;
    }
}
