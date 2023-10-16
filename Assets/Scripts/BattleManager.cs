using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    #region Singleton

    public static BattleManager instance;

    void Awake()
    {
        instance = this;
    }

    #endregion

    public BattleController battleController;
}
