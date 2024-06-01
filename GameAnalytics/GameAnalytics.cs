using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.Analytics;

namespace ST
{
    public class GameAnalytics : MonoBehaviour
    {
        public int deaths;

        async void Start()
        {
            await UnityServices.InitializeAsync();

            AskForConsent();
        }

        void AskForConsent()
        {
            //ConsentGiven();
            // ... show the player a UI element that asks for consent.
        }

        void ConsentGiven()
        {
            Debug.Log("started analytics");
            AnalyticsService.Instance.StartDataCollection();
        }

        public void CustomEvent()
        {
            AnalyticsResult res = Analytics.CustomEvent(
                "gameOver", 
                new Dictionary<string, object>
            {
                {"potions", 5},
                {"coins", 110}
            });
            
            
             AnalyticsService.Instance.CustomData("gameOver", new Dictionary<string, object>
            {
                {"potions", 5},
                {"coins", 110}
            });
            Debug.Log("sent analytics: " + res);
        }

        //Events.Flush(); to send event immediately sonst in 60 sek
    }
}