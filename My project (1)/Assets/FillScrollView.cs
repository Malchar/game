using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FillScrollView : MonoBehaviour
{
    [field: SerializeField]
    private GameObject ContentPrefab;
    [field: SerializeField]
    private Transform ContentParent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void AddContent(ISelectable content){
        GameObject container = Instantiate(ContentPrefab);
        container.GetComponentInChildren<TMP_Text>().text = content.GetName();
        // the content's data needs to be accessable when the container is selected
        container.transform.SetParent(ContentParent);
        container.transform.localScale = Vector2.one;
    }
    public void SetContent(ISelectable[] contents) {
        for (int i = 0; i < contents.Length; i++) {
            AddContent(contents[i]);
        }
    }
}
