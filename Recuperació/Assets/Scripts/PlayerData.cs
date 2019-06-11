using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlayerData{

    public int level;
    public int lastLevelPlayed;
    public bool music;
    public float volume;
    public bool fullscreen;
    public int [] resolution;

    public PlayerData(GameControl gameControl)
    {
        level = gameControl.level;
        lastLevelPlayed = gameControl.lastLevelPlayed;
        music = gameControl.music;
        volume = gameControl.volume;
        fullscreen = gameControl.fullscreen;
        resolution = gameControl.resolution;
    }
}
