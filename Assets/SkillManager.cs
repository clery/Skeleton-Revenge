using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkillManager : MonoBehaviour {
    
    [System.Serializable]
    public class SkillPair
    {
        public string ButtonName;
        public Skill Skill;
        public AudioClip AudioClip;
    }

    public SkillPair[] skills = null;
    private float cooldownTime = 0;
    private Animator animator = null;
    private AudioSource effectPlayer = null;

    // Use this for initialization
    void Start () {
        if (skills == null)
            Debug.LogWarning("Please set skills in the SkillManager");
        animator = GetComponentInChildren<Animator>();
        foreach (AudioSource source in GetComponentsInChildren<AudioSource>())
            if (source.clip == null)
            {
                effectPlayer = source;
                break;
            }
	}
	
	// Update is called once per frame
	void Update () {
        if (CanAttack())
            for (int i = 0; i < skills.Length; ++i)
            {
                if (Input.GetButtonDown(skills[i].ButtonName))
                {
                    skills[i].Skill.Launch();
                    animator.SetTrigger(skills[i].ButtonName);
                    cooldownTime = skills[i].Skill.cooldown;
                    effectPlayer.clip = skills[i].AudioClip;
                    effectPlayer.Play();
                }
            }
	}

    bool CanAttack()
    {
        return (true);
    }
}
