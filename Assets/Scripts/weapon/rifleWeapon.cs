using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class rifleWeapon : weapon
{
    protected SpriteResolver gunResolver;

    protected override void Start()
    {
        base.Start();
        gunResolver = transform.Find("image").GetComponent<SpriteResolver>();
        gunResolver.SetCategoryAndLabel("image", "2");
        //soundManager = GetComponent<SoundManager>();
    }

    protected void FixedUpdate()
    {
        if (!Input.GetKey(KeyCode.J))
        {
            if(soundManager.audioSource.isPlaying)
            {
                soundManager.audioSource.Stop();
            }
            anim.SetBool("isAttacking", false);
            gunResolver.SetCategoryAndLabel("image","2");
        }
    }

    public override void onAttack()
    {
        if (canAttack && bulletNowNumber > 0)
        {
            anim.SetBool("isAttacking", true);
            gunResolver.SetCategoryAndLabel("image", "1");

            if (!soundManager.audioSource.isPlaying)
            {
                soundManager.playAudio("shoot");
            }

            createEffect();
            if (bulletNowNumber > 0)
            {
                StartCoroutine("attackCD");
            }
            else if (isBase)
            {
                anim.SetBool("isChanging", true);
            }
            else
            {
                anim.SetBool("isAttacking", false);
                if (soundManager.audioSource.isPlaying)
                {
                    soundManager.audioSource.Stop();
                }
                //¶ªÆúÎäÆ÷
                transform.parent.GetComponent<PlayerBehave>().setBaseWeapon();
            }
        }
    }
}
