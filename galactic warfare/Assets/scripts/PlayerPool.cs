using UnityEngine;

public class PlayerPool : MonoBehaviour
{
    public static PlayerPool Instance;

    public GameObject playerPrefab;
    private GameObject playerInstance;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        SpawnPlayer();
    }

    public void SpawnPlayer()
    {
        if (playerInstance == null)
        {
            playerInstance = Instantiate(playerPrefab);
        }

        playerInstance.transform.position = Vector3.zero;
        playerInstance.SetActive(true);
    }

    public void DespawnPlayer()
    {
        if (playerInstance != null)
        {
            playerInstance.SetActive(false);
        }
    }
}
