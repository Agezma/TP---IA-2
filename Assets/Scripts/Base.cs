using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour, IRestartable
{
    [HideInInspector]
    public float lives;
    float endWaveLives;

    public float startingLife;

    private ChangeScene sceneChanger;

    GameObject losePanel;

    void Start()
    {
        sceneChanger = Main.Instance.sceneManager;
        losePanel = Main.Instance.losePanel;
        lives = endWaveLives = startingLife;
        RestartableManager.Instance.AddRestartable(this);
    }     

    public void TakeDamage(float damage)
    {
        lives -= damage;
        if(lives<= 0)
        {
            DestroyBase();
        }
    }
    public void DestroyBase()
    {
        //Main.Instance.mouseLock.UnlockMouse();
        //sceneChanger.LoadScene("Lose");
        losePanel.SetActive(true);
    }

    public void RestartFromLastWave()
    {
        losePanel.SetActive(false);
        lives = endWaveLives;
    }

    public void SetOnWaveEnd()
    {
        endWaveLives = lives;
    }
}
