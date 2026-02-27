using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Enemies
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
    public class Enemy : MonoBehaviour
    {
        [SerializeField] protected float _gracePeriod = 0.5f;

        protected virtual void Start()
        {
            StartCoroutine(GracePeriod());
        }

        protected IEnumerator<WaitForSeconds> GracePeriod()
        {
            var collider = GetComponent<Collider2D>();
            collider.enabled = false;
            yield return new WaitForSeconds(_gracePeriod);
            collider.enabled = true;
        }
    }
}
