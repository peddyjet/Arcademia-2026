using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Items
{
    public class UILink : MonoBehaviour
    {
        [SerializeField] private string _uuid;
        public string UUID => _uuid;
    }
}
