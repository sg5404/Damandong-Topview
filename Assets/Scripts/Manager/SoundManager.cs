using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoSingleton<SoundManager>
{
    public AudioSource BGMPlayer;
    public AudioSource LefteffectPlayer;
    public AudioSource RighteffectPlayer;

    public AudioSource[] effectPlayer;
    [Header("Set Name : 0 TownBGM / 1 DungeonBGM")]
    public AudioClip[] BGMContainSound;
    [Header("0 Rifle Sound / 1 Sniper Sound / 2 ShotGun Sound / 3 Granade Sound")]
    public AudioClip[] effectContainSound;
    public enum SCENE
    {
        NONE,
        TOWN = 0,
        DUNGEON
    }
    public enum STATE
    {   
        RIFLE = 0,
        SNIPER,
        SHOTGUN,
        GRANADE
    }
    public enum FIRE
    {
        LEFT,
        RIGHT
    }
    public SCENE scene = SCENE.TOWN;
    private void Awake()
    {
        effectPlayer[0] = LefteffectPlayer;
        effectPlayer[1] = RighteffectPlayer;
    }
    public void SetEnum()
    {
        scene = gameObject.CompareTag("Town") ? SCENE.TOWN : SCENE.DUNGEON;
        BGMSoundPlay(scene);
    }

    public void EffectSoundPlay(STATE _state, FIRE fire)
    {
        Debug.Log("State : " + (int)_state + " / Fire : " + (int)fire);
        Debug.Log(effectContainSound[(int)_state].name);
        effectPlayer[(int)fire].clip = effectContainSound[(int)_state];
        effectPlayer[(int)fire].Play();
    }
    public void BGMSoundPlay(SCENE _scene)
    {
        BGMPlayer.clip = BGMContainSound[(int)_scene];
        BGMPlayer.Play();
    }
}
