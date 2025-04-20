using System.Collections.Generic;
using UnityEngine;
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

    [Header("������")]
    public Transform firePoint; // ����� ������ ����
    public string enemyTag = "Enemy"; // ��� �����

    private float nextFireTime = 0f;
    private InputDevice rightHand;
    private InputDevice leftHand;
    void TryInitialize()
    {
        var rightDevices = new List<InputDevice>();
        var leftDevices = new List<InputDevice>();

        InputDevices.GetDevicesAtXRNode(XRNode.RightHand, rightDevices);
        InputDevices.GetDevicesAtXRNode(XRNode.LeftHand, leftDevices);

        if (rightDevices.Count > 0)
            rightHand = rightDevices[0];
        if (leftDevices.Count > 0)
            leftHand = leftDevices[0];
    }
    void Start()
    {
        TryInitialize();
    }

    void Update()
    {
        if (!rightHand.isValid || !leftHand.isValid)
        {
            TryInitialize();
        }
        // ��������� �����
        if (laserEnabled && laserLine != null)
        {
            UpdateLaser();
        }
        else if (laserLine != null)
        {
            laserLine.enabled = false;
        }
        // �������� ������� ������ �� ���������
        bool rightTrigger = rightHand.TryGetFeatureValue(CommonUsages.triggerButton, out bool rTriggerPressed) && rTriggerPressed;
        bool leftTrigger = leftHand.TryGetFeatureValue(CommonUsages.triggerButton, out bool lTriggerPressed) && lTriggerPressed;
        if ((rightTrigger || leftTrigger) && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(firePoint.position, firePoint.forward, out hit, range))
        {
            if (hit.collider.CompareTag(enemyTag))
            {
                float finalDamage = damage;

                // ���������� ���� ���������
                string hitPartName = hit.collider.gameObject.name.ToLower();

                if (hitPartName.Contains("head"))
                {
                    finalDamage *= 2f;
                    Debug.Log("Headshot!");
                }
                else if (hitPartName.Contains("leg") || hitPartName.Contains("foot"))
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
