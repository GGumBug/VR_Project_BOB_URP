using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Game,
    Edit,
}

public class GameManager : MonoBehaviour
{
    #region Singletone

    private static GameManager instance = null;

    PlayerUI playerUI = null;

    public static GameManager GetInstance()
    {
        if (instance == null)
        {
            GameObject go = new GameObject("@GameManager");
            instance = go.AddComponent<GameManager>();

            DontDestroyOnLoad(go);
        }
        return instance;

    }
    #endregion

    public GameState state = GameState.Game;

    public Player player = new Player("", 0, 100, 100, 0, 0, 0, 0, 0, 0);

    public void CheckJugement(NoteObject note, float curtime)
    {
        if (1000 < GetPerfectTiming(note) - curtime)
        {
            Debug.Log("BAD");
            GetJudgmentUI().InitJudgeIMG("Judge_Bad");
            player.PlusHP(1);
            player.PlusScore(20);
            RefreshPlayerInfo();
            player.CountCheck(2);
        }
        else if (500 < GetPerfectTiming(note) - curtime)
        {
            Debug.Log("GOOD");
            GetJudgmentUI().InitJudgeIMG("Judge_Good");
            player.PlusHP(5);
            player.PlusScore(50);
            RefreshPlayerInfo();
            player.CountCheck(1);
        }
        else
        {
            Debug.Log("PERFACT");
            GetJudgmentUI().InitJudgeIMG("Judge_Perfect");
            player.PlusHP(10);
            player.PlusScore(100);
            RefreshPlayerInfo();
            player.CountCheck(0);
        }
    }

    public void CheckLongJugement(NoteObject note)
    {
        if (note.longNoteCount == note.maxLongNoteCount)
        {
            Debug.Log("MISS");
            GetJudgmentUI().InitJudgeIMG("Judge_Miss");
            player.MinusHP(10);
            RefreshPlayerInfo();
            player.CountCheck(3);
        }
        else if (note.longNoteCount > note.maxLongNoteCount * 0.7)
        {
            Debug.Log("BAD");
            GetJudgmentUI().InitJudgeIMG("Judge_Bad");
            player.PlusHP(1);
            player.PlusScore(20);
            RefreshPlayerInfo();
            player.CountCheck(2);
        }
        else if (note.longNoteCount > note.maxLongNoteCount * 0.2)
        {
            Debug.Log("GOOD");
            GetJudgmentUI().InitJudgeIMG("Judge_Good");
            player.PlusHP(5);
            player.PlusScore(50);
            RefreshPlayerInfo();
            player.CountCheck(1);
        }
        else
        {
            Debug.Log("PERFACT");
            GetJudgmentUI().InitJudgeIMG("Judge_Perfect");
            player.PlusHP(10);
            player.PlusScore(100);
            RefreshPlayerInfo();
            player.CountCheck(0);
        }
    }

    public void CheckLongPerfactJugement()
    {
        Debug.Log("PERFACT");
        GetJudgmentUI().InitJudgeIMG("Judge_Perfect");
        player.PlusHP(10);
        player.PlusScore(100);
        RefreshPlayerInfo();
        player.CountCheck(0);
    }

    public void Miss()
    {
        Debug.Log("MISS");
        GetJudgmentUI().InitJudgeIMG("Judge_Miss");
        player.MinusHP(10);
        RefreshPlayerInfo();
        player.CountCheck(3);
    }

    int GetPerfectTiming(NoteObject note)
    {
        return note.note.time + SheetManager.GetInstance().sheets[SheetManager.GetInstance().GetCurrentTitle()].offset;
    }

    void RefreshPlayerInfo()
    {
        if (playerUI == null)
        {
            playerUI = UIManager.GetInstance().GetUI("PlayerUI").GetComponent<PlayerUI>();
        }
        playerUI.SetPlayerInfo();
    }
    public void GameOver(int next)
    {
        StartCoroutine(IEGameOver(next));
    }

    JudgmentUI GetJudgmentUI()
    {
        GameObject go = UIManager.GetInstance().GetUI("JudgmentUI");
        JudgmentUI judgmentUI = go.GetComponent<JudgmentUI>();
        return judgmentUI;
    }

    IEnumerator IEGameOver(int next)
    {
        if (SheetManager.GetInstance().sheets[SheetManager.GetInstance().GetCurrentTitle()].notes[next - 1].type == 0)
        {
            float noteDleay = SheetManager.GetInstance().sheets[SheetManager.GetInstance().GetCurrentTitle()].notes[next - 1].time + SheetManager.GetInstance().sheets[SheetManager.GetInstance().GetCurrentTitle()].offset;
            yield return new WaitForSeconds(SheetManager.GetInstance().sheets[SheetManager.GetInstance().GetCurrentTitle()].offset * 0.001f);
            UIManager.GetInstance().OpenUI("ResultUI");
        }
        else
        {
            float duration = SheetManager.GetInstance().sheets[SheetManager.GetInstance().GetCurrentTitle()].notes[next - 1].tail - SheetManager.GetInstance().sheets[SheetManager.GetInstance().GetCurrentTitle()].notes[next - 1].time;
            Debug.Log(duration + SheetManager.GetInstance().sheets[SheetManager.GetInstance().GetCurrentTitle()].offset);
            yield return new WaitForSeconds((duration + SheetManager.GetInstance().sheets[SheetManager.GetInstance().GetCurrentTitle()].offset)*0.001f);
            UIManager.GetInstance().OpenUI("ResultUI");
        }
    }
}
