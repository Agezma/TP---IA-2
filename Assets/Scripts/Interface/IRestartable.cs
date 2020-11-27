using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRestartable 
{
    void SetOnWaveEnd();
    void RestartFromLastWave();
    
}
