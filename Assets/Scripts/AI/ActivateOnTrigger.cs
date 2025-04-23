using UnityEngine;

public class ActivateOnTrigger : MonoBehaviour
{
    [SerializeField] EnemyStateManager[] enemyStateManagers;
    void Start()
    {
        foreach (EnemyStateManager manager in enemyStateManagers)
        {
            manager.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"{other.name} �����");
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("����� �����");
            foreach (EnemyStateManager manager in enemyStateManagers)
            {
                Debug.Log($"{manager.name} �������������");
                manager.enabled = true;
            }
        }
    }
}
