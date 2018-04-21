using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public Vector2 direction;
    [Range(0, 10)]
    public float speed;
    private Transform objective;
    private Node nodeObj;

    private int _aux;
    private float _range;

    public delegate void ChangeDirection(Vector2 dir);
    public event ChangeDirection OnChangeDirection;

    private void Awake()
    {
        SetObjectivenInit();
    }

    private void Start()
    {
        SetDirection();
    }

    private void SetObjectivenInit()
    {
        float distMin = Mathf.Infinity;
        Vector2 diff;

        foreach (Transform tr in GameObject.Find("Path").transform)
        {
            diff = transform.position - tr.position;
            if (diff.magnitude < distMin)
            {
                distMin = diff.magnitude;
                objective = tr;
                nodeObj = tr.GetComponent<Node>();
                _range = Random.Range(0, nodeObj.range);
            }
        }
    }

    private void SetDirection()
    {
        direction = (objective.position - transform.position).normalized;
        OnChangeDirection?.Invoke(direction);
    }

    // Update is called once per frame
    void Update ()
    {
        transform.Translate(direction * Time.deltaTime * speed);
        if (Arrived())
        {
            ChangeObjective();
        }
    }

    private bool Arrived()
    {
        return (transform.position - objective.position).magnitude < _range;
    }

    private void ChangeObjective()
    {
        _aux = Random.Range(0, nodeObj.neighboordNodes.Length);
        objective = nodeObj.neighboordNodes[_aux];
        nodeObj = objective.GetComponent<Node>();
        _range = Random.Range(0, nodeObj.range);
        SetDirection();
    }
}
