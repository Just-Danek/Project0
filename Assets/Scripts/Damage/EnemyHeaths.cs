using UnityEngine;

public class EnemyHeaths : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;

    private EnemyStateManager stateManager;

    private void Start()
    {
        currentHealth = maxHealth;
        stateManager = GetComponent<EnemyStateManager>();
    }
    public void TakeDamage(float damage)
    {
        Debug.Log($"[EnemyHealth] ���� ������� ����: {damage}, �������� ��������: {currentHealth}");

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Debug.Log("[EnemyHealth] �������� �� ����. ���� �������.");
            stateManager.Die();
        }
    }
}