using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackChances
{
    public float chance;
    public string attackName;

    public AttackChances(float chance, string attackName)
    {
        this.chance = chance;
        this.attackName = attackName;
    }
}

public class GameManager : MonoBehaviour
{
    public SoundManager soundManager;

    private void Awake()
    {
        GameData data = SaveLoad.Load();

        //load = data.load
    }
}
