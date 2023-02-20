using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScene : MonoBehaviour
{
    private void Awake()
    {
        AudioManager.GetInstance().PlayBgm("Bones");
        RankSystem.GetInstance().StartRankSystem();

    }
}
