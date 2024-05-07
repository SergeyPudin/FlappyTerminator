using UnityEngine;

public class Enemy : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
    }

    public void Die()
    {
        gameObject.SetActive(false);
    }
}