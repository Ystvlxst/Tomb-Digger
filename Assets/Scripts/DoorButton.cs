using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class DoorButton : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Door _door;

    private Animator _animator;
    private const string Player = "Player";

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _player = GameObject.FindGameObjectWithTag(Player).GetComponent<Player>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {
            _door._isOpen = true;
            _animator.SetBool(DoorButtonAnimatorController.Params.IsClick, true);
        }
    }
}

public static class DoorButtonAnimatorController
{
    public static class Params
    {
       public const string IsClick = nameof(IsClick);
    }
}
