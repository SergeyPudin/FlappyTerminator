using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(PlayerDeathHandler))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _tapSpeed;
    [SerializeField] private float _rotaionSpeed;
    [SerializeField] private float _topLimitY;
    [SerializeField] private float _downwardThrowing = 1.0f;

    [SerializeField] private float _maxRotationX = -30;
    [SerializeField] private float _minRotationX = 20;

    [SerializeField] private TimeHandler _timeHandler;

    private Quaternion _maxRotation;
    private Quaternion _minRotation;

    private Rigidbody _rigidbody;
    private Vector3 _defaltPosition;
    private PlayerDeathHandler _playerDeathHandler;
    private Coroutine _freezePositionCoroutine;

    private void Awake()
    {
        float rotationY = transform.rotation.eulerAngles.y;

        _rigidbody = GetComponent<Rigidbody>();
        _playerDeathHandler = GetComponent<PlayerDeathHandler>();

        _maxRotation = Quaternion.Euler(_maxRotationX, rotationY, 0);
        _minRotation = Quaternion.Euler(_minRotationX, rotationY, 0);

        _defaltPosition = transform.position;
    }

    private void OnEnable()
    {
        _playerDeathHandler.PlayerDied += ResetPosition;
    }

    private void OnDisable()
    {
        _playerDeathHandler.PlayerDied -= ResetPosition;
    }

    private void Update()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, _minRotation, _rotaionSpeed * Time.deltaTime);

        ClampTopLimit();
    }

    public void Fly()
    {
        Vector3 targetPosition = transform.position;

        _rigidbody.velocity = new Vector3(_moveSpeed, _tapSpeed, _rigidbody.transform.position.z);
        transform.rotation = _maxRotation;
    }

    private void ClampTopLimit()
    {
        if (transform.position.y >= _topLimitY)
        {
            Vector3 target = transform.position;
            target.y = _topLimitY - _downwardThrowing;

            transform.position = target;
        }
    }

    private void ResetPosition()
    {
        Vector3 curentVelocity = _rigidbody.velocity;
        curentVelocity.y = 0;

        _freezePositionCoroutine = StartCoroutine(FreezePosition());
        
        _rigidbody.velocity = curentVelocity;
    }

    private IEnumerator FreezePosition()
    {
        WaitForSecondsRealtime waitForSeconds = new(_timeHandler.PauseTime);

        yield return waitForSeconds;

        transform.position = _defaltPosition;

        _freezePositionCoroutine = null;
    }
}