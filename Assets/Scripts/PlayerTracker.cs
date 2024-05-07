using UnityEngine;

public class PlayerTracker : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private float _offsetX;

    private void Update()
    {
        Vector3 targetPosition = transform.position;
        targetPosition.x = _player.transform.position.x + _offsetX;

        transform.position = targetPosition;
    }
}