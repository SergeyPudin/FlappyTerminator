using UnityEngine;
using UnityEngine.Events;

public class PlayerDeathHandler : MonoBehaviour
{
    public event UnityAction PlayerDied;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerDestroyer>(out PlayerDestroyer destroyer))
            PlayerDied?.Invoke();
    }
}