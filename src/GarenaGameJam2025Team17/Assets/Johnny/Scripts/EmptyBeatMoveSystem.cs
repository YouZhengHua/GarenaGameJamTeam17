using System.Collections;
using UnityEngine;

public class EmptyBeatMoveSystem : MonoBehaviour
{
    [SerializeField] float destoryTime = 20f;

    private Vector3 _moveDirc = Vector3.zero;
    private bool _canMove = false;
    public void StartMoveBeat(int moveWay)
    {
        _moveDirc = new Vector3(moveWay, 0f, 0f);
        _canMove = true;
    }
    IEnumerator MoveStart()
    {
        yield return new WaitForSeconds(destoryTime);
        Destroy(gameObject);
    }
    private void Update()
    {
        if (!_canMove) return;
        transform.position += _moveDirc * GameSystem.BeatSpeed * Time.deltaTime;
    }
}
