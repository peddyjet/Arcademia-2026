using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public interface IPotion : ICollectible
{

    public string ConsumeMessage { get; }
    public void OnConsumption(PlayerController player);
    public string TargetUUID { get; }
}

