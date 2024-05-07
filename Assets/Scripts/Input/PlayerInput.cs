using UnityEngine;

[RequireComponent (typeof(PlayerMover), typeof(PlayerAttacker))]
public class PlayerInput : MonoBehaviour
{
    private PlayerMover _mover;
    private PlayerAttacker _attacker;

    private void Start()
    {
        _mover = GetComponent<PlayerMover>();
        _attacker = GetComponent<PlayerAttacker>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            _mover.Fly();

        if (Input.GetKeyDown(KeyCode.E))
            _attacker.Attack();
    }
}