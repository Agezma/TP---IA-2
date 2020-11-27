using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IController
{
    float HorizontalSpeed();
    float VerticalSpeed();

    bool AttackStart();
    bool AttackRelease();
    bool isAttacking();

    float HorizontalCameraSpeed();
    float VerticalCameraSpeed();

    bool SpawnWave();

    bool OpenConsole();
}
