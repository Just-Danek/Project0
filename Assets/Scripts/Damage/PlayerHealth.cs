using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using XR.Interaction.Toolkit.Samples;
using UnityEngine.InputSystem;

public class PlayerHealth : MonoBehaviour
{
    [Header("��������� ��������")]
    public float maxHealth;
    public float currentHealth;
    public InputActionProperty HealButton;
    public InputActionProperty SandewistanButton;
    public GameObject controller;
    float oldSpeed;
    DynamicMoveProvider speed = null;
    [Header("UI")]
    public Slider healthSlider; // ������ �� UI-������� ��������
    private void Awake()
    {
        HealButton.action.Enable();
        SandewistanButton.action.Enable();
        speed = controller.GetComponent<DynamicMoveProvider>();
        speed.moveSpeed = StaticHolder.PlayerBasicSpeed;
        oldSpeed = StaticHolder.PlayerBasicSpeed;
        maxHealth = maxHealth + StaticHolder.PlayerHPBuff;
    }
    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
        Debug.Log("�������� ������ = " + currentHealth);
    }

    public void PlayerTakeDamage(float damage)
    {
        if (StaticHolder.SpeedBuffAfterDamage)
        {
            StartCoroutine(SpeedAfterDamage());
        }
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
            Debug.Log("������");
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
        Debug.Log("�������� - " + currentHealth + " �� "+ maxHealth + " ������ ������ " + HealButton.action.ReadValue<float>());
        if (HealButton.action.ReadValue<float>() >= 0.7f && StaticHolder.PropitalHeal && currentHealth < maxHealth && !StaticHolder.PropitalHealActive)
        {
            StartCoroutine(Propital());
        }
        Debug.Log("������ ������������ " + SandewistanButton.action.ReadValue<float>());
        if (SandewistanButton.action.ReadValue<float>() >= 0.7 && StaticHolder.Sandevistan && !StaticHolder.SandevistanActive)
        {
            StartCoroutine(Sandewistan());
        }
    }
    public float GetCurrentHealth() => currentHealth;

    public float GetHealthPercent() => currentHealth / maxHealth;
    IEnumerator SpeedAfterDamage()
    {
        // ����� ���������� ��������
        Debug.Log("��������� ����� ��������� ����� ��������");
        speed.moveSpeed = StaticHolder.PlayerBasicSpeed * StaticHolder.SpeedAfterDamageValue;

        yield return new WaitForSeconds(StaticHolder.SpeedTimeAfterDamage);

        // �������� ���������
        Debug.Log("��������� ����� ��������� ����� ���������");
        speed.moveSpeed = oldSpeed;
    }
    IEnumerator Propital()
    {
        Debug.Log("����� �����");
        float wastedTime = 0f;
        StaticHolder.PropitalHealActive = true;
        while (wastedTime < StaticHolder.PropitalHealValue)
        {
            if (currentHealth < maxHealth) { currentHealth++; }
            wastedTime += 1f;
            yield return new WaitForSecondsRealtime(1f); // ��� 1 �������
        }
        Debug.Log("����� ��������");
    }
    IEnumerator Sandewistan()
    {
        Debug.Log("���������� ������� ��������");
        StaticHolder.SandevistanActive = true;
        // ��������� �����
        Time.timeScale = StaticHolder.SandevistanTimeSlower;
        Time.fixedDeltaTime = 0.02f * Time.timeScale; // ����� ��� ���������� ������ ������
        yield return new WaitForSecondsRealtime(StaticHolder.SandevistanTime); // ��� 
        // ���������� ����� � ����������� ���������
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
        Debug.Log("���������� ������� ����������");
    }
}
