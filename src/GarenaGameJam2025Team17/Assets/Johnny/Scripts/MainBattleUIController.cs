using UnityEngine;
using UnityEngine.UIElements;

public class MainBattleUIController : MonoBehaviour
{
    [SerializeField] GameObject BeatValueContactEffectOBJ;
    VisualElement rootVisualElement;
    VisualElement player1BeatValue;

    public void UpdateBeatUI()
    {
        float percentValue = GameSystem.BeatValue + 50f;

        player1BeatValue.style.width = new Length(percentValue, LengthUnit.Percent);

    }

    private void Start()
    {
        rootVisualElement = GetComponent<UIDocument>().rootVisualElement;
        player1BeatValue = rootVisualElement.Q<VisualElement>("Player1BeatValue");
        UpdateBeatUI();
    }

}
