// Unique identifier formats:
/// <see href="link">https://learn.microsoft.com/en-us/dotnet/api/system.guid.tostring?view=net-7.0</see>

using System;
using UnityEngine;

[Serializable]
[ExecuteInEditMode]
public class Uid : MonoBehaviour
{
    [ReadOnly, SerializeField] private string _id = Guid.NewGuid().ToString();
    [SerializeField] private static SerializableDict<string, GameObject> idDatastore = new SerializableDict<string, GameObject>();

    public string Id => _id;

    private void OnValidate()
    {
        if (idDatastore.ContainsKey(_id))
        {
            Generate();
        }
        else
        {
            idDatastore.Add(_id, this.gameObject);
        }
    }

    private void OnDestroy()
    {
        if (idDatastore.ContainsKey(_id))
        {
            idDatastore.Remove(_id);
        }
    }

    private void Generate()
    {
        _id = Guid.NewGuid().ToString();
    }
}
