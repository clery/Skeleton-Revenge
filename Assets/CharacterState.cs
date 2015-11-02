using UnityEngine;
using System.Collections;

public class CharacterState : MonoBehaviour {

    public int maxHP;
    private bool isDead = false;
    public bool IsDead
    {
        get
        {
            return (isDead);
        }
        private set
        {
            isDead = value;
        }
    }
    private int HP;
    private Animator animator;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        HP = maxHP;
    }

    public void ReceiveAttack(Skill skill)
    {
        ChangeHP(skill.damages);
    }

    void Die()
    {
        if (!IsDead)
        {
            HP = 0;
            IsDead = true;
            animator.SetTrigger("Death");
            foreach (Collider collider in GetComponentsInChildren<Collider>())
                collider.gameObject.layer = LayerMask.NameToLayer("Dead");
        }
    }

    void ChangeHP(int modifier)
    {
        Debug.Log("Monster has " + HP + " HP");
        HP += modifier;
        if (HP > maxHP)
            HP = maxHP;
        else if (HP <= 0)
            Die();
        Debug.Log("Modifier = " + modifier);
        Debug.Log("Monster " + gameObject.name + " has " + HP + " HP over " + maxHP);
    }
}
