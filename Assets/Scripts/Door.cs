using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Door : MonoBehaviour
{
    private Animator _animator;

    public bool _isOpen;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _isOpen = false;
    }

    private void Update()
    {
        CheckDoorOpen();
    }

    private void CheckDoorOpen()
    {
        if (_isOpen == true)
        {
            _animator.SetBool(DoorAnimatorController.Params.IsOpen, true);
        }
    }
}

public static class DoorAnimatorController
{
    public static class Params
    {
        public const string IsOpen = nameof(IsOpen);
    }
}
