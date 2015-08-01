using UnityEngine;
using System.Collections;

public class DestroyAfterTime : MonoBehaviour {

    [SerializeField] private int _lifeTime;

	
	void Awake () {
        Invoke("Explode", _lifeTime);
	}

    private void Explode()
    {
        Destroy(gameObject);
    }
}
