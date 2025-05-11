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
    [Header("��������� ������")]
    public InputActionProperty HealButton;
    public InputActionProperty SandewistanButton;
    [Header("��������� ����")]
    public InputActionProperty MenuButton;
    public GameObject MenuCanvas;
    public Slider healthSlider; // ������ �� UI-������� ��������
    public GameObject MainCheck;
    public GameObject SecCheck;
    [Header("�� �������")]
    public GameObject controller;
    float oldSpeed;
    DynamicMoveProvider speed = null;
    [Header("Katana")]
    public GameObject katana;
    private void Awake()
    {
        HealButton.action.Enable();
        SandewistanButton.action.Enable();
        MenuButton.action.Enable();
        speed = controller.GetComponent<DynamicMoveProvider>();
        speed.moveSpeed = StaticHolder.PlayerBasicSpeed;
        oldSpeed = StaticHolder.PlayerBasicSpeed;
        if (StaticHolder.StrongLegs)
        {
            speed.moveSpeed = speed.moveSpeed * StaticHolder.StrongLegsKoef;
        }
        maxHealth = maxHealth + StaticHolder.PlayerHPBuff;
        katana.SetActive(StaticHolder.Katana);
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
        if (StaticHolder.StrongArms && StaticHolder.StrongLegs) { StaticHolder.Ciborg = true; }
        StaticHolder.DiedinCyberpunk = true;
        StaticHolder.DieStation = true;
        StaticHolder.CurrentGun = 0;
        StaticHolder.BuffGrenade = false;
        StaticHolder.BuffGunFireRate = 1f;
        StaticHolder.BuffGunDamage = 1f;
        StaticHolder.BuffGunMaxAmmo = 1f;
        StaticHolder.PlayerHPBuff = 0;
        StaticHolder.PlayerBasicSpeed = 3f;
        StaticHolder.SpeedBuffAfterDamage = false;
        StaticHolder.SpeedAfterDamageValue = 1f;
        StaticHolder.PropitalHeal = false;
        StaticHolder.PropitalHealActive = false;
        StaticHolder.Sandevistan = false;
        StaticHolder.SandevistanActive = false;
        StaticHolder.Akimbo = false;
        StaticHolder.AkimboWas = false;
        StaticHolder.Katana = false;
        StaticHolder.StrongArms = false;
        StaticHolder.StrongArmsKoef = 1f;
        StaticHolder.StrongLegs = false;
        StaticHolder.StrongLegsKoef = 1f;
        SceneManager.LoadSceneAsync(0);
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
        if (MenuButton.action.ReadValue<float>() >= 0.7)
        {
            MenuCanvas.SetActive(true);
        }
        else
        {
            MenuCanvas.SetActive(false);
        }
        if (StaticHolder.levelCheksComplete)
        {
            MainCheck.SetActive(true);
        }
        if (StaticHolder.ItemPickedUp)
        {
            SecCheck.SetActive(true);
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
    public void ToMain()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
