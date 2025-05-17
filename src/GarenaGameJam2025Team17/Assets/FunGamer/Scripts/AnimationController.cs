using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.Users;
using UnityEngine.Serialization;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif
public class AnimationController : MonoBehaviour
{
    private Animator _animator;
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
    }
}
