using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class OfflineTicker : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private int secondsToTick;
    [SerializeField] private UnityEvent onTick;
    [SerializeField] private OfflineTimeCounter offlineTimeCounter;
    [SerializeField] private bool isOnStart;

    private Coroutine tickCoroutine;
    private int currentSecondsRemaining;
    private bool isTickingActive;

    private const string RemainingTimeKey = "RemainingTime"; // ���� ��� ���������� ����������� �������

    private void Start()
    {
        // ������������� ��������� ������ ��� ������
        if (isOnStart)
            StartTicking();
    }

    // ����� ��� ������� �������
    public void StartTicking()
    {
        if (isTickingActive) return; // ���� ������ ��� �������, ������ �� ������

        isTickingActive = true;

        // ���������, ������� ������� ������ � ������� ���������� ������ � �������
        if (offlineTimeCounter.IsHasLastQuitTime())
        {
            long offlineSeconds = offlineTimeCounter.CalculateInactiveSeconds();

            // ��������� ���������� ����������� ���������� � ������� �������
            int ticksMissed = (int)(offlineSeconds / secondsToTick);
            int remainderSeconds = (int)(offlineSeconds % secondsToTick);

            // �������� ������� onTick �� ����������� ���������
            for (int i = 0; i < ticksMissed; i++)
            {
                onTick.Invoke();
            }

            // ������������� ���������� ����� �� ���������� ������ �������
            currentSecondsRemaining = secondsToTick - remainderSeconds;
        }
        else
        {
            // ���� ������ � ������� ������ � ������� ���, ��������� ����������� ��������
            if (SaveSystem.HasKey(RemainingTimeKey))
            {
                currentSecondsRemaining = SaveSystem.LoadInt(RemainingTimeKey);
            }
            else
            {
                currentSecondsRemaining = secondsToTick;
            }
        }

        // ��������� �������� ��� ������������ �������
        if (tickCoroutine != null)
        {
            StopCoroutine(tickCoroutine);
        }
        tickCoroutine = StartCoroutine(TickCoroutine());
    }

    // ����� ��� ��������� �������
    public void StopTicking()
    {
        if (!isTickingActive) return; // ���� ������ ��� ����������, ������ �� ������

        isTickingActive = false;

        // ������������� ��������
        if (tickCoroutine != null)
        {
            StopCoroutine(tickCoroutine);
            tickCoroutine = null;
        }

        // ��������� ���������� �����
        SaveSystem.SaveInt(RemainingTimeKey, currentSecondsRemaining);

        // ������� ����� ������� (�����������)
        if (timerText != null)
        {
            timerText.text = "Timer stopped";
        }
    }

    private IEnumerator TickCoroutine()
    {
        while (isTickingActive)
        {
            // ��������� ������ ������ �������
            yield return new WaitForSeconds(1);

            // ��������� ���������� �����
            currentSecondsRemaining--;

            // ���� ����� �����, �������� ������� � ���������� ������
            if (currentSecondsRemaining <= 0)
            {
                onTick.Invoke();
                currentSecondsRemaining = secondsToTick;
            }

            // ��������� ����� �������
            UpdateTimerText(currentSecondsRemaining);
        }
    }

    private void UpdateTimerText(int timeRemaining)
    {
        if (timerText != null)
        {
            // ����������� ����� � mm:ss
            int minutes = timeRemaining / 60;
            int seconds = timeRemaining % 60;
            timerText.text = $"{minutes:00}:{seconds:00}";
        }
    }

    private void OnDestroy()
    {
        // ��������� ����� ������ � �������
        offlineTimeCounter.SaveTime();

        // ��������� ���������� �����
        SaveSystem.SaveInt(RemainingTimeKey, currentSecondsRemaining);

        // ������������� �������� ��� ����������� �������
        StopTicking();
    }
}