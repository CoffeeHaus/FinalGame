using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Door hit");
        if (other.CompareTag("Player")) 
        {
            SceneManager.LoadScene("Level2"); 
        }
    }
}
