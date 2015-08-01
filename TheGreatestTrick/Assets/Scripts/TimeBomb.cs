using UnityEngine;
using System.Collections;

public class TimeBomb : MonoBehaviour
{
    [SerializeField] private int _maxTimer;
    //all the sprites which the platform is made of
    private SpriteRenderer[] _sprites;
    private int _curTimer;

    void Start()
    {
        _sprites = transform.parent.GetComponentsInChildren<SpriteRenderer>();
        _curTimer = 1;  
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;
        Logger.Log(Debug.Log, "StartCourtine");
        StartCoroutine(Tick());
    }

    private IEnumerator Tick()
    {
        while (_curTimer <= _maxTimer)
        {
            Logger.Log(Debug.Log, "CurTimer:" + _curTimer);
            foreach (var spriteRenderer in _sprites)
            {
                spriteRenderer.color = Color.Lerp(Color.white, Color.red, _curTimer / (float)_maxTimer);
            }
            _curTimer++;
            yield return new WaitForSeconds(1);
        }
        Destroy(transform.parent.gameObject);
    } 
}
