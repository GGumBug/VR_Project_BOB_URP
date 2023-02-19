using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Count
{
    perfect,
    good,
    bad,
    miss,
}
public class Player
{
    public string playerName { get; private set; }
    public int score { get; private set; }
    public int maxHp { get; private set; }
    public int hp { get; private set; }
    public int combo { get; private set; }
    public int maxcombo { get; private set; }
    public int perfectCount { get; private set; }
    public int goodCount { get; private set; }
    public int badCount { get; private set; }
    public int missCount { get; private set; }

    TitleUI titleUI;
    public Player(string playerName, int score, int maxHp, int hp, int combo, int maxcombo, int perfectCount, int goodCount, int badCount, int missCount)
    {
        this.playerName = playerName;
        this.score = score;
        this.maxHp = maxHp;
        this.hp = hp;
        this.combo = combo;
        this.maxcombo = maxcombo;
        this.perfectCount = perfectCount;
        this.goodCount = goodCount;
        this.badCount = badCount;
        this.missCount = missCount;

    }

    public void PlusHP(int plusHp)
    {
        combo++;
        hp += plusHp;
        Mathf.Clamp(hp, 0, 100);
    }

    public void MinusHP(int minusHp)
    {
        combo = 0;
        hp -= minusHp;
        Mathf.Clamp(hp, 0, 100);
    }

    public void PlusScore(int plusScore)
    {
        score += plusScore;
    }

    public void ResetPlayer()
    {
        score = 0;
        maxHp = 100;
        hp = 100;
        combo = 0;
        combo = 0;
        maxcombo = 0;
        perfectCount = 0;
        goodCount = 0;
        badCount = 0;
        missCount = 0;
    }

    public void CountCheck(int Count)
    {
        if (Count == 0)
        {
            perfectCount++;
            Debug.Log("퍼펙트 = " + perfectCount);
        }

        else if (Count == 1)
        {
            goodCount++;
            Debug.Log("굿 = " + goodCount);
        }


        else if (Count == 2)
        {
            badCount++;
            Debug.Log("배드 = " + badCount);
        }
        else if (Count == 3)
        {
            missCount++;
            Debug.Log("미스 = " + missCount);
        }
    }

    public void ComboUse(int plusScore)
    {

        if (combo >= 100)
        {
            combo--;
            FeverTime(plusScore);
        }
        else if (combo == 0)
        {

        }
    }
    IEnumerator FeverTime(int plusScore)
    {
        yield return combo == 0;
        {
            PlusScore(plusScore * 2);
        }
    }
    public void SetPlayerName()
    {
        playerName = PlayerPrefs.GetString("CurrentPlayerName");
    }
}
