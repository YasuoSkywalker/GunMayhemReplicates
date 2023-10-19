using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testAttack : MonoBehaviour
{
    private weapon weaponComponent;
    private SoundManager soundManager;

    private void Start()
    {
        weaponComponent = transform.Find("weapon").GetComponent<weapon>();
        soundManager = transform.Find("soundManager").GetComponent<SoundManager>();
    }

    private void FixedUpdate()
    {
        onAttack();
    }

    void onAttack()
    {
        if (Input.GetKey(KeyCode.J))
        {
            weaponComponent.onAttack();
        }
        if(Input.GetKey(KeyCode.K))
        {
            soundManager.playAudio("shoot");
        }
    }    
}
