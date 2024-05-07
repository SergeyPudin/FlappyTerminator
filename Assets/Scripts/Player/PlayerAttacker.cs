using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerAttacker : MonoBehaviour
{
    [SerializeField] private Transform _fireballPoint;
    [SerializeField] private float _loadTime;
    [SerializeField] private ObjectPool _pool;

    private bool _isLoaded;

    private Coroutine _loadCoroutine;

    private void Start()
    {
        _loadCoroutine = StartCoroutine(Load());
    }

    private void OnDisable()
    {
        StopCoroutine(_loadCoroutine);
    }

    public void Attack()
    {
        if (_isLoaded)
        {
            Poolable fireball = _pool.RetrieveObject();

            if (fireball != null) 
            {
                fireball.transform.position = _fireballPoint.position; 
                fireball.gameObject.SetActive(true);
            }

            _isLoaded = false;
        }
    }

    private IEnumerator Load()
    {
        WaitForSeconds waitForSeconds = new(_loadTime);

        while (true)
        {
            _isLoaded = true;

            yield return waitForSeconds;
        }
    }
}