using MikeNspired.XRIStarterKit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class VRGun : MonoBehaviour
{
    [SerializeField] private XRGrabInteractable grabInteractable;
    [Header("�����������")]
    [SerializeField] public int maxAmmo;
    public int currentAmmo;
    public Transform magazineSocket; // �����, ���� ����������� �������
    public bool IsLoaded => currentAmmo > 0;
    private VRMagazine currentMagazine;
    public bool IsCharged = true;
    public GameObject emptyMagazinePrefab; // ������ ������� ��������
    public Transform ejectPoint; // �����, ������ �������� �������
    [SerializeField] private GameObject internalMagazineModel; // ���������� ���������� ������� (������������/����������)

    [Header("��������� �����")]
    [SerializeField] private Transform movablePart; // ��������� ����� ������ (������)
    [SerializeField] private float recoilDistance = 0.2f; // ����������, �� ������� ��������� ����� ����� ���������
    [SerializeField] private float recoilDuration = 0.1f;     // ������� ������ ������ (���)

    [Header("��������� ��������")]
    public float fireRate = 0.1f;
    public float damage = 10f;
    public float range = 100f;

    [Header("��������� ������")]
    public bool laserEnabled = true;
    public LineRenderer laserLine; // ���� ������������ LineRenderer � ����������

    [Header("Muzzle Flash")]
    public ParticleSystem muzzleFlash;
    public Light muzzleLight;
    public float lightDuration; // ������������ ������� �����

    [Header("XR")]
    public InputActionProperty triggerAction; // <-- ���� ����������� Input Action � ��������
    public Transform firePoint;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip shotSound;

    [Header("������")]
    public string enemyTag = ""; // ��� �����
    private float nextFireTime = 0f;


    void Awake()
    {
        triggerAction.action.Enable();
    }
    void Update()
    {

        if (currentAmmo <= 0 && IsCharged)
        {
            IsCharged = false;
            EjectMagazine(); // ������������� ����������� ������� ��� ��������� ��������
        }
        // �������� ����� ����� Input System
        if (triggerAction.action != null && triggerAction.action.ReadValue<float>() > 0.8f && Time.time >= nextFireTime && grabInteractable.isSelected && IsLoaded)
        {
            Debug.Log("�������!");
            nextFireTime = Time.time + fireRate;
            Shoot();
        }

        if (laserEnabled && laserLine != null)
        {
            UpdateLaser();
        }
        else if (laserLine != null)
        {
            laserLine.enabled = false;
        }
    }

    void Shoot()
    {
        currentAmmo--;
        if (currentAmmo <= 0)
        {
            Debug.Log("�������! �������� ��������: " + currentAmmo);
        }
        // ���������� ������
        if (muzzleFlash != null)
        {
            muzzleFlash.Play();
        }
        if (muzzleLight != null)
            StartCoroutine(MuzzleLightFlash());
        // ����
        if (audioSource != null && shotSound != null)
            audioSource.PlayOneShot(shotSound);
        StartCoroutine(MoveRecoil());
        RaycastHit hit;
        if (Physics.Raycast(firePoint.position, firePoint.forward, out hit, range))
        {
            if (hit.collider.CompareTag("Head") || hit.collider.CompareTag("Leg") || hit.collider.CompareTag("Body"))
            {
                float finalDamage = damage;
                // ���������� ���� ���������
                string hitPartName = hit.collider.name.ToLower();


                if (hit.collider.CompareTag("Head"))
                {
                    finalDamage *= 2f;
                    Debug.Log("Headshot!");
                }
                else if (hitPartName.Contains("leg"))
                {
                    finalDamage *= 0.5f;
                    Debug.Log("Leg shot!");

                    // ��������� �����
                    EnemyStateManager movement = hit.collider.GetComponentInParent<EnemyStateManager>();
                    if (movement != null)
                    {
                        movement.walkSpeed = movement.walkSpeed * 0.9f;
                        movement.runSpeed = movement.runSpeed * 0.9f;
                    }
                }
                else
                {
                    // ���� � ������� ����
                    Debug.Log("Body shot!");
                }

                EnemyHeaths target = hit.collider.GetComponentInParent<EnemyHeaths>();
                if (target != null)
                {
                    target.TakeDamage(finalDamage);
                }
            }
        }
    }
    public bool CanInsertMagazine()
    {
        return currentMagazine == null;
    }
    public void InsertMagazine(VRMagazine magazine)
    {
        IsCharged = true;
        if (currentMagazine != null) return;

        currentMagazine = magazine;
        currentAmmo = Mathf.Min(magazine.ammoAmount, maxAmmo);

        // ������ ������� �������
        magazine.gameObject.SetActive(false);

        // ���������� ���������� ���������� �������
        if (internalMagazineModel != null)
            internalMagazineModel.SetActive(true);

        Debug.Log("������� ��������. �������: " + currentAmmo);
    }
    public void EjectMagazine()
    {
        // �������� ������ �������
        if (currentMagazine == null && emptyMagazinePrefab != null && ejectPoint != null)
        {
            Instantiate(emptyMagazinePrefab, ejectPoint.position, ejectPoint.rotation);
        }
        Debug.Log("���������� ������� ����");
        // ��������� ���������� ���������� �������
        internalMagazineModel.SetActive(false);
        internalMagazineModel.gameObject.SetActive(false);
        Debug.Log("��������� ���������� �������: " + internalMagazineModel.name);
        currentAmmo = 0;

        Debug.Log("������� ��������.");
    }
    IEnumerator MuzzleLightFlash()
    {
        muzzleLight.enabled = true;
        yield return new WaitForSeconds(lightDuration);
        muzzleLight.enabled = false;
    }
    private IEnumerator MoveRecoil()
    {
        // 1) ��������� �������� ��������� �������
        Vector3 originalLocalPos = movablePart.localPosition;

        // 2) ������ ��������� ������ ������: ����� �� ��������� Z
        Vector3 recoilOffsetLocal = new Vector3(0f, 0f, -recoilDistance);

        float halfDur = recoilDuration * 0.5f;
        float timer = 0f;

        // 3) ������� ������ ����� (�������� ������)
        while (timer < halfDur)
        {
            float t = timer / halfDur; 
            movablePart.localPosition = Vector3.Lerp(originalLocalPos,
                                                     originalLocalPos + recoilOffsetLocal,
                                                     t);
            timer += Time.deltaTime;
            yield return null;
        }

        // 4) ���������� ������ � �������� ��������� (�������� ��������)
        timer = 0f;
        while (timer < halfDur)
        {
            float t = timer / halfDur;
            movablePart.localPosition = Vector3.Lerp(originalLocalPos + recoilOffsetLocal,
                                                     originalLocalPos,
                                                     t);
            timer += Time.deltaTime;
            yield return null;
        }

        // 5) ����������� ����� �������� �������
        movablePart.localPosition = originalLocalPos;
    }

    void UpdateLaser()
    {
        RaycastHit hit;
        Vector3 endPoint;

        if (Physics.Raycast(firePoint.position, firePoint.forward, out hit, range))
        {
            endPoint = hit.point;
        }
        else
        {
            endPoint = firePoint.position + firePoint.forward * range;
        }

        laserLine.enabled = true;
        laserLine.SetPosition(0, firePoint.position);
        laserLine.SetPosition(1, endPoint);
    }
}
