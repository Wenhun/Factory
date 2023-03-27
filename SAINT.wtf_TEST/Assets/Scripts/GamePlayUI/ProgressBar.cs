using UnityEngine;
using UnityEngine.UI;

namespace Factory.GamePlayUI
{
    public class ProgressBar : MonoBehaviour
    {
        [Header("Bar components")]
        [SerializeField] private Image _bar;
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Camera _camera;

        private float timer, totalTime;

        private void Awake()
        {
            if (_bar == null)
            {
                Debug.LogError($"{nameof(_bar)} is not set.");
                enabled = false;
            }

            if (_bar != null && (_bar.type != Image.Type.Filled || _bar.fillMethod != Image.FillMethod.Horizontal))
            {
                Debug.LogError($"{nameof(_bar)} is incorrectly configured.");
                enabled = false;
            }

            if (_canvas == null)
            {
                Debug.LogError($"{nameof(_canvas)} is not set.");
                enabled = false;
            }

            if (_camera == null)
            {
                Debug.LogError($"{nameof(_camera)} is not set.");
                enabled = false;
            }

            _canvas.enabled = false;
        }

        private void Update()
        {
            if (!enabled) return;

            timer -= Time.deltaTime;
            _bar.fillAmount = timer / totalTime;

            if (timer <= 0)
            {
                _canvas.enabled = false;
            }
        }

        private void LateUpdate()
        {
            if (!enabled) return;
            _canvas.transform.LookAt(new Vector3(transform.position.x, _camera.transform.position.y, _camera.transform.position.z));
        }

        public void BarChange(float timer)
        {
            this.timer = timer;
            totalTime = timer;
            _canvas.enabled = true;
        }
    }
}