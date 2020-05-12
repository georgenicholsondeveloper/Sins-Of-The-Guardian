using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaskSelection : MonoBehaviour
{
    private int abilitySelected = 0, lowerMask = 0, upperMask = 0;
    [SerializeField]
    private Material[] materials;
    [SerializeField]
    private Image[] images;
    [SerializeField]
    private GameObject gameManager;

    public bool abilityRefreshed = true;

    private void Start()
    {
        ChangeImage(); //Set the Mask Selection UI Images.
    }

    void Update()
    {
        if(!GetComponent<DamageScript>().dead && !gameManager.GetComponent<GameManagerScript>().paused)
            SelectAbilities(); //Select the Ability to use.
    }

    void ChangeImage()
    {
        lowerMask = abilitySelected - 1;

        if (lowerMask < 0) //Wrap the images (If the image value is less than 0, Make it's value 4).
            lowerMask = materials.Length-1;
        else if (lowerMask > materials.Length-1) //Wrap the Image the other way (Make the value 0 if it's above 4).
            lowerMask = 0;

        upperMask = abilitySelected + 1;

        if (upperMask > materials.Length-1)
            upperMask = 0;
        else if (upperMask < 0)
            upperMask = materials.Length-1;

        images[1].material = materials[upperMask];
        images[0].material = materials[abilitySelected]; //Set the UI Images accordingly.
        images[2].material = materials[lowerMask];

    }

    void SelectAbilities()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button5)) //If the Right bumper is pressed.
        {
            if (abilitySelected < materials.Length-1) // Cycle through the abilities.
                abilitySelected += 1;                    
            else
                abilitySelected -= materials.Length-1; //Wrap the abilitySelected (If it's the fourth ability and the player presses RB, select the 1st).

            ChangeImage(); //Call the UI function to change the image in game.
        }
        else if (Input.GetKeyDown(KeyCode.Joystick1Button4)) //If the Left bumper is pressed.
        {
            if (abilitySelected > 0)
                abilitySelected -= 1;
            else
                abilitySelected += materials.Length-1;

            ChangeImage();
        }

        if (Input.GetKeyDown(KeyCode.Joystick1Button2) && abilityRefreshed) //If the 'X' button is pressed, and the Ability has Refreshed.
        {
                abilityRefreshed = false;

                switch (abilitySelected) //Switch through the abilitySelected Cases.
                {
                    case 0:
                        ActivateMask(GetComponent<SlothMask>()); //Parse the selected Mask to the ActivateMask function.
                        break;
                    case 1:
                        ActivateMask(GetComponent<WrathMask>());
                        break;
                    case 2:
                        ActivateMask(GetComponent<PrideMask>());
                        break;
                    case 3:
                          ActivateMask(GetComponent<LustMask>());
                        break;
                }                       
        }
    }

    void ActivateMask(Object i)
    {
         i.GetType().GetMethod("ActivateAbility").Invoke(i, new object[] { true }); 
        //Invoke the ActivateAbility method of the MaskScript parsed in.
    }
}
