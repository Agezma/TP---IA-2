using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowMoney : MonoBehaviour
{
    [SerializeField] Text txt;
    public void AddMoneyFeedback(int money)
    {
        StartCoroutine(ShowMoneyAgregated(money));
    }
    IEnumerator ShowMoneyAgregated(int money)
    {
        txt.gameObject.SetActive(true);
        txt.text = "+ " + money;
        yield return new WaitForSeconds(4);
        txt.gameObject.SetActive(false);
    }
}
