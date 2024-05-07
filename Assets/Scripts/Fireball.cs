using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Fireball : MonoBehaviour, Ikillable
{
    [SerializeField] private float _speed;
    [SerializeField] private float _lifeTime;

    private Coroutine _dieCoroutine;
    private Counter _counter;

    public event UnityAction Killed;

    private void Start()
    {
        Counter counter = FindObjectOfType<Counter>();
        counter.Subscribe(this);        
    }

    private void OnEnable()
    {
        _dieCoroutine = StartCoroutine(Die());        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Enemy>(out Enemy enemy))
        {
            Killed?.Invoke();
            enemy.Die();
        }
    }

    private void Update()
    {
        transform.Translate(Vector3.right * _speed * Time.deltaTime);
    }
       
    private IEnumerator Die()
    {
        WaitForSeconds waitForSeconds = new (_lifeTime);

        yield return waitForSeconds;

        gameObject.SetActive(false);

        _dieCoroutine = null;
    }
}