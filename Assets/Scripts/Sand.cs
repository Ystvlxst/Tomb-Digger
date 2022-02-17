using System.Collections;
using UnityEngine;

public class Sand : MonoBehaviour
{
    [SerializeField] ParticleSystem _particle;

    public void Destroyed()
    {
        StartCoroutine(Destroy());
    }

    private IEnumerator Destroy()
    {
        _particle.Play();
        yield return new WaitForSeconds(0.05f);
        gameObject.SetActive(false);
    }
}
