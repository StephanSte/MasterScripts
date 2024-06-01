using System;
using System.Collections.Generic;
using UnityEngine;

namespace ST
{
    public class TestParameter : MonoBehaviour
    {
        private int error = 999;
        public List<int> deaths;
        public List<int> frustlevel;
        public List<int> testSkipped;
        public List<int> dodges;
        public List<int> sprints;
        public List<float> ttk;
        public List<int> damageTotal;
        public List<int> twoHanded;
        public List<int> lockOns;
        public List<int> consumables;
        public List<int> lightAttacks;
        public List<int> heavyAttacks;


        private TestManager _testManager;
        private HandleTextFile _handleTextFile;
        private float deltaTime;
        private bool enableNextButton;

        private void Start()
        {
            _testManager = FindObjectOfType<TestManager>();
            _handleTextFile = FindObjectOfType<HandleTextFile>();
        }

        public void startTest()
        {
            //set all tests to 0
            deaths.Add(0);
            frustlevel.Add(0);
            testSkipped.Add(0);
            dodges.Add(0);
            sprints.Add(0);
            ttk.Add(0f);
            damageTotal.Add(0);
            twoHanded.Add(0);
            lockOns.Add(0);
            consumables.Add(0);
            lightAttacks.Add(0);
            heavyAttacks.Add(0);
            deltaTime = Time.time;
            if (_testManager != null || _testManager.nextTestUIButton != null)
            {
                _testManager.nextTestUIButton.SetActive(false);
            }
        }

        public void skipedTest()
        {
            //set all tests to 0
            testSkipped[getId()] = 1;
        }

        public void skipedTestFalse()
        {
            //set all tests to 0
            testSkipped[getId()] = 0;
        }

        public void calcTimeElapsed()
        {
            float temp = (Time.time - deltaTime);
            ttk[getId()] = temp;
        }

        public string endTest()
        {
            string val = formatTests();
            return _handleTextFile.WriteString(val);
        }

        public string formatTests()
        {
            string returnstring = "";
            for (int i = 0; i < _testManager.bosses.Length; i++)
            {
                returnstring += deaths[i].ToString();
                returnstring += " ";
                returnstring += frustlevel[i].ToString();
                returnstring += " ";
                returnstring += testSkipped[i].ToString();
                returnstring += " ";
                returnstring += dodges[i].ToString();
                returnstring += " ";
                returnstring += sprints[i].ToString();
                returnstring += " ";
                returnstring += ttk[i].ToString();
                returnstring += " ";
                returnstring += damageTotal[i].ToString();
                returnstring += " ";
                returnstring += twoHanded[i].ToString();
                returnstring += " ";
                returnstring += lockOns[i].ToString();
                returnstring += " ";
                returnstring += consumables[i].ToString();
                returnstring += " ";
                returnstring += lightAttacks[i].ToString();
                returnstring += " ";
                returnstring += heavyAttacks[i].ToString();
                returnstring += "\n";
            }

            return returnstring;
        }

        public int getId()
        {
            return _testManager.testiD;
        }

        public void setFrustlevel(int level)
        {
            frustlevel[getId()] = level;
            _testManager.nextTestUIButton.SetActive(true);
        }
    }
}