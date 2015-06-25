using UnityEngine;
using System.Collections;

public class Restarter : MonoBehaviour {
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player enters trigger");
        other.gameObject.transform.position = new Vector3(0, 0.5f, 0);
        other.gameObject.GetComponent<Rigidbody>().Sleep();
        //Application.LoadLevel("miniGame");
    }
}
