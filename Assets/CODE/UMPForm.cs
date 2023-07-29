using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Ump.Api;

public class UMPForm : MonoBehaviour {
    private ConsentForm _consentForm;

    private static UMPForm instance = null;


    void Awake() {

        if (instance != null && instance != this) {
            Destroy(this.gameObject);
            return;
        } else {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    void Start() {
        ConsentRequestParameters request = new ConsentRequestParameters {
            TagForUnderAgeOfConsent = false,
        };

        ConsentInformation.Update(request, OnConsentInfoUpdated);
    }

    void OnConsentInfoUpdated(FormError error) {
        if (error != null) {
            Debug.LogError(error);
            return;
        }

        if (ConsentInformation.IsConsentFormAvailable()) {
            LoadConsentForm();
        }
    }

    void LoadConsentForm() {
        ConsentForm.Load(OnLoadConsentForm);
    }

    void OnLoadConsentForm(ConsentForm consentForm, FormError error) {
        if (error != null) {
            // Handle the error.
            Debug.LogError(error);
            return;
        }
        _consentForm = consentForm;

        if (ConsentInformation.ConsentStatus == ConsentStatus.Required) {
            _consentForm.Show(OnShowForm);
        }
    }

    void OnShowForm(FormError error) {
        if (error != null) {
            Debug.LogError(error);
            return;
        }

        // Handle dismissal by reloading form.
        LoadConsentForm();
    }
}
