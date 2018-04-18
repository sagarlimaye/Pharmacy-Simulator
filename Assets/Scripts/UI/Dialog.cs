using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog : MonoBehaviour {

    public string text;
    private string placeholderText;
    void OnEnable()
    {
        CustomerAgent.CustomerSpawned += OnCustomerSpawned;
        CustomerDestroyer.CustomerDestroyed += OnCustomerDestroyed;
    }
    void OnDisable()
    {
        CustomerAgent.CustomerSpawned -= OnCustomerSpawned;
        CustomerDestroyer.CustomerDestroyed -= OnCustomerDestroyed;
    }
    void OnCustomerSpawned(CustomerAgent customer)
    {
        placeholderText = text;
        text = text.Replace("$name", customer.customerName);
        text = text.Replace("$dob", customer.dob);
        text = text.Replace("$drug", customer.drug);
    }
    void OnCustomerDestroyed()
    {
        text = placeholderText;
    }


}
