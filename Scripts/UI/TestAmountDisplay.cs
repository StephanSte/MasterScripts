using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ST
{
    public class TestAmountDisplay : MonoBehaviour
    {
        public Text testAmountText;

        public void SetTestAmountText(int testId, int tests)
        {
            testAmountText.text = testId + " / " + (tests - 1);
        }
    }
}