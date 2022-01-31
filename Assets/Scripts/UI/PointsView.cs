using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PointsView  : MonoBehaviour
    {
        [SerializeField]
        private Text _text;
        
        public string Text
        {
            set => _text.text = "Score: " + value;
        }
    }
}