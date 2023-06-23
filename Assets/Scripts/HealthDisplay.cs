using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class HealthDisplay : MonoBehaviour
{
    TextMeshProUGUI healthText;
    Player MyPlayer;

    
    // Start is called before the first frame update
    void Start()
    {
        healthText = GetComponent<TextMeshProUGUI>();
        MyPlayer = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = MyPlayer.GetHealth().ToString();
    }
}
