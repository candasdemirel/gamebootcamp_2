using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemController : MonoBehaviour , IReset
{
    [SerializeField] DropItemModel behaviour;
    private int _damage;
    private int _score;
    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        if (behaviour is DamageObject damageObject)
        {
            _damage = (behaviour.dropType == PoolObjectType.bomb) ? -damageObject.damage : damageObject.damage;
        }
        else if (behaviour is ScoreObject scoreObject)
        {
            _score = scoreObject.score;
        }

    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if (behaviour is DamageObject damageObject)
            {
                collision.gameObject.GetComponent<PlayerController>().ChangeHitPoint(_damage);
            }
            else if (behaviour is ScoreObject scoreObject)
            {
                collision.gameObject.GetComponent<PlayerController>().ChangeScore(_score);
            }

        }

        Reset();
        ObjectPooler.Instance.PoolDestroy(behaviour.dropType , this.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "SafeArea")
        {
            Reset();
            ObjectPooler.Instance.PoolDestroy(behaviour.dropType, this.gameObject);
        }
    }

    public void Reset()
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
    }
}

[System.Serializable]
class DropItemModel1
{
    public PoolObjectType dropType;
    public int damage;
    public int score;
}

