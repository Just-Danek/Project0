using UnityEngine;

public class HelicopterBoss : MonoBehaviour
{
    public Transform player; // �����
    public float rotationSpeed = 0.2f; // �������� �������� (��������� ��� ���������)
    public float radius = 10f; // ������ ��������� ��������
    public float riseHeight = 30f; // ��������� ������ �������� ��-��� ������
    public float riseSpeed = 4f; // �������� �����
    public float approachSpeed = 2f; // �������� ������� � ������

    private float angle = 0f; // ��������� ���� ��������
    private bool isActivated = false; // ��������� �����
    private bool isRising = false; // �����
    private bool isApproachingPlayer = false; // ���������� � ������
    private bool isRotating = false; // ������ ��������

    private Vector3 targetRisePosition; // ���� ��� ������
    private Vector3 targetPosition; // ������� �� ������� �� ������, ��� ����� ������ ��������

    private void Update()
    {
        if (!isActivated) return;

        if (isRising)
        {
            RiseFromCover(); // �����
        }
        else if (isApproachingPlayer)
        {
            ApproachToRadius(); // ������ � ������� ��������
        }
        else if (isRotating)
        {
            RotateAroundPlayer(); // �������� ������ ������
        }
    }

    private void RiseFromCover()
    {
        // ������ ��������� �������� �� ������������ ������
        transform.position = Vector3.MoveTowards(transform.position, targetRisePosition, riseSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetRisePosition) < 0.1f)
        {
            // ����� �������� ������ ������, �������� ������ � �������
            isRising = false;
            isApproachingPlayer = true;
        }
    }

    private void ApproachToRadius()
    {
        // �������, ������� ��������� �� ������� �� ������ �� ������ riseHeight
        targetPosition = new Vector3(player.position.x + Mathf.Cos(angle) * radius,
                                      player.position.y + riseHeight,
                                      player.position.z + Mathf.Sin(angle) * radius);

        // ������ ��������� � ����� �� �������
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, approachSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            // ����� �������� ������ ������� �� �������, �������� ��������
            isApproachingPlayer = false;
            isRotating = true;
        }
    }

    private void RotateAroundPlayer()
    {
        // ������ ������� �������� ������ ������ �� �������� �������
        angle += rotationSpeed * Time.deltaTime; // ��������� ��������

        // ������������ ����� ������� ��������� �� ����� �� �������
        float x = player.position.x + Mathf.Cos(angle) * radius;
        float z = player.position.z + Mathf.Sin(angle) * radius;
        float y = transform.position.y; // ������ ������� ����������

        // ��������� ������� ���������
        transform.position = new Vector3(x, y, z);

        // ������ ������������ �������� � ������� ������
        Vector3 direction = player.position - transform.position;

        // ���������� �������� ����� � ������ (������� �� ������)
        Quaternion targetRotation = Quaternion.LookRotation(direction); // ��������� �������� � ������

        // ������ ������������ ��������
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    public void ActivateBoss()
    {
        // �������� ��������� ������� ��� ������
        targetRisePosition = new Vector3(transform.position.x, transform.position.y + riseHeight, transform.position.z);
        isActivated = true;
        isRising = true; // �������� �����
    }
}
