using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("��������� ��������")]
    public float maxHealth = 100f;
    public float currentHealth;

    [Header("UI")]
    public Slider healthSlider; // ������ �� UI-������� ��������

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    public void PlayerTakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log("����� ������� ����: " + damage + ". ������� ��������: " + currentHealth);
        UpdateHealthUI();
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthUI()
    {
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth / maxHealth;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Body") && collision.collider.name.Contains("Hand"))
        {
            Debug.Log("�����");
        }
    }
    void Die()
    {
        Debug.Log("����� �����!");
        StaticHolder.DieStation = true;
        SceneManager.LoadScene(0);
        // ����� ����� �������� ����� Game Over � �.�.
    }
    void Update()
    {

    }
    public float GetCurrentHealth() => currentHealth;

    public float GetHealthPercent() => currentHealth / maxHealth;
}
