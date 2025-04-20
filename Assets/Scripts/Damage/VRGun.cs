using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class VRGun : MonoBehaviour
{
    [Header("��������� ��������")]
    public float fireRate = 0.1f;
    public float damage = 10f;
    public float range = 100f;

    [Header("��������� ������")]
    public bool laserEnabled = true;
    public LineRenderer laserLine; // ���� ������������ LineRenderer � ����������

    [Header("XR ����������")]
    public InputActionProperty triggerAction; // <-- ���� ����������� Input Action � ��������
    public Transform firePoint;

    [Header("������")]
    public string enemyTag = ""; // ��� �����
    private float nextFireTime = 0f;
    void Awake()
    {
        triggerAction.action.Enable();
    }
    void Update()
    {
        // �������� ����� ����� Input System
        if (triggerAction.action != null && triggerAction.action.ReadValue<float>() > 0.8f && Time.time >= nextFireTime)
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
