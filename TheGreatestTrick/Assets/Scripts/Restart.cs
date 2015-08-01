using UnityEngine;

public class Restart : MonoBehaviour {


    public void OnTriggerEnter2D(Collider2D collision)
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}
