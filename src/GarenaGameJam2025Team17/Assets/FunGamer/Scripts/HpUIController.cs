using UnityEngine;
using UnityEngine.UI;

public class HpUIController : MonoBehaviour
{
    [SerializeField] private Image _hpImage;

    public void UpdateUI(float rate)
    {
        _hpImage.fillAmount = rate;
    }
}
