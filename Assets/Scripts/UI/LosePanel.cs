using UnityEngine;

public class LosePanel : MonoBehaviour
{
    [SerializeField] private PlayerDeathHandler _dier;
    [SerializeField] private TimeHandler _timeHandler;
    [SerializeField] private Panel _panel;

    private void Awake()
    {
        _timeHandler.TimeResumed += TurnedOffPanel;
        _dier.PlayerDied += TurnOnPanel;
    }

    private void OnApplicationQuit()
    {
        _timeHandler.TimeResumed -= TurnedOffPanel;
        _dier.PlayerDied -= TurnOnPanel;
    }

    private void TurnOnPanel()
    {
        _panel.gameObject.SetActive(true);
    }

    private void TurnedOffPanel()
    {
        _panel.gameObject.SetActive(false);
    }
}