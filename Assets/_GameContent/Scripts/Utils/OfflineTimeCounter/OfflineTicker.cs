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
    [SerializeField] private int maxTicks;
    private Coroutine tickCoroutine;
    private int currentSecondsRemaining;
    private bool isTickingActive;



    public int GetOfflineTicks()
    {
        if (offlineTimeCounter.IsHasLastQuitTime())
        {
            double inactiveSecondsDouble = offlineTimeCounter.CalculateInactiveSeconds();
            print("Last offline time (sec) " + inactiveSecondsDouble);
            long inactiveSeconds = (long)inactiveSecondsDouble;

            int ticks = 0;
            while (inactiveSeconds >= secondsToTick)
            {
                print("enter Tick");
                if (ticks >= maxTicks)
                {
                    inactiveSeconds = 0;
                    return maxTicks;
                }
                ticks++;
                inactiveSeconds -= secondsToTick;
            }
            return ticks;
        }
        return 0;
    }
    public int LastedSeconds()
    {
        if (offlineTimeCounter.IsHasLastQuitTime())
        {
            double inactiveSecondsDouble = offlineTimeCounter.CalculateInactiveSeconds();
            print("Last offline time (sec) " + inactiveSecondsDouble);
            long inactiveSeconds = (long)inactiveSecondsDouble;

            int ticksThresold = maxTicks;
            while (inactiveSeconds >= secondsToTick)
            {
                print("enter Tick");
                if (ticksThresold <= 0)
                {
                    inactiveSeconds = 0;
                    break;
                }
                ticksThresold--;
                inactiveSeconds -= secondsToTick;
                onTick?.Invoke();
            }
            currentSecondsRemaining -= (int)inactiveSeconds;
        }
        return 0;
    }
    public void CheckOfflineTime()
    {
        if (offlineTimeCounter.IsHasLastQuitTime())
        {
            double inactiveSecondsDouble = offlineTimeCounter.CalculateInactiveSeconds();
            long inactiveSeconds = (long)inactiveSecondsDouble;

            int ticksThresold = maxTicks;
            while (inactiveSeconds >= secondsToTick)
            {
                if (ticksThresold <= 0)
                {
                    inactiveSeconds = 0;
                    break;
                }
                ticksThresold--;
                inactiveSeconds -= secondsToTick;
                onTick?.Invoke();
            }
            currentSecondsRemaining -= (int)inactiveSeconds;
        }
        else
        {
            offlineTimeCounter.SaveTime();
        }
    }
    public void ContinueTicking()
    {
        currentSecondsRemaining = secondsToTick;

        CheckOfflineTime();

        if (tickCoroutine != null)
            StopCoroutine(tickCoroutine);
        tickCoroutine = StartCoroutine(TickCoroutine());
    }
    public void StartNewTicking()
    {
        offlineTimeCounter.SaveTime();
        currentSecondsRemaining = secondsToTick;
        if (tickCoroutine != null)
            StopCoroutine(tickCoroutine);
        tickCoroutine = StartCoroutine(TickCoroutine());
    }

    public void StopTicking()
    {
        if (tickCoroutine != null)
            StopCoroutine(tickCoroutine);
    }
    public void DeleteTimer()
    {
        offlineTimeCounter.DeleteTime();
    }
    public void TickEventOnline()
    {
        onTick?.Invoke();
        offlineTimeCounter.SaveTime();
    }

    private IEnumerator TickCoroutine()
    {

        UpdateTimerText(currentSecondsRemaining);

        while (true)
        {
            // Обновляем таймер каждую секунду
            yield return new WaitForSeconds(1);

            // Уменьшаем оставшееся время
            currentSecondsRemaining--;

            // Если время вышло, вызываем событие и сбрасываем таймер
            if (currentSecondsRemaining <= 0)
            {
                TickEventOnline();
                currentSecondsRemaining = secondsToTick;
            }

            // Обновляем текст таймера
            UpdateTimerText(currentSecondsRemaining);
        }
    }

    private void UpdateTimerText(int timeRemaining)
    {
        if (timerText != null)
        {
            // Форматируем время в mm:ss
            int minutes = timeRemaining / 60;
            int seconds = timeRemaining % 60;
            timerText.text = $"{minutes:00}:{seconds:00}";
        }
    }

}