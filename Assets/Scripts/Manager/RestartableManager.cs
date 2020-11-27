using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartableManager
{
    private static RestartableManager _instance;
    public static RestartableManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = new RestartableManager();
            return _instance;
        }
        private set { }
    }

    List<IRestartable> allRestartables = new List<IRestartable>();

    public void Initialize()
    {
        allRestartables = new List<IRestartable>();      
    }

    public void WaveEnded()
    {
        for (int i = 0; i < allRestartables.Count; i++)
        {
            allRestartables[i].SetOnWaveEnd();
        }
    }

    public void RestartFromLastWave()
    {        for (int i = 0; i < allRestartables.Count; i++)
        {
            allRestartables[i].RestartFromLastWave();
        }         
    }
       
    public void AddRestartable(IRestartable element)
    {
        if (!allRestartables.Contains(element))
            allRestartables.Add(element);
    }

    public void RemoveRestarable(IRestartable element)
    {
        if (allRestartables.Contains(element))
            allRestartables.Remove(element);
    }
}
