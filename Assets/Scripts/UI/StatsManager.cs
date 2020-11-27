using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsManager : MonoBehaviour, IUpdate
{
    private Base myBase;
    private MoneyManager myMoney;

    public Text moneyText;
    public Text livesText;
    public Image lifeIm;


    void Start()
    {
        UpdateManager.Instance.AddUpdate(this);
        myBase = Main.Instance.baseToAttack;
        myMoney = Main.Instance.myMoneyManager;
    }

    public void OnUpdate()
    {
        UpdateMoney();
        UpdateLives();
    }
    
    public void UpdateMoney()
    {
        moneyText.text = "" + myMoney.money;
    }

    void UpdateLives()
    {
        livesText.text =  myBase.lives + "/" + myBase.startingLife;
        lifeIm.fillAmount = myBase.lives / myBase.startingLife;
    }
}
