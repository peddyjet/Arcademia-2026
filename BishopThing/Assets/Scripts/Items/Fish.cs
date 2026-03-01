using System.Collections;
using UnityEngine;
using UnityEngine.UI;

class Fish : MonoBehaviour
{
    [SerializeField] Slider _aimSlider;
    [SerializeField] GameObject _fishingMiniGame;
    [SerializeField] GameObject _reward;

    public bool IsFishing => _fishingMiniGame.activeInHierarchy;
    private Coroutine _jigglePhysics;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        StartFishingGame();
    }

    private void StartFishingGame()
    {
        _aimSlider.value = 0;
        _fishingMiniGame.SetActive(true);
        _jigglePhysics = StartCoroutine(JiggleSlider());

    }

    IEnumerator JiggleSlider()
    {
        while (true) {
            _aimSlider.value += Random.Range(-0.05f, 0.1f);
            if (_aimSlider.value > 1) { _aimSlider.value = 0; }
            if (_aimSlider.value < 0) { _aimSlider.value = 0; }
            yield return new WaitForSeconds(0.15f);
        }
    }

    public void Reel()
    {
        // If you are a personal failure
        if(!IsFishing || !(_aimSlider.value > 0.4 && _aimSlider.value < 0.6))
        {
            _aimSlider.value = 0;
            return;
        }

        StopCoroutine(_jigglePhysics);
        _fishingMiniGame.SetActive(false);
        Instantiate(_reward, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }
}