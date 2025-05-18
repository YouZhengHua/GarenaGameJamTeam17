using UnityEngine;
using UnityEngine.Events;

public class BeatMoveSystem : MonoBehaviour
{
    [SerializeField] int beatIndex = 0;
    [SerializeField] AudioSource BeatAudioClip;

    private Vector3 _moveDirc = Vector3.zero;
    private float _attackAmount = 0f;
    private bool _canMove = false;
    public int GetBeatIndex() { return beatIndex; }
    public float GetHitPoint() { return _attackAmount; }
    public void StartMoveBeat(int moveWay, float attack)
    {
        _moveDirc = new Vector3(moveWay, 0f, 0f);
        _attackAmount = attack;
        _canMove = true;
    }
    public void SuccessJudge()
    {
        Destroy(gameObject);
    }
    public void HitPlayer()
    {
        Destroy(gameObject);
    }
    private void Update()
    {
        if (!_canMove) return;
        transform.position += _moveDirc * GameSystem.BeatSpeed * Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CenterPlay"))
        {
            if (BeatAudioClip != null) BeatAudioClip.Play();
        }
    }

}
