using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class SkillManager : MonoBehaviour
{
    public List<Ability> abilities; // makes a list for the abilities
    
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Q)) //PLACEHOLDER KEYS, MAKE JORDON CHANGE THEM.
        {
            abilities[0].Activate();
            // activates dash
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            abilities[1].Activate();
            // activates AOE
        }
        */
    }

    public void OnInteract(InputValue value)
    {
        if (!value.isPressed) return;

        abilities[0].Activate(gameObject);
    }
}
