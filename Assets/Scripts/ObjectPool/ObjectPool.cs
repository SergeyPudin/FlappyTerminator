using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private Poolable _prefab;
    [SerializeField] private int _objectQuantity;
    [SerializeField] private Transform _parentObject;

    private List<Poolable> _poolableObjects = new();

    private void Start()
    {
        Quaternion initializeRotation = _prefab.transform.rotation;

        InitializePool(initializeRotation);
    }

    private void InitializePool(Quaternion rotation)
    {
        for (int i = 0; i < _objectQuantity; i++)
        {
            Poolable newObject = Instantiate(_prefab, transform.position, rotation, _parentObject);
            newObject.gameObject.SetActive(false);
            _poolableObjects.Add(newObject);
        }
    }

    public Poolable RetrieveObject()
    {
        foreach (Poolable obj in _poolableObjects)
        {
            if (obj.gameObject.activeSelf == false)
                return obj;
        }

        return null;
    }    
}