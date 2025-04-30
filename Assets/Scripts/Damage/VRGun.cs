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
    public InputActionProperty LeftTrigger;
    public InputActionProperty RightTrigger;
    public InputActionProperty LeftGrip;
    public InputActionProperty RightGrip;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip shotSound;

    [Header("������")]
    public string enemyTag = ""; // ��� �����
    private float nextFireTime = 0f;

    string ap = null;
    EnemyStateManager movement = null;
    void Awake()
    {
        triggerAction.action.Enable();
        LeftGrip.action.Enable();
        RightGrip.action.Enable();
        LeftTrigger.action.Enable();
        RightTrigger.action.Enable();
        if (StaticHolder.Difficulty)
        {
            laserLine.enabled = false;
        }
    }
    void Update()
    {
        //Debug.Log("����� ����" + LeftGrip.action.ReadValue<float>());
        //Debug.Log("����� ������" + LeftTrigger.action.ReadValue<float>());
        //Debug.Log("������ ����" + RightGrip.action.ReadValue<float>());
        //Debug.Log("������ ������" + RightTrigger.action.ReadValue<float>());
        if (currentAmmo <= 0 && IsCharged)
        {
            IsCharged = false;
            currentMagazine = null;
            EjectMagazine(); // ������������� ����������� ������� ��� ��������� ��������
        }
        // �������� ����� ����� Input System
        //Debug.Log(grabInteractable.attachTransform);
        if (grabInteractable.attachTransform != null)
        {
            ap = grabInteractable.attachTransform.name;
        } else { ap = null; }
        
        //Debug.Log(grabInteractable.attachTransform.name);
        //if (triggerAction.action != null && triggerAction.action.ReadValue<float>() > 0.8f && Time.time >= nextFireTime && grabInteractable.isSelected && IsLoaded)
        if (((LeftGrip.action.ReadValue<float>() > 0.8f && LeftTrigger.action.ReadValue<float>() > 0.8f && ap.Contains("L")) || (RightGrip.action.ReadValue<float>() > 0.8f && RightTrigger.action.ReadValue<float>() > 0.8f && ap.Contains("R"))) && Time.time >= nextFireTime && grabInteractable.isSelected && IsLoaded)
        {
            Debug.Log("�������!");
            nextFireTime = Time.time + fireRate;
            Shoot();
        }

        if (laserEnabled && laserLine != null && !StaticHolder.Difficulty)
        {
            UpdateLaser();
        }
        else if (laserLine != null)
        {
            laserLine.enabled = false;
        }
    }

    public void Shoot()
    {
        currentAmmo--;
        if (currentAmmo <= 0)
        {
            Debug.Log("�������! �������� ��������: " + currentAmmo);
        }
        StaticHolder.countShots++;
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
                    if (hit.collider.GetComponentInParent<EnemyStateManager>() != null)
                    {
                        movement = hit.collider.GetComponentInParent<EnemyStateManager>();
                    }
                    else { movement = null; }
                    
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
                    StaticHolder.countHits++;
                    StaticHolder.Damage += finalDamage;
                    target.TakeDamage(finalDamage);
                }
            }
        }
        Debug.Log("��������� - " + StaticHolder.countShots);
        Debug.Log("��������� - " + StaticHolder.countHits);
        Debug.Log("���� - " + StaticHolder.Damage);
    }
    public bool CanInsertMagazine()
    {
        return !IsCharged;
    }
    public void InsertMagazine(VRMagazine magazine)
    {
        IsCharged = true;
        if (currentMagazine != null) { }

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
        if (movablePart == null)
        {
            yield break;
        }
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
