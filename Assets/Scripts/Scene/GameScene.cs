using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : MonoBehaviour
{
    private void Awake()
    {
        SheetManager.GetInstance().sheets[SheetManager.GetInstance().GetCurrentTitle()].Init();

        AudioManager.GetInstance().InitClip(SheetManager.GetInstance().GetCurrentTitle());
        AudioManager.GetInstance().progressTime = 0f;
        AudioManager.GetInstance().Play();

        NoteManager.GetInstance().StartGame();

        UIManager.GetInstance().OpenUI("PlayerUI");
        UIManager.GetInstance().OpenUI("JudgmentUI");

        ObjectPoolManager.GetInstance();
        NoteManager.GetInstance();
    }
}
