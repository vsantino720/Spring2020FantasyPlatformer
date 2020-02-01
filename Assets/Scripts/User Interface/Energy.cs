using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Energy : MonoBehaviour
{
    public int energy;
    public int numEP;

    public Image[] energyPoints;
    public Sprite fullEP;
    public Sprite emptyEP;

    void Update()
    {
        for (int i = 0; i < energyPoints.Length; i++)
        {
            if (i < energy)
            {
                energyPoints[i].sprite = fullEP;
            }
            else
            {
                energyPoints[i].sprite = emptyEP;
            }

            if (i < numEP)
            {
                energyPoints[i].enabled = true;
            }
            else
            {
                energyPoints[i].enabled = false;
            }
        }
    }
}
