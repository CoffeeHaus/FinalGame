using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activation : MonoBehaviour
{
    public Skeleton[] monsters; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player")) 
        {
            Debug.Log("activation");
            foreach (var monster in monsters)
            {
                if (monster != null)
                {
                    monster.isActive = true; 
                }
            }
        }
    }
}
