using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Counter : MonoBehaviour
{
    [SerializeField] private PlayerDeathHandler _dier;
    [SerializeField] private TimeHandler _timeHandler;

    private int _points;
    private Ikillable _killable;
    private Coroutine _resetCoroutine;

    public event UnityAction PointsChanged;

    public int Points => _points;

    private void OnEnable()
    {
        _dier.PlayerDied += ResetCount;
    }

    private void OnDisable()
    {
        _dier.PlayerDied -= ResetCount;

        if (_killable != null)
            _killable.Killed -= IncreasePoints;
    }

    private void Start()
    {
        _points = 0;
        PointsChanged?.Invoke();
    }

    public void Subscribe(Ikillable fireball)
    {
        _killable = fireball;
        _killable.Killed += IncreasePoints;
    }

    private void IncreasePoints()
    {
        _points += 1;
        PointsChanged?.Invoke();
    }

    private void ResetCount()
    {
        _resetCoroutine = StartCoroutine(ShowRecord());
    }

    private IEnumerator ShowRecord()
    {
        WaitForSecondsRealtime waitForSeconds = new(_timeHandler.PauseTime);

        yield return waitForSeconds;

        _points = 0;
        PointsChanged?.Invoke();

        _resetCoroutine = null;
    }
}