using UnityEngine;

public abstract class AsynchronousEvent : MonoBehaviour, IAsynchronous
{
    private int asynchronous;

    public void AddControlFinishWork()
    {
        asynchronous++;
        FinishWork();
    }

    private void FinishWork()
    {
        if (asynchronous >= 2)
        {
            asynchronous = 0;
            FinishAsynchronousWork();
        }
    }

    public void SendAsynchronous(Collider otherCollider)
    {
        otherCollider.GetComponent<AsynchronousEvent>()?.AddControlFinishWork();
    }

    protected void ControlFinishWork(Collider otherCollider)
    {
        if (otherCollider.GetComponent<IAsynchronous>() == null)
        {
            asynchronous++;
        }

        SendAsynchronous(otherCollider);
        AddControlFinishWork();
    }

    private void OnDisable()
    {
        asynchronous = 0;
    }

    protected abstract void FinishAsynchronousWork();
}