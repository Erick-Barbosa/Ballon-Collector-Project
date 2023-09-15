using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private float xLimit;
    [SerializeField] private float yLimit;
    [SerializeField] private float gameCoinSpawnLimit = 20;

    public GameObject coin;
    private int spawnedCoinsAmount = 0;

    IEnumerator Start() {
        yield return new WaitForSeconds(Random.Range(1.5f, 4.5f));
        Vector3 randomPosition = new Vector3(Random.Range(xLimit, -xLimit), Random.Range(yLimit, -yLimit), 0f);
        Instantiate(coin, randomPosition, transform.rotation);

        spawnedCoinsAmount++;

        if (spawnedCoinsAmount >= gameCoinSpawnLimit) {
            spawnedCoinsAmount = 0;
            SceneManager.LoadScene("StartScene");
        }

        StartCoroutine(Start());
    }
}
