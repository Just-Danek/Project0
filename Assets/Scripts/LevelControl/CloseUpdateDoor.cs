using UnityEngine;

public class CloseUpdateDoors : MonoBehaviour
{
    [SerializeField] private float openHeight = 2.5f;
    [SerializeField] private float openSpeed = 1f;
    [SerializeField] private AudioSource openSound;

    private bool isOpening = false;
    private Vector3 targetPosition;
    private bool hasOpened = false;

    private void Start()
    {
        targetPosition = transform.position + Vector3.up * openHeight;
    }

    private void Update()
    {
        if (isOpening)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, openSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                isOpening = false; // ��������� �������� ����� ����������
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasOpened && other.CompareTag("Player") && StaticHolder.UpdateWasBought)
        {
            isOpening = true;
            hasOpened = true;

            if (openSound != null)
            {
                openSound.Play();
            }
            Debug.Log("����� �����������");
        }

    }
}
