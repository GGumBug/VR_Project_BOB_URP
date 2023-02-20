using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public enum State
{
    Playing,
    Paused,
    Unpaused,
    Stop,
}
public class Sound
{
    public string name;
    public AudioClip clip;

    public float volume;
    public bool loop = true;


    public Sound(string _name, AudioClip _clip, bool _loop)
    {
        name = _name;
        clip = _clip;
        volume = 0.3f;
        loop = _loop;
    }
}

public class AudioManager : MonoBehaviour
{

    #region SingletoneMake
    public static AudioManager instance = null;
    public static AudioManager GetInstance()
    {
        if (instance == null)
        {
            GameObject go = new GameObject("@AudioManager");
            instance = go.AddComponent<AudioManager>();

            DontDestroyOnLoad(go);
        }
        return instance;
    }
    #endregion

    public State state = State.Stop;

    public AudioSource BgmPlayer;
    public AudioSource SfxPlayer;
    public AudioSource leftAudio;
    public AudioSource rightAudio;
    public Dictionary<string, Sound> bgms = new Dictionary<string, Sound>();
    public Dictionary<string, Sound> sfxs = new Dictionary<string, Sound>();
    public float curBgmTime;

    private void Awake()
    {
        var ob1 = new GameObject();
        ob1.name = "@BgmPlayer";
        var ob2 = new GameObject();
        ob2.name = "@SfxPlayer";
        ob1.transform.SetParent(gameObject.transform);
        ob2.transform.SetParent(gameObject.transform);
        ob1.AddComponent<AudioSource>();
        ob2.AddComponent<AudioSource>();
        BgmPlayer = ob1.GetComponent<AudioSource>();
        SfxPlayer = ob2.GetComponent<AudioSource>();
        InitClip();
    }
    public void InitClip()
    {
        AudioClip[] bgm = Resources.LoadAll<AudioClip>($"Sound/Bgm");
        AudioClip[] sfx = Resources.LoadAll<AudioClip>($"Sound/Sfx");

        for (int i = 0; i < bgm.Length; i++)
        {
            bgms.Add(bgm[i].name, new Sound(bgm[i].name, bgm[i], false));
        }

        for (int j = 0; j < sfx.Length; j++)
        {
            sfxs.Add(sfx[j].name, new Sound(sfx[j].name, sfx[j], false));
        }

    }
    /*   public void InitClip(string title)
       {
           BgmPlayer = gameObject.AddComponent<AudioSource>();
           BgmPlayer.clip = SheetManager.GetInstance().sheets[title].clip;
       }*/
    public void PlayGameBgm(string title)
    {
        BgmPlayer.clip = SheetManager.GetInstance().sheets[title].clip;
        state = State.Playing;
        BgmPlayer.volume = 1;
        BgmPlayer.Play();
    }
    public void PlayBgm(string name)
    {
        var bgm = bgms[name];
        BgmPlayer.clip = bgm.clip;
        state = State.Playing;
        BgmPlayer.volume = 1;
        BgmPlayer.Play();
    }
    public void PlaySfx(string name)
    {
        var sfx = sfxs[name];
        SfxPlayer.clip = sfx.clip;
        SfxPlayer.volume = 1;
        SfxPlayer.loop = sfx.loop;
        SfxPlayer.Play();
    }

    public float progressTime
    {
        get
        {
            float time = 0f;
            if (BgmPlayer.clip != null)
                time = BgmPlayer.time;
            return time;
        }
        set
        {
            if (BgmPlayer.clip != null)
                BgmPlayer.time = value;
        }
    }

    public float GetMilliSec()
    {
        return BgmPlayer.time * 1000;
    }

    public void FadeOutBGM()
    {
        StartCoroutine("IEFadeOutBGM");
    }

    IEnumerator IEFadeOutBGM()
    {
        while (BgmPlayer.volume > 0)
        {
            BgmPlayer.volume -= 0.003f;
            yield return null;
        }

        if (BgmPlayer.volume == 0)
        {
            BgmPlayer.Stop();
        }

    }

    public void FindGunAudio()
    {
        leftAudio = GameObject.FindGameObjectWithTag("LeftGun").GetComponent<GunFire>().FindGunAudioSource();
        rightAudio = GameObject.FindGameObjectWithTag("RightGun").GetComponent<GunFire>().FindGunAudioSource();
    }

    public void GetBGMTime()
    {
        curBgmTime = BgmPlayer.time;
    }

    public void ReturnBGM()
    {
        PlayBgm("Hole In The Sun");
        BgmPlayer.time = curBgmTime;
    }

}
