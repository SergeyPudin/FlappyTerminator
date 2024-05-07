using TMPro;
using UnityEngine;

[RequireComponent(typeof(Counter))]
public class CounterViewer : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    private Counter _counter;

    private void OnEnable()
    {
        _counter.PointsChanged += ViewChangedPoints;
    }

    private void OnDisable()
    {
        _counter.PointsChanged -= ViewChangedPoints;
    }

    private void Awake()
    {
        _counter = GetComponent<Counter>();
    }

    private void ViewChangedPoints()
    {
        _text.text = _counter.Points.ToString();
    }
}