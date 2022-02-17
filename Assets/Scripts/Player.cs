using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private ParticleSystem _particle;
    [SerializeField] private LayerMask _distructible;

    private float _radiusDestroy = 1f;
    private Vector3 _moveVector;
    private Animator _animator;
    private CharacterController _controller;
    private MobileMovement _mMovement;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _mMovement = GameObject.FindGameObjectWithTag(StringConst.Params.Joystick).GetComponent<MobileMovement>();
    }

    private void Update()
    {
        Movement();
        CheckDistructibles();
        CheckCrystalUp();
    }

    private void Movement()
    {
        _moveVector = Vector3.zero;
        _moveVector.x = _mMovement.Horizontal() * _moveSpeed;
        _moveVector.z = _mMovement.Vertical() * _moveSpeed;

        if (Vector3.Angle(Vector3.forward, _moveVector) > 1f || Vector3.Angle(Vector3.forward, _moveVector) == 0)
        {
            Vector3 direction = Vector3.RotateTowards(transform.forward, _moveVector, _moveSpeed, 0.0f);
            transform.rotation = Quaternion.LookRotation(direction);
        }

        if (_moveVector.x != 0 || _moveVector.z != 0)
            _animator.SetBool(StringConst.Params.IsRunning, true);
        else
        {
            _animator.SetBool(StringConst.Params.IsRunning, false);
            _animator.SetBool(StringConst.Params.IsDigging, false);
        }

        _controller.Move(_moveVector * Time.deltaTime);
    }

    private void CheckDistructibles()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _radiusDestroy, _distructible);

        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent<Sand>(out Sand sand))
            {
                sand.Destroyed();
                _animator.SetBool(StringConst.Params.IsDigging, true);
            }
        }
    }

    private void CheckCrystalUp()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, _radiusDestroy, _distructible);

        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent<Crystal>(out Crystal crystal))
            {
                _radiusDestroy = 5f;
                _particle.Play();
                crystal.IsUp();
            }
        }
    }
}

public static class StringConst
{
    public static class Params
    {
        public const string IsDigging = nameof(IsDigging);
        public const string IsRunning = nameof(IsRunning);
        public const string Horizontal = nameof(Horizontal);
        public const string Vertical = nameof(Vertical);
        public const string Joystick = nameof(Joystick);
    }
}