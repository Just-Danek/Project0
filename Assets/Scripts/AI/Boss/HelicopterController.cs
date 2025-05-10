using System.Collections;
using UnityEngine;

public class HelicopterController : MonoBehaviour
{
    public Transform player;                  // ������ �� ������
    public Transform helicopter;              // ������ �� �������
    public Transform exitPoint;               // �����, ��-��� ������� �������� �������

    public float flyOutSpeed = 5f;            // �������� ������
    public float approachDistance = 10f;      // �� ����� ���������� ������� �������� �������
    public float circleSpeed = 20f;           // �������� �������� ������ ������ (�������� � �������)

    public float minDirectionChangeTime = 15f; // ���. �������� ����� �����������
    public float maxDirectionChangeTime = 30f; // ����. �������� ����� �����������

    public float tiltAngle = 20f;             // ���� ������� � ������
    public float tiltSpeed = 2f;              // �������� �������

    public GameObject bulletPrefab;           // ������ ����
    public float bulletSpeed = 30f;           // �������� ����
    public float fireInterval = 2f;           // �������� �������� (���)

    private bool isActivated = false;
    private bool isCircling = false;
    private int circleDirection = 1;          // 1 ��� -1

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isActivated)
        {
            isActivated = true;
            StartCoroutine(HelicopterSequence());
        }
    }

    IEnumerator HelicopterSequence()
    {
        // ����� �� ��� ������ � ����� ��� ������ (����� � �������)
        Vector3 targetPos = player.position + (player.forward * approachDistance) + Vector3.up * 5f;
        while (Vector3.Distance(helicopter.position, targetPos) > 0.5f)
        {
            helicopter.position = Vector3.MoveTowards(helicopter.position, targetPos, flyOutSpeed * Time.deltaTime);
            yield return null;
        }

        // ������ ����������� ����� � ������
        Quaternion targetRotation = Quaternion.LookRotation(player.position - helicopter.position);
        targetRotation *= Quaternion.Euler(tiltAngle, 0, 0);  // ��������� ������ ����

        while (Quaternion.Angle(helicopter.rotation, targetRotation) > 1f)
        {
            helicopter.rotation = Quaternion.Slerp(helicopter.rotation, targetRotation, tiltSpeed * Time.deltaTime);
            yield return null;
        }

        // ��������� �������� � ��������
        isCircling = true;
        StartCoroutine(ChangeDirectionRoutine());
        StartCoroutine(FireRoutine());
    }

    IEnumerator ChangeDirectionRoutine()
    {
        while (isCircling)
        {
            float waitTime = Random.Range(minDirectionChangeTime, maxDirectionChangeTime);
            yield return new WaitForSeconds(waitTime);
            circleDirection *= -1;
        }
    }

    IEnumerator FireRoutine()
    {
        while (isCircling)
        {
            FireAtPlayer();
            yield return new WaitForSeconds(fireInterval);
        }
    }

    void FireAtPlayer()
    {
        if (bulletPrefab != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, helicopter.position, Quaternion.identity);
            Vector3 dir = (player.position - helicopter.position).normalized;
            bullet.GetComponent<Rigidbody>().velocity = dir * bulletSpeed;
        }
    }

    void Update()
    {
        if (isCircling)
        {
            helicopter.RotateAround(player.position, Vector3.up, circleSpeed * circleDirection * Time.deltaTime);

            // ������� ������ ������� �� ������ � ��������
            Vector3 dirToPlayer = player.position - helicopter.position;
            Quaternion lookRotation = Quaternion.LookRotation(dirToPlayer);
            lookRotation *= Quaternion.Euler(tiltAngle, 0, 0);
            helicopter.rotation = Quaternion.Slerp(helicopter.rotation, lookRotation, tiltSpeed * Time.deltaTime);
        }
    }
}
