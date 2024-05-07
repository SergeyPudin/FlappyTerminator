using System.Collections;
using UnityEngine;

public class EnemyAttacker : MonoBehaviour
{
    [SerializeField] private ObjectPool _objectPool;
    [SerializeField] private float _minAttackInterval;
    [SerializeField] private float _maxAttackInterval;
    [SerializeField] private Transform _fireballPoint;

    private Coroutine _attackCoroutine;

    private void OnEnable()
    {
        _attackCoroutine = StartCoroutine(Attack());
    }

    private void OnDisable()
    {
        StopCoroutine(_attackCoroutine);
    }

    private IEnumerator Attack()
    {
        while (true)
        {
            float random = Random.Range(_minAttackInterval, _maxAttackInterval);

            yield return new WaitForSeconds(random);

            Poolable fireball = _objectPool.RetrieveObject();

            if (fireball != null)
            {
                fireball.gameObject.SetActive(true);
                fireball.transform.position = _fireballPoint.position;
                fireball.transform.parent = transform.parent;
            }
        }
    }
}