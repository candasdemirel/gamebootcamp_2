using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropController : MonoBehaviour
{

    [SerializeField] private PoolObjectType[] _dropGameObjects;

    private Coroutine _dropCoroutine;
    private GameValues gameValues;

    private bool isGameState;

    private void OnEnable()
    {
        isGameState = true;
        gameValues = GameValues.Instance;
        _dropCoroutine = StartCoroutine(SpawnDrop(gameValues.dropPeriod));
    }

    private void OnDisable()
    {
        isGameState = false;
        StopCoroutine(_dropCoroutine);
    }



    private IEnumerator SpawnDrop(float spawnPerid)
    {
       
        WaitForSeconds wait = new WaitForSeconds(spawnPerid);

        while (true)
        {
            //Instantiate(_dropGameObjects[Random.Range(0, _dropGameObjects.Length)],GetRandomSpawnPosition(), Quaternion.identity);
            ObjectPooler.Instance.SpawnFromPool(_dropGameObjects[Random.Range(0, _dropGameObjects.Length)], GetRandomSpawnPosition(), Quaternion.identity);
            yield return wait;

          
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        return new Vector3(Random.Range(-gameValues.x_boundary, gameValues.x_boundary), gameValues.dropHeight ,0);
    }


}
