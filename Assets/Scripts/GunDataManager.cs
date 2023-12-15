using UnityEngine;

public class GunDataManager : MonoBehaviour
{
    private static GunDataManager _instance;
    public GunData gunData;
    public GameObject gunObject;

    public static GunDataManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GunDataManager>();
                if (_instance == null)
                {
                    GameObject go = new GameObject("GunDataManager");
                    _instance = go.AddComponent<GunDataManager>();
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(gunData);
            DontDestroyOnLoad(gunObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
