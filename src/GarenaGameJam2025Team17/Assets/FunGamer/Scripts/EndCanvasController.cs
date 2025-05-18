using UnityEngine;
[RequireComponent(typeof(CanvasGroup))]
public class EndCanvasController : CanvasController
{
    [SerializeField] private AudioSource _endBgm;
    public override void ShowCanvas()
    {
        _endBgm.Play();
        base.ShowCanvas();
    }
}
