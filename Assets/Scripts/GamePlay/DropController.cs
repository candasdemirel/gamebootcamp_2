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
        EventManager.PauseStateEvent += EventManager_PauseEvent;
        //PauseGameState.Subscribe(this, this.gameObject);
        _dropCoroutine = StartCoroutine(SpawnDrop(gameValues.dropPeriod));
    }

    private void OnDisable()
    {
        //PauseGameState.Unsubscribe(this, this.gameObject);
        EventManager.PauseStateEvent -= EventManager_PauseEvent;
        isGameState = false;
        StopCoroutine(_dropCoroutine);
    }


    void EventManager_PauseEvent(bool isPaused)
    {
        if (isPaused)
        {
            StopCoroutine(_dropCoroutine);
        }
        else
        {
            _dropCoroutine = StartCoroutine(SpawnDrop(gameValues.dropPeriod));
        }

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


    /*
    public void Notify(bool isPaused)
    {
        if (isPaused)
        {
            StopCoroutine(_dropCoroutine);
        }
        else
        {
            _dropCoroutine = StartCoroutine(SpawnDrop(gameValues.dropPeriod));
        }
    }*/
}
