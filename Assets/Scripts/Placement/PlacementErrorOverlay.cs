using System;
using System.Collections;
using Exception;
using TMPro;
using UnityEngine;

namespace Placement {
    public class PlacementErrorOverlay : MonoBehaviour {
        
        [Header("UI Elements")]
        [SerializeField]
        private TextMeshProUGUI errorTitleText;
        
        [SerializeField]
        private TextMeshProUGUI errorMessageText;
        
        [Header("Positioning")]
        [SerializeField]
        private float positionThreshold = 140f;

        [Header("Animation")] 
        [SerializeField] 
        [Range(5, 15)]
        private float slideSpeed;
        
        [SerializeField] 
        [Range(0, 5)]
        private int secondsOnScreen;
        
        private RectTransform _rectTransform;
        
        private void Awake() {
            this.gameObject.SetActive(false);
            _rectTransform = this.GetComponent<RectTransform>();
            _rectTransform.anchoredPosition = new Vector2(_rectTransform.anchoredPosition.x, positionThreshold);
        }

        private void OnDisable() {
            this.StopAllCoroutines();
        }

        public void ShowError(UserException exception) {
            this.StopAllCoroutines();
            errorTitleText.text = exception.ErrorTitle;
            errorMessageText.text = exception.ErrorMessage;
            this.gameObject.SetActive(true);
            this.StartCoroutine(AnimateOverlay());
        }

        private IEnumerator AnimateOverlay() {
            Debug.Log($"[PlacementErrorOverlay] Start animating overlay");
            yield return this.StartCoroutine(SlideOverlayIn());
            yield return new WaitForSeconds(secondsOnScreen);
            yield return this.StartCoroutine(SlideOverlayOut());
            Debug.Log($"[PlacementErrorOverlay] Finished animating overlay");
        }
        
        private IEnumerator SlideOverlayIn() {
            Debug.Log($"[PlacementErrorOverlay] Start sliding overlay in");
            float i = 0f;
            Vector3 currentPosition = _rectTransform.anchoredPosition;
            Vector3 newPosition = new Vector2(_rectTransform.anchoredPosition.x, -positionThreshold);
            
            while (i < 1) {
                i += Time.deltaTime * slideSpeed;
                _rectTransform.anchoredPosition = Vector2.Lerp(currentPosition, newPosition, i);
                yield return null;
            }
            
            _rectTransform.anchoredPosition = new Vector2(_rectTransform.anchoredPosition.x, -positionThreshold);
        }
        
        private IEnumerator SlideOverlayOut() {
            Debug.Log($"[PlacementErrorOverlay] Start sliding overlay out");
            float i = 0f;
            Vector3 currentPosition = _rectTransform.anchoredPosition;
            Vector3 newPosition = new Vector2(_rectTransform.anchoredPosition.x, positionThreshold);
            
            while (i < 1) {
                i += Time.deltaTime * slideSpeed;
                _rectTransform.anchoredPosition = Vector2.Lerp(currentPosition, newPosition, i);
                yield return null;
            }
            
            _rectTransform.anchoredPosition = new Vector2(_rectTransform.anchoredPosition.x, positionThreshold);

        }
    }
}