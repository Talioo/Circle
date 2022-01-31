using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PathView : MonoBehaviour
    {
        [SerializeField]
        private Text _text;
        
        public string Text
        {
            set => _text.text = value;
        }

    }
}