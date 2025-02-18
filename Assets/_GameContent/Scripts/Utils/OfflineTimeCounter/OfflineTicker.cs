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

    private const string RemainingTimeKey = "RemainingTime"; // Ключ для сохранения оставшегося времени

    private void Start()
    {
        // Автоматически запускаем таймер при старте
        if (isOnStart)
            StartTicking();
    }

    // Метод для запуска таймера
    public void StartTicking()
    {
        if (isTickingActive) return; // Если таймер уже активен, ничего не делаем

        isTickingActive = true;

        // Проверяем, сколько времени прошло с момента последнего выхода в оффлайн
        if (offlineTimeCounter.IsHasLastQuitTime())
        {
            long offlineSeconds = offlineTimeCounter.CalculateInactiveSeconds();

            // Вычисляем количество пропущенных интервалов и остаток времени
            int ticksMissed = (int)(offlineSeconds / secondsToTick);
            int remainderSeconds = (int)(offlineSeconds % secondsToTick);

            // Вызываем событие onTick за пропущенные интервалы
            for (int i = 0; i < ticksMissed; i++)
            {
                onTick.Invoke();
            }

            // Устанавливаем оставшееся время до следующего вызова события
            currentSecondsRemaining = secondsToTick - remainderSeconds;
        }
        else
        {
            // Если данных о времени выхода в оффлайн нет, загружаем сохраненное значение
            if (SaveSystem.HasKey(RemainingTimeKey))
            {
                currentSecondsRemaining = SaveSystem.LoadInt(RemainingTimeKey);
            }
            else
            {
                currentSecondsRemaining = secondsToTick;
            }
        }

        // Запускаем корутину для отслеживания времени
        if (tickCoroutine != null)
        {
            StopCoroutine(tickCoroutine);
        }
        tickCoroutine = StartCoroutine(TickCoroutine());
    }

    // Метод для остановки таймера
    public void StopTicking()
    {
        if (!isTickingActive) return; // Если таймер уже остановлен, ничего не делаем

        isTickingActive = false;

        // Останавливаем корутину
        if (tickCoroutine != null)
        {
            StopCoroutine(tickCoroutine);
            tickCoroutine = null;
        }

        // Сохраняем оставшееся время
        SaveSystem.SaveInt(RemainingTimeKey, currentSecondsRemaining);

        // Очищаем текст таймера (опционально)
        if (timerText != null)
        {
            timerText.text = "Timer stopped";
        }
    }

    private IEnumerator TickCoroutine()
    {
        while (isTickingActive)
        {
            // Обновляем таймер каждую секунду
            yield return new WaitForSeconds(1);

            // Уменьшаем оставшееся время
            currentSecondsRemaining--;

            // Если время вышло, вызываем событие и сбрасываем таймер
            if (currentSecondsRemaining <= 0)
            {
                onTick.Invoke();
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

    private void OnDestroy()
    {
        // Сохраняем время выхода в оффлайн
        offlineTimeCounter.SaveTime();

        // Сохраняем оставшееся время
        SaveSystem.SaveInt(RemainingTimeKey, currentSecondsRemaining);

        // Останавливаем корутину при уничтожении объекта
        StopTicking();
    }
}