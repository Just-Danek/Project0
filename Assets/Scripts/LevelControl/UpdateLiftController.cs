using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.HDROutputUtils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class UpdateLiftController : MonoBehaviour
{
    [Header("��������� �����")]
    public float waitTime = 3f;               // ����� �������� � ����� ����� ��������� �����
    private bool playerInElevator = false;
    private float timer = 0f;
    int l = StaticHolder.CurrentLevel + 1;
    public GameObject loadingUI;
    public Slider progres;
    bool IsLoading = false;
    bool IsLoaded = false;
    AsyncOperation operation;

    private void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && StaticHolder.UpdateLevelEnd)
        {
            playerInElevator = true;
            timer = 0f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInElevator = false;
            timer = 0f;
        }
    }

    private void Update()
    {
        Debug.Log("������� ������� �� ����� - " + StaticHolder.CurrentLevel);
        //Debug.Log(l);
        if (playerInElevator && !IsLoading)
        {
            IsLoading = true;
            LoadNextLevel();
        }
    }

    void LoadNextLevel()
    {
        Debug.Log("����������� �������� �����...");
        //SceneManager.LoadSceneAsync(l);
        StaticHolder.CurrentLevel = l;
        StaticHolder.levelCheksComplete = false;
        StaticHolder.UpdateLevelEnd = false;
        StaticHolder.ItemPickedUp = false;
        StaticHolder.PropitalHealActive = false;
        StaticHolder.SandevistanActive = false;
        StartCoroutine(LoadSceneAsync(l));
    }
    IEnumerator LoadSceneAsync(int sceneIndex)
    {
        Debug.Log("�������� ����");
        if (!IsLoaded)
        {
            //loadingUI.SetActive(true);
            operation = SceneManager.LoadSceneAsync(sceneIndex);
            Debug.Log("����� ��������");
            IsLoaded = true;
        }

        while (!operation.isDone)
        {
            // ����� ����� ��������� �������� ���, ���� �� ����:
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            progres.value = progress;
            yield return null;
        }
    }
}
