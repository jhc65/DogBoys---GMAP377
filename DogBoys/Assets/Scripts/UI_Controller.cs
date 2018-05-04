using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //Need for UI components

public class UI_Controller : MonoBehaviour
{
    [SerializeField]
    private Image currentPlayerHealthBar_;
    [SerializeField]
    private Image currentPlayerMovementBar_;
    [SerializeField]
    private Image currentPlayerPortrait_;
    [SerializeField]
    private Text currentplayerName_;
    [SerializeField]
    private Text currentPlayerGunName_;
    [SerializeField]
    private Image currentPlayerAmmoCount_;
    private int lastRecordedHealth_ = 0;
    private int lastRecordedMovement_ = 0;
    private int lastRecoredAmmoCount_ = 0;


    public void updateCurrentHealthBar(int currentHealth, int maxHealth) {
        if(currentHealth != lastRecordedHealth_) {
            lastRecordedHealth_ = currentHealth;
            float newFillAmount = ((float)currentHealth) / maxHealth;
            currentPlayerHealthBar_.fillAmount = newFillAmount;
        }
    }

    public void updateCurrentMovementBar(int currentMovement, int maxMovement) {
        if (currentMovement != lastRecordedMovement_) {
            lastRecordedMovement_ = currentMovement;
            float newFillAmount = ((float)currentMovement) / maxMovement;
            currentPlayerMovementBar_.fillAmount = newFillAmount;
        }
    }

    public void updateCurrentPortraitSprite(Sprite newPortrait) {
        currentPlayerPortrait_.overrideSprite = newPortrait;
    }

    public void updateCurrentName(string newName) {
        currentplayerName_.text = newName;
    }

    public void updateCurrentAmmoCountBar(int currentAmmo, int maxAmmo) {
        if (currentAmmo != lastRecoredAmmoCount_) {
            lastRecoredAmmoCount_ = currentAmmo;
            float newFillAmount = ((float)currentAmmo) / maxAmmo;
            currentPlayerAmmoCount_.fillAmount = newFillAmount;
        }
    }

    public void updateCurrentGunName(string newName)
    {
        currentPlayerGunName_.text = newName;
    }
}