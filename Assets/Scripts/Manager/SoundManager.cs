using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoSingleton<SoundManager>
{
    public AudioSource BGMPlayer;
    public AudioSource effectPlayer;

    //[Header] 
    //public 

    public AudioClip[] containSound;
    public enum STATE
    {
        NONE,
        RIFLE,
        SNIPER,
        SHOTGUN,
        GRANADE
    }
    public STATE state = STATE.RIFLE;
    public enum SCENE
    {
        NONE,
        TOWN,
        DUNGEON
    }
    public SCENE scene = SCENE.TOWN;
    public Dictionary<SCENE, AudioClip> bgmDict;
    public Dictionary<STATE, AudioClip> effectDict;

    private void Start()
    {
    }

    public void PlayEffect(STATE _state)
    {
        if (state != _state)
            effectPlayer.clip = effectDict[_state];
        effectPlayer.Play();
    }
    public void PlayBGM(SCENE _scene)
    {
        if (_scene != scene)
            BGMPlayer.clip = bgmDict[_scene];
        BGMPlayer.Play();
    }
}
