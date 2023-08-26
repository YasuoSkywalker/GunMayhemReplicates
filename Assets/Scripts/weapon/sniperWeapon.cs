using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sniperWeapon : weapon
{
    protected override void Start()
    {
        canAttack = true;
        bulletNowNumber = bulletTotalNumber;
        anim = GetComponentInChildren<Animator>();
        fireSpark.SetActive(false);
        slotParticle = transform.Find("slotParticle").GetComponent<ParticleSystem>();
        soundManager = GetComponent<SoundManager>();
    }

    public override void onAttack()
    {
        if (canAttack && bulletNowNumber > 0 && !anim.GetBool("isAttacking"))
        {
            soundManager.playAudio("shoot");
            StartCoroutine("attackCD");
        }
    }

    protected override IEnumerator attackCD()
    {
        anim.SetBool("isAttacking", true);
        canAttack = false;
        yield return new WaitForSeconds(attackSpacing);
    }

    public override void exitChanging()
    {
        if (bulletNowNumber > 0)
        {
            anim.SetBool("isAttacking", false);
            canAttack = true;
        }
        else if (isBase)
        {
            anim.SetBool("isChanging", true);
        }
        else
        {
            //丢弃武器
            transform.parent.GetComponent<PlayerBehave>().setBaseWeapon();
        }
    }
}
