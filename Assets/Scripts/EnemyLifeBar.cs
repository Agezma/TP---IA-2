using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyLifeBar : MonoBehaviour
{
    public Slider lifeBarPrefab;
    public Slider lifeBar;
    private Camera myCam;
    private Canvas canvas;
    public float offsetY;
    private void Awake()
    {
        lifeBar = Instantiate(lifeBarPrefab);
    }
    void Start()
    {
        myCam = Main.Instance.myCam;
        canvas = Main.Instance.worldCanvas;

        lifeBar.transform.SetParent(canvas.transform);
    }

    void Update()
    {
        if (lifeBar != null)
        {
            lifeBar.transform.position = transform.position + Vector3.up * offsetY;
    
            lifeBar.transform.LookAt(myCam.transform);

        }
    }

    public void UpdateLife()
    {
        lifeBar.value = GetComponent<Enemy>().life / GetComponent<Enemy>().startLife;   
    }
}
