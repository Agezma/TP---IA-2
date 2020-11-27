using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main
{
    private static Main instance = null;

    public static Main Instance 
    {
        get
        {
            if (instance == null)
                instance = new Main();          
            return instance;
        }
        private set { }
    }

    public Canvas myCanvas;
    public Canvas worldCanvas;
    public CharacterHead player;
    public MoneyManager myMoneyManager;
    public StatsManager statManager;
    public Camera myCam;
    public Base baseToAttack;
    public ChangeScene sceneManager;
    public ScreenMouse mouseLock;
    public EnemyManager enemyManager;
    public AdsManager adsManager;
    public GameObject losePanel;
    public MobileController mobilePad;
    public GameObject winPanel;
}
