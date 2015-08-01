using UnityEngine;

public class CoinPickUp : MonoBehaviour
{
    //SoundEffect to play on PickUp
    [SerializeField] private GameObject _effect;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        GameController.Instance.IncrementCoinAndChangeUI();
        Logger.Log(Debug.Log, "Player gets Coin");
        Destroy(gameObject);
        Instantiate(_effect,transform.position,Quaternion.identity);
    }
}
