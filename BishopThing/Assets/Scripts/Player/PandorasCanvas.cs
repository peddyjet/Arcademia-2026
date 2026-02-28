using System.Collections;
using TMPro;
using UnityEngine;

public class PandorasCanvas : MonoBehaviour
{
    [SerializeField] GameObject _boxAsset;
    [SerializeField] TextMeshProUGUI _text;
    [SerializeField] float _speed;
    [SerializeField] float _movementSpeed;
    [SerializeField] float _verticalOffset = 0.2f;

    private void Start()
    {
        _text.gameObject.SetActive(false);
    }

    private IEnumerator ShowMessageAsync(string message)
    {
        float currentElevation = _verticalOffset;

        _text.gameObject.SetActive(true);
        transform.position = _boxAsset.transform.position;
        _text.text = message;
        _text.color = new Color(_text.color.r, _text.color.g, _text.color.b, 0);
        while (_text.color.a < 1f)
        {
            yield return new WaitForEndOfFrame();
            Color c = _text.color;
            c.a = Mathf.MoveTowards(c.a, 1f, _speed * Time.deltaTime);
            _text.color = c;
            currentElevation += _movementSpeed / 10 * Time.deltaTime;
            transform.position = new Vector2(_boxAsset.transform.position.x, _boxAsset.transform.position.y + currentElevation);
        }
        yield return new WaitForEndOfFrame();
        _text.gameObject.SetActive(false);
    }

    public void ShowMessage(string message) => StartCoroutine(ShowMessageAsync(message));
}