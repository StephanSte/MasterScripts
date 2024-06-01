using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ST
{
    public class QuickSlotsUI : MonoBehaviour
    {
        public Image leftWeaponIcon;
        public Image rightWeaponIcon;
        public Image FlaskIcon;
        public Image SpellIcon;
        public TextMeshProUGUI flaskCharges;
        public void UpdateFlaskQuickSlotsUI(ConsumableItem flask)
        {
            if(flask.itemIcon != null)
            {
                FlaskIcon.sprite = flask.itemIcon;
                FlaskIcon.enabled = true;
            }
            else
            {
                FlaskIcon.sprite = null;
                FlaskIcon.enabled = false;
            }
        }
        
        public void UpdateFlaskChargesUI(ConsumableItem flask)
        {
            flaskCharges.text = flask.currentItemAmount.ToString();
        }
        
        public void UpdateSpellQuickSlotsUI(SpellItem spell)
        {
            if(spell.itemIcon != null)
            {
                SpellIcon.sprite = spell.itemIcon;
                SpellIcon.enabled = true;
            }
            else
            {
                SpellIcon.sprite = null;
                SpellIcon.enabled = false;
            }
        }
        
        public void UpdateWeaponQuickSlotsUI(bool isLeft, WeaponItem weapon)
        {
            if(isLeft == false)
            {
                if(weapon.itemIcon != null)
                {
                    rightWeaponIcon.sprite = weapon.itemIcon;
                    rightWeaponIcon.enabled = true;
                }
                else
                {
                    rightWeaponIcon.sprite = null;
                    rightWeaponIcon.enabled = false;
                }
            }
            else
            {
                if(weapon.itemIcon != null)
                {
                    leftWeaponIcon.sprite = weapon.itemIcon;
                    leftWeaponIcon.enabled = true;
                }
                else
                {
                    leftWeaponIcon.sprite = null;
                    leftWeaponIcon.enabled = false;
                }
            }
        }
    }
}