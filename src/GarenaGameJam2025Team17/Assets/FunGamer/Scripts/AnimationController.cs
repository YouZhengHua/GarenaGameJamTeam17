using UnityEngine;
using UnityEngine.InputSystem.Users;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif
public class AnimationController : MonoBehaviour
{
    private Animator _animator;
    private PlayerInput _playerInput;
    [SerializeField] private PlayerSoundObject playerSoundObject;
    private void Awake()
    {
        _animator = this.GetComponentInChildren<Animator>();
        _playerInput = this.GetComponent<PlayerInput>();
    }

    private void Start()
    {
        InputUser.PerformPairingWithDevice(Keyboard.current, _playerInput.user);
    }

#if ENABLE_INPUT_SYSTEM
    public void OnUp(InputValue input)
    {
        _animator.SetTrigger("Up");
    }
    
    public void OnDown(InputValue input)
    {
        _animator.SetTrigger("Down");
    }
    
    public void OnLeft(InputValue input)
    {
        _animator.SetTrigger("Left");
    }
    
    public void OnRight(InputValue input)
    {
        _animator.SetTrigger("Right");
    }
    #endif

    private void PlaySound(AnimationEvent animationEvent)
    {
        Debug.Log(animationEvent.stringParameter);
        if(playerSoundObject != null && playerSoundObject.TryGetSoundData(animationEvent.stringParameter, out var soundData))
        {
            var audioSources = GameObject.FindObjectsByType<AudioSource>(FindObjectsSortMode.None)[0];
            audioSources.PlayOneShot(soundData.audioClip, soundData.volume);
        }
    }

    public void PlayHurtAnimation()
    {
        _animator.SetTrigger("Hurt");
    }
    
    public void PlayDefend()
    {
        _animator.SetTrigger("Defend");
    }

    public void PlayWin()
    {
        _animator.SetBool("IsWin", true);
        _animator.SetTrigger("WinTrigger");
    }
    
    public void PlayLose()
    {
        _animator.SetBool("IsDie", true);
        _animator.SetTrigger("DieTrigger");
    }
}
