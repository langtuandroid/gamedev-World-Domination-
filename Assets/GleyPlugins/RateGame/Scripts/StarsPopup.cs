﻿namespace GleyRateGame
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

    public class StarsPopup : MonoBehaviour
    {
        public Text mainText;
        public Text sendButton;
        public Text notNowButton;
        public Text neverButton;
        public Transform starsHolder;
        public Button send;

        private bool openUrl;
        int starsN = 0;

        /// <summary>
        /// Set popup texts from Settings Window
        /// </summary>
        private void Start()
        {
            mainText.text = RateGame.Instance.RateGameSettings.mainText;
            if (!string.IsNullOrEmpty(RateGame.Instance.RateGameSettings.sendButton))
            {
                sendButton.text = RateGame.Instance.RateGameSettings.sendButton;
            }
            else
            {
                sendButton.transform.parent.gameObject.SetActive(false);
            }
            if (!string.IsNullOrEmpty(RateGame.Instance.RateGameSettings.notNowButton))
            {
                notNowButton.text = RateGame.Instance.RateGameSettings.notNowButton;
            }
            else
            {
                notNowButton.transform.parent.gameObject.SetActive(false);
            }

            if (!string.IsNullOrEmpty(RateGame.Instance.RateGameSettings.neverButton))
            {
                neverButton.text = RateGame.Instance.RateGameSettings.neverButton;
            }
            else
            {
                neverButton.transform.parent.gameObject.SetActive(false);
            }
            send.interactable = false;
            for (int i = 0; i < starsHolder.childCount; i++)
            {
                starsHolder.GetChild(i).GetChild(0).gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// Button event called from Send Button 
        /// </summary>
        public void SendButtonClick()
        {
            ClosePopup();
            RateGame.Instance.NeverShowPopup();
            if (openUrl)
            {
                RateGame.Instance.OpenUrl();            
            }

            PlayerPrefs.SetInt("Rated", 1);
            PlayerPrefs.Save();

            FindObjectOfType<SceneController>().rateButton.SetActive(false);
            FindObjectOfType<SceneController>().RateEvent(starsN);
        }


        /// <summary>
        /// Button event called from Later button
        /// </summary>
        public void NotNowButton()
        {
            ClosePopup();

            starsN = 0;
            FindObjectOfType<SceneController>().RateEvent(starsN);
        }


        /// <summary>
        /// Button event called from never button
        /// </summary>
        public void NeverButton()
        {
            ClosePopup();
            RateGame.Instance.NeverShowPopup();
        }


        /// <summary>
        /// Called when a star is clicked, activates the required stars
        /// </summary>
        /// <param name="star"></param>
        public void StarClicked(GameObject star)
        {
            int starNUmber = int.Parse(star.name.Split('_')[1]);
            starsN = starNUmber + 1;
            if (starNUmber + 1 < RateGame.Instance.RateGameSettings.minStarsToSend)
            {
                openUrl = false;
            }
            else
            {
                openUrl = true;
            }
            for (int i = 0; i < starsHolder.childCount; i++)
            {
                if (i <= starNUmber)
                {
                    starsHolder.GetChild(i).GetChild(0).gameObject.SetActive(true);
                }
                else
                {
                    starsHolder.GetChild(i).GetChild(0).gameObject.SetActive(false);
                }
            }
            send.interactable = true;
        }


        /// <summary>
        /// Make close animation then destroy the popup
        /// </summary>
        private void ClosePopup()
        {
            GetComponent<Animator>().SetTrigger("Close");


            StartCoroutine(CloseEvents());
        }

        IEnumerator CloseEvents()
        {
            AnimatorStateInfo info = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);
            yield return new WaitForSecondsRealtime(info.length);
            CloseEvent();
            yield return new WaitForSecondsRealtime(0.1f);
            Destroy(gameObject.transform.parent.gameObject);
        }

        /// <summary>
        /// Trigger close popup event
        /// </summary>
        private void CloseEvent()
        {
            RateGame.Instance.RatePopupWasClosed();
        }
    }
}
