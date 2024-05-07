using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float _timeBetweenSpawn;
    [SerializeField] private float _topLimit;
    [SerializeField] private float _bottomLimit;
    [SerializeField] private float _offsetX;

    [SerializeField] private Player _player;
    [SerializeField] private ObjectPool _pool;
    [SerializeField] private Transform _parent;
    [SerializeField] private PlayerDeathHandler _playerDeathHandler;
    
    private Coroutine _spawnCoroutine;

    private void Start()
    {
        _spawnCoroutine = StartCoroutine(Spawn());
    }

    private void OnEnable()
    {
        _playerDeathHandler.PlayerDied += TurnOffEnemies;
    }

    private void OnDisable()
    {
        StopCoroutine(Spawn());
        _playerDeathHandler.PlayerDied -= TurnOffEnemies;
    }

    private IEnumerator Spawn()
    {
        WaitForSeconds waitForSeconds = new(_timeBetweenSpawn);

        while (true)
        {
            if (_pool.RetrieveObject() != null)
            {
                Poolable currentEnemy = _pool.RetrieveObject();

                currentEnemy.gameObject.SetActive(true);
                currentEnemy.transform.position = SpawnPoint();
            }

            yield return waitForSeconds;
        }
    }

    private Vector3 SpawnPoint()
    {
        float randomY = Random.Range(_bottomLimit, _topLimit);
        float positionX = _player.transform.position.x + _offsetX; 

        Vector3 currentPoint = new Vector3(positionX, randomY, 0);
        
        return currentPoint;
    }

    private void TurnOffEnemies()
    {
        foreach (Transform childTransform in _parent)
        {
            if (childTransform.gameObject.activeSelf == true)
                childTransform.gameObject.SetActive(false);
        }
    }
}