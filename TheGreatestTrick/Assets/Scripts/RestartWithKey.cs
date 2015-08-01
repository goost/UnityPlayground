using UnityEngine;
using System.Collections;

public class RestartWithKey : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
        if (!Input.GetButtonDown("Jump")) return;
        Application.LoadLevel("Level_01");
	}
}
