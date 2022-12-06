using UnityEngine;

public class PersistenceUIController : MonoBehaviour
{
    public GameObject PersistenceMenu;

    private void Awake()
    {
        PersistenceMenu.gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        SavePoint.OnPersistentMenuRequested += DisplayPersistentMenu;
    }
    private void OnDisable()
    {
        SavePoint.OnPersistentMenuRequested -= DisplayPersistentMenu;
    }

    void DisplayPersistentMenu()
    {
        PersistenceMenu.gameObject.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && PersistenceMenu.gameObject.activeInHierarchy)
        {
            PersistenceMenu.gameObject.SetActive(false);
        }
    }
}