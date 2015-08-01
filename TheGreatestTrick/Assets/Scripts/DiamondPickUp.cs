using UnityEngine;
using System.Collections;

public class DiamondPickUp : MonoBehaviour {

    [SerializeField] private GameObject _effect;

    //NOTE(goost) Duplicated Code, alomost identical to CoinPickUp
    //Not refactored due to time....
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        GameController.Instance.DiamondPickUp();
        Logger.Log(Debug.Log, "Player gets Diamond");
        Destroy(gameObject);
        Instantiate(_effect, transform.position, Quaternion.identity);
    }
}
