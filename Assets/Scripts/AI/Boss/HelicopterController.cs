using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class HelicopterController : MonoBehaviour
{
    public Transform player;                  // ������ �� ������
    public Transform helicopter;
    public GameObject hel; // ������ �� ������� (������������ ������)
    public HelicopterGunSystem gunSystem;     // ������� ��������
    public Transform exitPoint;               // �����, ��-��� ������� �������� �������
    public Transform escapePoint;             // ����� ���� ������ ������� ����� �������
    public AudioSource audioSource;
    public EnemyHeaths health;

    public float flyOutSpeed = 5f;            // �������� ������
    public float approachDistance = 10f;      // �� ����� ���������� ������� �������� �������
    public float circleSpeed = 20f;           // �������� �������� ������ ������ (�������� � �������)

    public float minDirectionChangeTime = 15f; // ���. �������� ����� �����������
    public float maxDirectionChangeTime = 30f; // ����. �������� ����� �����������

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
        if (audioSource != null)
        {
            audioSource.loop = true;
            audioSource.Play();
        }

        // ����� �� ��� ������ � ����� ��� ������ (����� � �������)
        Vector3 targetPos = player.position + (player.forward * approachDistance) + Vector3.up * 5f;
        while (Vector3.Distance(helicopter.position, targetPos) > 0.5f)
        {
            helicopter.position = Vector3.MoveTowards(helicopter.position, targetPos, flyOutSpeed * Time.deltaTime);
            yield return null;
        }

        // ������ ������������� ������� ����� � ������
        Quaternion targetRotation = GetCorrectedLookRotation(player.position - helicopter.position);
        while (Quaternion.Angle(helicopter.rotation, targetRotation) > 1f)
        {
            helicopter.rotation = Quaternion.Slerp(helicopter.rotation, targetRotation, 2f * Time.deltaTime);
            yield return null;
        }

        if (gunSystem != null)
        {
            gunSystem.ActivateWeapons();
        }

        // ��������� ��������
        isCircling = true;

        StartCoroutine(ChangeDirectionRoutine());
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
    void Update()
    {
        if (!isActivated) return;

        // �������� ������
        if (health != null && health.GetCurrentHp() <= 0f)
        {
            StartCoroutine(EscapeAndDestroy());
            isActivated = false; // ����� �� ���������� �����
            return;
        }

        if (isCircling)
        {
            helicopter.RotateAround(player.position, Vector3.up, circleSpeed * circleDirection * Time.deltaTime);

            Vector3 dirToPlayer = player.position - helicopter.position;
            Quaternion lookRotation = GetCorrectedLookRotation(dirToPlayer);
            helicopter.rotation = Quaternion.Slerp(helicopter.rotation, lookRotation, 2f * Time.deltaTime);
        }
    }

    IEnumerator EscapeAndDestroy()
    {
        isCircling = false;

        if (gunSystem != null)
        {
            gunSystem.DeactivateWeapons();
        }

        Vector3 target = escapePoint.position;

        // ������������� �������� � ������� ����� ������
        Vector3 directionToEscape = target - helicopter.position;
        Quaternion targetRotation = GetCorrectedLookRotation(directionToEscape);

        // ������ ������������� �������� � ������ �����������
        while (Quaternion.Angle(helicopter.rotation, targetRotation) > 1f)
        {
            helicopter.rotation = Quaternion.Slerp(helicopter.rotation, targetRotation, 2f * Time.deltaTime);
            yield return null;
        }

        // ���������� �������� � ����� ������
        while (Vector3.Distance(helicopter.position, target) > 1f)
        {
            helicopter.position = Vector3.MoveTowards(helicopter.position, target, flyOutSpeed * 1.5f * Time.deltaTime);
            yield return null;
        }

        // ������������� ����, ���� �� ���
        if (audioSource != null)
            audioSource.Stop();

        // ���������� ��������
        Destroy(hel);
    }



    // ��� ������� ������������ ������� � ������ ����, ��� ��� ������ �� X+
    Quaternion GetCorrectedLookRotation(Vector3 direction)
    {
        Quaternion baseRotation = Quaternion.LookRotation(direction);
        // ������������ �� -90� ������ Y, ����� forward ���� �� X (� �� �� Z)
        baseRotation *= Quaternion.Euler(0, -90f, 0);
        return baseRotation;
    }
}
