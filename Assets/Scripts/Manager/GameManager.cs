using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public Canvas myCanvas;
    public Canvas worldCanvas;
    public CharacterHead myPlayer;
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

    private void Awake()
    {
        Main.Instance.myCanvas = myCanvas;
        Main.Instance.worldCanvas = worldCanvas;
        Main.Instance.player = myPlayer;
        Main.Instance.myMoneyManager = myMoneyManager;
        Main.Instance.statManager = statManager;
        Main.Instance.myCam = myCam;
        Main.Instance.baseToAttack = baseToAttack;
        Main.Instance.sceneManager = sceneManager;
        Main.Instance.mouseLock = mouseLock;
        Main.Instance.enemyManager = enemyManager;
        Main.Instance.losePanel = losePanel;
        Main.Instance.adsManager = adsManager;
        Main.Instance.mobilePad = mobilePad;
        Main.Instance.winPanel = winPanel;

        UpdateManager.Instance.Initialize();
        RestartableManager.Instance.Initialize();
    }

    private void Update()
    {
        UpdateManager.Instance.OnUpdate();
    }
    private void FixedUpdate()
    {
        UpdateManager.Instance.OnFixedUpdate();
    }       
}
