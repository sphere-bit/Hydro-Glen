using UnityEngine;

public class CucumberSeeds : MonoBehaviour
{
    public void Collect()
    {
        Destroy(gameObject);
        Debug.Log("Collected cucumber");
    }
}
