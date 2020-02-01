using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int health;
    public int numHP;

    public Image[] healthPoints;
    public Sprite fullHP;
    public Sprite emptyHP;

    void Update()
    {
        for (int i = 0; i < healthPoints.Length; i++)
        {
            if (i < health)
            {
                healthPoints[i].sprite = fullHP;
            }
            else
            {
                healthPoints[i].sprite = emptyHP;
            }

            if (i < numHP)
            {
                healthPoints[i].enabled = true;
            } else
            {
                healthPoints[i].enabled = false;
            }
        }
    }
}
