using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretHolder : MonoBehaviour, IRestartable
{
    CharacterHead myChar;
    [SerializeField] float range;
    public Turret currentTurret;

    public InfoTurret infoPanel;

    public Turret[] turretPrefab;
    public string[] turretInfo;
    public Vector3 offset;
    public bool isBuilding;
    bool isBuilt = false;

    public bool builtLastRound = true;

    int currentTurretID = 0;


    void Start()
    {
        myChar = Main.Instance.player;
        RestartableManager.Instance.AddRestartable(this);
    }
    Vector3 dir;
    void Update()
    {
        if (Vector3.Distance(transform.position, myChar.transform.position) < range)
        {
            dir = (transform.position - Main.Instance.myCam.transform.position );
            infoPanel.transform.forward = new Vector3(dir.x, 0, dir.z);

            if (!isBuilding && !isBuilt)
            {
                StartBuildTurret();
            }
        }
        else
        {
            GetFar();
        }
    }

    void StartBuildTurret()
    {
        isBuilding = true;
        int currentTurretID = 0;
        currentTurret = Instantiate(turretPrefab[currentTurretID]);
        currentTurret.GetComponent<TurretState>().buildState = TurretState.state.isBuilding;
        currentTurret.transform.position = this.transform.position + offset;

        infoPanel.myName.text = currentTurret.TurretName;
        infoPanel.info.text = currentTurret.Description;
        infoPanel.filterSelected.text = currentTurret.filterInfo;
        infoPanel.gameObject.SetActive(true);
    }

    public TurretState myTurret;
    public void PlaceTurret()
    {
        if (isBuilding)
        {
            myTurret = currentTurret.GetComponentInChildren<TurretState>();
            if (myTurret.isBuyable)
            {
                Main.Instance.myMoneyManager.money -= myTurret.price;         
                infoPanel.gameObject.SetActive(false);
                myTurret.buildState = TurretState.state.built;
                isBuilt = true;
                builtLastRound = true;
                isBuilding = false;
            }
            else
            {
                myTurret.buildState = TurretState.state.isBuilding;
                infoPanel.info.text = "You don´t have enough gold";
            }
        }
    }

    public void GetFar()
    {
        infoPanel.gameObject.SetActive(false);

        if (currentTurret && currentTurret.GetComponent<TurretState>().buildState != TurretState.state.built)
        {
            Destroy(currentTurret.gameObject);
        }
        isBuilding = false;
    }
    public void GoNext(bool isNext)
    {
        Destroy(currentTurret.gameObject);
        if (isNext) currentTurretID++;
        else currentTurretID--;

        if (currentTurretID >= turretPrefab.Length) currentTurretID = 0;
        else if (currentTurretID <= -1) currentTurretID = turretPrefab.Length-1;

        currentTurret = Instantiate(turretPrefab[currentTurretID]);
        currentTurret.GetComponent<TurretState>().buildState = TurretState.state.isBuilding;
        currentTurret.transform.position = this.transform.position + offset;

        infoPanel.myName.text = currentTurret.TurretName;
        infoPanel.info.text = currentTurret.Description;
        infoPanel.filterSelected.text = currentTurret.filterInfo;

    }
    public void SelectFilter(int number)
    {
        if (number == 1)
            infoPanel.filterSelected.text = currentTurret.filterInfo;

        else if (number == 2)
            infoPanel.filterSelected.text = currentTurret.filterInfo;

        else if (number == 3)
            infoPanel.filterSelected.text = currentTurret.filterInfo;

        else if (number == 4)
            infoPanel.filterSelected.text = currentTurret.filterInfo;

    }

    public float gizmorange;
    [ExecuteInEditMode]
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, gizmorange);
    }

    public void SetOnWaveEnd()
    {
        builtLastRound = false;
    }

    public void RestartFromLastWave()
    {
        if (builtLastRound && myTurret != null)
        {
            Destroy(myTurret.gameObject);
            isBuilt = false;
        }

    }
}
