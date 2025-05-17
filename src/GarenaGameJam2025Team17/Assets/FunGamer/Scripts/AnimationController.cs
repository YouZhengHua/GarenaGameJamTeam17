using System;
using UnityEngine;
using UnityEngine.InputSystem.Users;
using UnityEngine.Serialization;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif
public class AnimationController : MonoBehaviour
{
    private Animator _animator;
    public bool up = false;
    public bool down = false;
    public bool left = false;
    public bool right = false;
    [SerializeField] private float delayTime = 0f;
    private PlayerInput _playerInput;
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
        up = input.isPressed;
        _animator.SetBool("Up", up);
        Invoke("AutoFalse", delayTime);
    }
    
    public void OnDown(InputValue input)
    {
        down = input.isPressed;
        _animator.SetBool("Down", down);
        Invoke("AutoFalse", delayTime);
    }
    
    public void OnLeft(InputValue input)
    {
        left = input.isPressed;
        _animator.SetBool("Left", left);
        Invoke("AutoFalse", delayTime);
    }
    
    public void OnRight(InputValue input)
    {
        right = input.isPressed;
        _animator.SetBool("Right", right);
        Invoke("AutoFalse", delayTime);
    }
    #endif

    private void OnFootstep(AnimationEvent animationEvent)
    {
        
    }
    
    private void OnLand(AnimationEvent animationEvent)
    {
        
    }

    private void AutoFalse()
    {
        up = false;
        down = false;
        left = false;
        right = false;        
        _animator.SetBool("Up", up);
        _animator.SetBool("Down", down);
        _animator.SetBool("Left", left);
        _animator.SetBool("Right", right);
    }
}
