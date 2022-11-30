using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField]
    private Image _healthBar;
    [SerializeField]
    private float _healthRem = 100;
    [SerializeField]
    private float _healthMax = 100;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
        if (_healthRem <= 0)
        {
            Application.LoadLevel(Application.loadedLevel);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            playerDamage(10);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            playerHeal(10);
        }
    }

    public void playerDamage(float damage)
    {
        _healthRem -= damage;
        _healthBar.fillAmount = _healthRem / _healthMax;
    }

    public void playerHeal(float restore)
    {
        _healthRem += restore;
        _healthRem = Mathf.Clamp(_healthRem, 0, _healthMax);
        _healthBar.fillAmount = _healthRem / _healthMax;
    }
}
