using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildTurret : MonoBehaviour
{
    public Turret[] turretPrefab;
    public Vector3 offset;
    public bool isBuilding;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !isBuilding)
        {
            StartBuildTurret();
        }
        PlaceTurret();
    }
    
    void StartBuildTurret()
    {
        isBuilding = true;
        GameObject turret = Instantiate(turretPrefab[Random.Range(0,turretPrefab.Length-1)], this.transform).gameObject;
        turret.GetComponent<TurretState>().buildState = TurretState.state.isBuilding;
        turret.transform.position = this.transform.position + transform.forward  * 3 + offset;
    }

    public void PlaceTurret()
    {
        if (Input.GetMouseButtonDown(0) && isBuilding)
        {
            TurretState myTurret = GetComponentInChildren<TurretState>();
            if (myTurret.isBuyable)
            {
                myTurret.transform.parent = null;
                Main.Instance.myMoneyManager.money -= myTurret.price;                
            }
            else
            {
                Destroy(myTurret.gameObject);
            }
            isBuilding = false;
        }
    }
}
