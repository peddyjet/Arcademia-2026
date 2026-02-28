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
        [SerializeField] protected float _iframes = 0.5f;

        protected virtual void OnBlessing()
        {
            var rb = GetComponent<Rigidbody2D>();
            rb.AddForce(new Vector2(UnityEngine.Random.Range(-3f, 3f), UnityEngine.Random.Range(-3f, 3f)).normalized * 5f, ForceMode2D.Impulse);
        }

        protected virtual void Start()
        {
            FindFirstObjectByType<PlayerController>().OnBlessingUsed.AddListener(OnBlessing);
            StartCoroutine(GracePeriod());
        }

        protected IEnumerator<WaitForSeconds> GracePeriod()
        {
            var sprite = GetComponent<SpriteRenderer>();
            tag = "Untagged";
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0.5f);
            yield return new WaitForSeconds(_iframes / 3);
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1f);
            yield return new WaitForSeconds(_iframes / 3);
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0.5f);
            yield return new WaitForSeconds(_iframes / 3);
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1f);
            tag = "Enemy";
        }
    }
}
