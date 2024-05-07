using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class TimeHandler : MonoBehaviour
{
    [SerializeField] private PlayerDeathHandler _diyer;
    [SerializeField] private float _pauseDuration;

    private Coroutine _timeCoroutine;

    public float PauseTime => _pauseDuration;

    public event UnityAction TimeResumed;

    private void OnEnable()
    {
        _diyer.PlayerDied += StopTime;
    }

    private void OnDisable()
    {
        _diyer.PlayerDied -= StopTime;
    }

    private void StopTime()
    {
        if (_timeCoroutine == null)
        {
            _timeCoroutine = StartCoroutine(MakePause());
        }
    }

    private IEnumerator MakePause()
    {
        float off = 0f;
        float on = 1f;

        WaitForSecondsRealtime wait = new WaitForSecondsRealtime(_pauseDuration);

        Time.timeScale = off;

        yield return wait;

        Time.timeScale = on;
        TimeResumed?.Invoke();

        _timeCoroutine = null;
    }
}
