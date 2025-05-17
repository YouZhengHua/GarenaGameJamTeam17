using UnityEngine;
[RequireComponent(typeof(CanvasGroup))]
public class CanvasController : MonoBehaviour
{
    private CanvasGroup _canvasGroup;

    [SerializeField] private bool isHide = false;
    [SerializeField] private float hideSpeed = 10f;

    private void Awake()
    {
        _canvasGroup = this.GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        _canvasGroup.alpha = _targetAlpha;
    }

    private float _targetAlpha => isHide ? 0f : 1f;

    private void Update()
    {;
        if (_canvasGroup.alpha != _targetAlpha)
        {
            _canvasGroup.alpha = Mathf.Lerp(_canvasGroup.alpha, _targetAlpha, Time.deltaTime * hideSpeed);
            if (Mathf.Approximately(_canvasGroup.alpha, _targetAlpha))
            {
                _canvasGroup.alpha = _targetAlpha;
            }
        }
    }
    
    public void ShowCanvas()
    {
        isHide = true;
    }

    public void HideCanvas()
    {
        isHide = false;
    }
}
