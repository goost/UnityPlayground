using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    //NOTE(goost) Only one, easy access. Do not abuse this pattern
    public static GameController Instance;

    [SerializeField] private Image _coinCounterImage;
    [SerializeField] private Sprite[] _coinImages;
    private int _curCoin;


    //All Awakes before Start ->Only access Instance in Start and later and all is fine
	void Awake () {
	    Instance = this;
	    _curCoin = 0;
	}

    void Start()
    {
        SetCoinUI();
    }

    public void IncrementCoinAndChangeUI()
    {
        _curCoin++;
        SetCoinUI();
    }

    public void DiamondPickUp()
    {
        Application.LoadLevel("TheEnd");
    }

    private void SetCoinUI()
    {
        _coinCounterImage.sprite = _coinImages[_curCoin];
    }
	
	
}
