using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    public static int money;

    TMP_Text text;
    // Start is called before the first frame update
    void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        money = 1000;
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "$" + money;
    }
}
