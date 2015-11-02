using UnityEngine;
using System.Collections;

public class ShortRangeAttack : Skill {

    public SkillTemplate template = null;

    void Start()
    {
        if (template == null)
            Debug.LogWarning("Please set a template with the ShortRangeAttack skill");
    }

    override public void Launch()
    {
        template.colliders.Remove(null);
        foreach (Collider collider in template.colliders)
        {
            if (collider.tag == "Breakable")
                collider.GetComponent<Breakable>().Break();
            if (collider.tag == "Monster")
                collider.GetComponent<CharacterState>().ReceiveAttack(this);
        }
    }
}
