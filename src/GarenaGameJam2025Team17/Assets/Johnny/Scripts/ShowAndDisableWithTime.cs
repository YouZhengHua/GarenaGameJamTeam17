using UnityEngine;
using System.Collections;

public class ShowAndDisableWithTime : MonoBehaviour
{
    [SerializeField] float ShowTime = 0.1f;
    private void OnEnable()
    {
        StartCoroutine(StartShow());
    }
    IEnumerator StartShow()
    {
        yield return new WaitForSeconds(ShowTime);
        gameObject.SetActive(false);
    }
}
