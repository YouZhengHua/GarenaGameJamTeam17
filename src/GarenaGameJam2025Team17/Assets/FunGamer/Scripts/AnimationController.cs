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
    private InputControl _inputControl;
    private GameSystemSetting _gameSystemSetting;
    private void Awake()
    {
        _animator = this.GetComponentInChildren<Animator>();
        _playerInput = this.GetComponent<PlayerInput>();
        _inputControl = this.GetComponent<InputControl>();
        _gameSystemSetting = GameObject.Find("GameSystemSetting").GetComponent<GameSystemSetting>();
    }

    private void Start()
    {
        InputUser.PerformPairingWithDevice(Keyboard.current, _playerInput.user);
        _gameSystemSetting.OnPlayerWin.AddListener(this.PlayResult);
    }

#if ENABLE_INPUT_SYSTEM
    public void OnUp(InputValue input)
    {
        if(_inputControl.IsAttackTurn)
            _animator.SetTrigger("Up");
        else
            this.PlayDefend();
    }
    
    public void OnDown(InputValue input)
    {
        if(_inputControl.IsAttackTurn)
            _animator.SetTrigger("Down");
        else
            this.PlayDefend();
    }
    
    public void OnLeft(InputValue input)
    {
        if(_inputControl.IsAttackTurn)
            _animator.SetTrigger("Left");
        else
            this.PlayDefend();
    }
    
    public void OnRight(InputValue input)
    {
        if(_inputControl.IsAttackTurn)
            _animator.SetTrigger("Right");
        else
            this.PlayDefend();
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

    private void PlayWin()
    {
        _animator.SetBool("IsWin", true);
        _animator.SetTrigger("WinTrigger");
    }
    
    private void PlayLose()
    {
        _animator.SetBool("IsDie", true);
        _animator.SetTrigger("DieTrigger");
    }

    public void PlayResult(int winPlayerIndex)
    {
        if (_inputControl.PlayerIndex == winPlayerIndex)
        {
            this.PlayWin();
        }
        else
        {
            this.PlayLose();
        }
    }
}
