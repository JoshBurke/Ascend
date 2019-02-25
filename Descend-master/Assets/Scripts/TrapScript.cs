﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapScript : MonoBehaviour, ISavable {
    bool activated;
    public Animator anim;
    public AudioSource trapAudio;

    // Use this for initialization
    void Start()
    {
        activated = false;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (!activated)
        {
            Debug.Log("trap collision: " + collider.gameObject.name);
            if (collider.gameObject.tag == "Player" || collider.gameObject.tag == "Enemy")
            {
                activated = true;
                if(collider.gameObject.tag == "Player") {
                    PlayerController player = collider.gameObject.GetComponent<PlayerController>();
                    player.Kill();
                } else if(collider.gameObject.tag == "Enemy"){
                    EnemyController enemy = collider.gameObject.GetComponent<EnemyController>();
                    enemy.Kill();
                }
                anim.Play("trap_close");
                trapAudio.Play();
            }
        }
        else {
            anim.Play("trap_open");
        }
    }

    public void OnSave(ISavableWriteStore store) {
        store.WriteBool("activated", activated);
    }

    public void OnLoad(ISavableReadStore store) {
        activated = store.ReadBool("activated");

        if(!activated)
            anim.Play("trap_open");
        else
            anim.Play("trap_close");
    }
}