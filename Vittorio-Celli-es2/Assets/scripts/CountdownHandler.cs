using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CountdownHandler : MonoBehaviour
{
    public Text countdownText;      
    public UnityEvent onCountdownEnd; 
    public Text winnerText;   
    public CarMovement[] cars; 

    private CancellationTokenSource cancellationTokenSource;

    private void OnEnable()
    {
        CarMovement.onRaceFinish.AddListener(HandleRaceFinish);
    }

    private void OnDisable()
    {
        CarMovement.onRaceFinish.RemoveListener(HandleRaceFinish);
    }

    public async void StartCountdown()
    {
        if (cancellationTokenSource != null)
        {
            cancellationTokenSource.Cancel();
        }

        cancellationTokenSource = new CancellationTokenSource();

        await RunCountdown(10, cancellationTokenSource.Token);
    }

    private async UniTask RunCountdown(int duration, CancellationToken token)
    {
        for (int i = duration; i >= 0; i--)
        {
            countdownText.text = i.ToString();
            await UniTask.Delay(1000, cancellationToken: token);

            if (token.IsCancellationRequested)
            {
                return;
            }
        }

        countdownText.text = "Go!";
        onCountdownEnd?.Invoke(); 
    }

    private void HandleRaceFinish(string winner)
    {
        winnerText.text = $"{winner} wins!"; 

        foreach (CarMovement car in cars)
        {
            car.StopMoving();
        }
    }
}
