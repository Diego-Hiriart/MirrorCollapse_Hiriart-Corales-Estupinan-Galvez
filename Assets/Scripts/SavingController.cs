using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavingController : MonoBehaviour
{
    [SerializeField]
    private LevelController level;

    public void SaveGame()
    {
        level.SaveGame();
    }
}
