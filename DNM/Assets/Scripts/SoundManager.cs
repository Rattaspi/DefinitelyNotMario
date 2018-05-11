using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
    public AudioSource sfx, music;

    public AudioClip levelMusic, coin, box, jump, death, star, monster;
    public enum SFX {COIN, BOX, JUMP, DEATH, STAR, MONSTER};

	// Use this for initialization
	void Start () {
        music.clip = levelMusic;
        music.volume = 0.6f;
        music.Play();
        print("play level music");
	}
	
	public void PlaySound(SFX type) {
        switch (type) {
            case SFX.COIN:
                sfx.PlayOneShot(coin, 0.6f);
                break;
            case SFX.BOX:
                sfx.PlayOneShot(box, 0.6f);
                break;
            case SFX.JUMP:
                sfx.PlayOneShot(jump, 0.6f);
                break;
            case SFX.DEATH:
                sfx.PlayOneShot(death, 1.0f);
                break;
            case SFX.STAR:
                sfx.PlayOneShot(star, 1.0f);
                break;
            case SFX.MONSTER:
                sfx.PlayOneShot(monster, 1.0f);
                break;
        }
    }
}
