using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack
{
    public Animator anim;
    public void StartAttack()
    {
        anim.SetBool("Attack", true);
            
    }
    public void EndAttack()
    {
        anim.SetBool("Attack", false);
    }
}
