using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class UniTest : MonoBehaviour
{

async UniTask Start()
{
    await DoSomething();
    Debug.Log("10 Secondi Passati");
}

async UniTask DoSomething()
{
    await UniTask.Delay(10000);
}

}
