using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarScript : MonoBehaviour, IHealth
{
    enum building_states { Idle, Attack, Death }
    int DPS = 100;
    float attack_time_interval = 0.5f;
    float attack_timer;
    float minAttackDistance = 8.0f;
    float maxAttackRange = 20.0f;
    building_states my_state = building_states.Idle;

    private int MHP = 1000, CHP = 1000, _level = 0;
    CharacterScript current_target;
{
get { return _level + 1; }
    set
        {
        if (current_active_model) current_active_model.SetActive(false);
        _level = value - 1;
        current_active_model = all_levels[_level];
        current_active_model.SetActive(true);
        myRenderer = current_active_model.GetComponent<Renderer>();
        }
}

   

    List<GameObject> all_levels;
GameObject current_active_model;

    // Start is called before the first frame update
    void Start()
    {
        all_levels = new List<GameObject>();
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            child.SetActive(false);
            all_levels.Add(child);

        }

        Level = 1;
    }

// Update is called once per frame
void Update()
    {
    switch (my_state)
    {

        case building_states.Idle:

            if ((current_target) && within_attack_range(current_target))
            {
                my_state = building_states.Attack;
                attack_timer = 0;
            }

            break;

        case building_states.Attack:

            if (attack_timer <= 0f)
            {
                current_target.takeDamage((int)((float)DPS * attack_time_interval));
                attack_timer = attack_time_interval;

                while (within_attack_range(current_target))
                {
                    Vector3 from_me_to_Character = current_target.transform.position - transform.position;
                    Vector3 direction = from_me_to_Character.normalized;
                    transform.forward = direction;
                }
            }

            attack_timer -= Time.deltaTime;

            break;


        case building_states.Death:


            break;

    }
}

    private bool within_range(CharacterScript current_target)
    {
        throw new NotImplementedException();
    }

    public void takeDamage(int how_much_damage)
    {
        myRenderer.material.color = Color.blue;
        CHP -= how_much_damage;
        if (CHP <= 0)
        {
            destroyed = true;
            myRenderer.material.color = Color.red;
        }
    }
    internal void levelUp()
    {

        Level++;

    }

    public void repair(int how_much_heal)
    {
        CHP += how_much_heal;
        if (CHP > MHP)
        {
            CHP = MHP;
            myRenderer.material.color = Color.white;
        }
    }
private bool within_attack_range(CharacterScript current_target)
{
    return (Vector3.Distance(transform.position, current_target.transform.position) <= minAttackDistance && >= maxAttackDistance);

}

internal void ImtheMan(Manager manager)
    {
        theManager = manager;
    }
}
