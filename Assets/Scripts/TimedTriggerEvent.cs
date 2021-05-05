using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimedTriggerEvent : MonoBehaviour
{
    public UnityEvent OnEnter;
    public UnityEvent OnExit;
    [SerializeField]
    private bool requireTag;
    [SerializeField]
    private string targetTag;
    [SerializeField]
    public float time;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (requireTag && !collision.CompareTag(targetTag)) return;
        OnEnter.Invoke();
        StartCoroutine(timer());
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (requireTag && !collision.CompareTag(targetTag)) return;
        StopAllCoroutines();
        OnExit.Invoke();
    }
    private IEnumerator timer()
    {
        yield return new WaitForSeconds(time);
        OnExit.Invoke();
    }
}
