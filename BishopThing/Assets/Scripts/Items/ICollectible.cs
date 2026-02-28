using UnityEngine;

public interface ICollectible
{
    public string Message { get; }
    public void Collect();
}
