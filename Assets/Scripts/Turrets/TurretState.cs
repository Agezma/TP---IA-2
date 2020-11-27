using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretState : MonoBehaviour
{
    MoneyManager moneyManager;
    float money;
    [HideInInspector] public bool isBuyable;

    public float price;

    public enum state
    {
        isBuilding,
        failedToBuild,
        built
    }
    public state buildState;

    public void Start()
    {
        moneyManager = Main.Instance.myMoneyManager;
    }       

    void Update()
    {
        money = moneyManager.money;

        if (money < price)
        {
            isBuyable = false;
        }
        else
        {
            isBuyable = true;
        }

        if(buildState == state.built)
        {

        }
    }        
}
