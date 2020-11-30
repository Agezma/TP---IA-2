using System;
using UnityEngine;

//[ExecuteInEditMode]
public class GridEntity : MonoBehaviour
{
	public event Action<GridEntity> OnMove = delegate {};

    public Action OnDestroyed = delegate { };
    public bool onGrid;

    public void Update()
    {
        
    }
    public void UpdateGrid()
    {      
	    OnMove(this);
	}
}
