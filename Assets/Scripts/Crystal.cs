using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Crystal : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void IsUp()
    {
        StartCoroutine(Destroy());
    }

    private IEnumerator Destroy()
    {
        _animator.SetBool(CrystalAnimator.Params.IsUp, true);
        yield return new WaitForSeconds(1.6f);
        gameObject.SetActive(false);
    }
}
public static class CrystalAnimator
{
    public static class Params
    {
        public const string IsUp = nameof(IsUp);
    }
}
