using MikeNspired.XRIStarterKit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class VRgunForEnemy : MonoBehaviour
{
    [Header("�����������")]
    [SerializeField] public float maxAmmo;
    public float currentAmmo;
    public bool IsLoaded => currentAmmo > 0;
    private VRMagazine currentMagazine;
    public bool IsCharged = true;

    [Header("��������� �����")]
    [SerializeField] private Transform movablePart; // ��������� ����� ������ (������)
    [SerializeField] private float recoilDistance = 0.2f; // ����������, �� ������� ��������� ����� ����� ���������
    [SerializeField] private float recoilDuration = 0.1f;     // ������� ������ ������ (���)

    [Header("��������� ��������")]
    public float fireRate;
    public float damage;
    public float range = 100f;

    [Header("��������� ������")]
    public bool laserEnabled = true;
    public LineRenderer laserLine; // ���� ������������ LineRenderer � ����������

    [Header("Muzzle Flash")]
    public ParticleSystem muzzleFlash;
    public Light muzzleLight;
    public float lightDuration; // ������������ ������� �����

    [Header("XR")]
    public Transform firePoint;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip shotSound;

    [Header("������")]
    public string enemyTag = ""; // ��� �����
    private float nextFireTime = 0f;

    string ap = null;
    EnemyStateManager movement = null;
    public void Shoot()
    {
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
            //Debug.Log("��������� � " + hit.collider.tag);
            if (hit.collider.CompareTag("Player"))
            {
                Debug.Log("� ������ ��������");
                float finalDamage = damage;
                PlayerHealth player = hit.collider.GetComponent<PlayerHealth>();
                if (player != null)
                {
                    player.PlayerTakeDamage(finalDamage);
                }
            }
        }
        //Debug.Log("��������� - " + StaticHolder.countShots);
        //Debug.Log("��������� - " + StaticHolder.countHits);
        //Debug.Log("���� - " + StaticHolder.Damage);
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
    public void Update()
    {
        UpdateLaser();
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

