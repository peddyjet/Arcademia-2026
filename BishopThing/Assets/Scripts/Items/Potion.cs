using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public struct Potion
{
    public string ConsumeMessage { get; set; }
    public string TargetUUID { get; set; }
    public System.Action<PlayerController> OnConsumption { get; set; }
}