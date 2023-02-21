using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class E_JudgmentUI : MonoBehaviour
{
    [SerializeField] Image e_JudgmentImg;

    public void ChangeSprite(string name)
    {
        e_JudgmentImg.sprite = Resources.Load<Sprite>($"Image/{name}");
    }
}
