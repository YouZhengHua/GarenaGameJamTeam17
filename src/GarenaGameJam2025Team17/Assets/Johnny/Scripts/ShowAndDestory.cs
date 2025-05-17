using System.Collections;
using UnityEngine;

public class ShowAndDestory : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(StartShow());
    }
    IEnumerator StartShow()
    {
        yield return new WaitForSeconds(GameSystem.JudgeShowTime);
        gameObject.SetActive(false);
    }
}
