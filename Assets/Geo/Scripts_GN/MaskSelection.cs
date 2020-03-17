using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskSelection : MonoBehaviour
{
    private int abilitySelected = 1;

    void Update()
    {
        SelectAbilities();
    }

    void SelectAbilities()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button5) || Input.GetKeyDown(KeyCode.E))
        {
            if (abilitySelected < 4)
                abilitySelected += 1;
            else
                abilitySelected -= 3;
        }
        else if (Input.GetKeyDown(KeyCode.Joystick1Button4) || Input.GetKeyDown(KeyCode.Q))
        {
            if (abilitySelected > 1)
                abilitySelected -= 1;
            else
                abilitySelected += 3;
        }

        if (Input.GetKeyDown(KeyCode.Joystick1Button2) || Input.GetKeyDown(KeyCode.F))
        {
            switch (abilitySelected)
            {
                case 1:
                    ActivateMask(FindObjectOfType<SlothMask>());
                    break;
                case 2:
                    ActivateMask(FindObjectOfType<WrathMask>());
                    break;
                case 3:
                    ActivateMask(FindObjectOfType<PrideMask>());
                    break;
                case 4:
                  //  ActivateMask(FindObjectOfType<LustMask>());
                    break;
            }
        }
    }

    void ActivateMask(Object i)
    {
         i.GetType().GetMethod("ActivateAbility").Invoke(i, new object[] { true });    
    }
}
