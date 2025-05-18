using UnityEngine;
[RequireComponent(typeof(CanvasGroup))]
public class CanvasController : MonoBehaviour
{
    protected CanvasGroup _canvasGroup;

    [SerializeField] protected bool isHide = false;
    [SerializeField] protected float hideSpeed = 10f;

    protected virtual void Awake()
    {
        _canvasGroup = this.GetComponent<CanvasGroup>();
    }

    protected virtual void Start()
    {
        _canvasGroup.alpha = _targetAlpha;
    }

    protected float _targetAlpha => isHide ? 0f : 1f;

    protected virtual void Update()
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
    
    public virtual void ShowCanvas()
    {
        isHide = false;
    }

    public virtual void HideCanvas()
    {
        isHide = true;
    }
}
