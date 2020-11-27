using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour, IRestartable
{
    public float money;
    public float startingMoney;
    float lastWaveMoney;

    public void RestartFromLastWave()
    {
        money = lastWaveMoney;
    }

    public void SetOnWaveEnd()
    {
        lastWaveMoney = money;
    }

    void Start()
    {
        money = lastWaveMoney = startingMoney;
        RestartableManager.Instance.AddRestartable(this);
    }

}
