using UnityEngine;
using System.Collections;

public class UITransitionController : MonoBehaviour
{
    [SerializeField] private CanvasGroup mainCanvasGroup;
    [SerializeField] private float setTime = 1f;
    [SerializeField] private SceneLoader sceneLoader; // ���Mԭ���� SceneLoader

    public void StartFunction()
    {
        StartCoroutine(StartTime());
    }

    private IEnumerator StartTime()
    {
        mainCanvasGroup.gameObject.SetActive(true);

        float t = 0f;
        while (t < setTime)
        {
            t += Time.deltaTime;
            mainCanvasGroup.alpha = Mathf.Clamp01(t / setTime);
            yield return null;
        }

        sceneLoader.LoadScene(); // �� ��Ǭ�Q�� SceneLoader �����d��
    }
}
