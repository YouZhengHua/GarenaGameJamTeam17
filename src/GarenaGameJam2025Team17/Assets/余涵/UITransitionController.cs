using UnityEngine;
using System.Collections;

public class UITransitionController : MonoBehaviour
{
    [SerializeField] private CanvasGroup ��Ļ;
    [SerializeField] private float �����r�g = 1f;
    [SerializeField] private SceneLoader sceneLoader; // ���Mԭ���� SceneLoader

    public void �����D���Ј���()
    {
        StartCoroutine(�������Ј���());
    }

    private IEnumerator �������Ј���()
    {
        ��Ļ.gameObject.SetActive(true);

        float t = 0f;
        while (t < �����r�g)
        {
            t += Time.deltaTime;
            ��Ļ.alpha = Mathf.Clamp01(t / �����r�g);
            yield return null;
        }

        sceneLoader.LoadScene(); // �� ��Ǭ�Q�� SceneLoader �����d��
    }
}
