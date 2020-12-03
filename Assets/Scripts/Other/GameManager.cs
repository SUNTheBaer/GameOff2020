using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public BossManager bossManager = new BossManager();
    public SoundManager soundManager;

    private void Awake()
    {
        GameData data = SaveLoad.Load();

        //load = data.load
    }
}