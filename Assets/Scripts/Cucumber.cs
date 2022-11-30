using UnityEngine;

public class Cucumber : MonoBehaviour
{
    public void Collect()
    {
        Destroy(gameObject);
        Debug.Log("Collected cucumber");
    }
}
