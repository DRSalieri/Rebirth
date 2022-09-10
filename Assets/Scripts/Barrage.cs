using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Barrage : MonoBehaviour
{
    public GameObject barragePrefab;
    public GameObject barragePanel;
    public List<Transform> _barrages;

    // Start is called before the first frame update
    void Start()
    {
        _barrages = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (_barrages != null) {
            foreach(Transform b in _barrages) {
                b.position += new Vector3(-3, 0, 0);         
            }
        }
    }

    void Clear() {
        _barrages = null;
        foreach(Transform c in barragePanel.transform) {
            Destroy(c.gameObject);
        }
    }

    IEnumerator waitToClear() {
        yield return new WaitForSeconds(5);
        Clear();
    }

    public void TriggerBarrage(List<string> barrages) {
        if (_barrages == null) {
            _barrages = new List<Transform>();

            for (int i = 0; i < barrages.Count; ++i) {
                Instantiate(barragePrefab, new Vector3(1300 + i / 7 * 600 + Random.Range(-40, 40), (7 - i % 7) * 100 + Random.Range(-20, 20), 0), Quaternion.identity, barragePanel.transform);
            }
            int index = 0;
            foreach (Transform t in barragePanel.transform) {
                TextMeshProUGUI text = t.gameObject.GetComponent<TextMeshProUGUI>();
                text.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f),Random.Range(0f, 1f), 1);
                text.text = barrages[index];
                ++index;
                _barrages.Add(t);
            }
            StartCoroutine(waitToClear());
        }
    }
}
