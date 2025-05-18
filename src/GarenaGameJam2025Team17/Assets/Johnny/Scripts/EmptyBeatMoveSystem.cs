using System.Collections;
using UnityEngine;

public class EmptyBeatMoveSystem : MonoBehaviour
{
    [SerializeField] float destoryTime = 60f;

    private Vector3 _moveDirc = Vector3.zero;
    private bool _canMove = false;
    private int _playerIndex;

    public int GetPlayerIndex () { return _playerIndex; }
    public void StartMoveBeat(int moveWay,int playerIndex)
    {
        _playerIndex = playerIndex;
        _moveDirc = new Vector3(moveWay, 0f, 0f);
        _canMove = true;
        StartCoroutine(MoveStart());
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
