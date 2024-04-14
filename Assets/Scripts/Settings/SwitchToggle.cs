using UnityEngine ;
using UnityEngine.UI ;


public class SwitchToggle : MonoBehaviour, IDataPersistence
{
   [SerializeField] RectTransform uiHandleRectTransform ;
    //[SerializeField] Color backgroundActiveColor;
    //[SerializeField] Color handleActiveColor;

    //Image backgroundImage, handleImage;
    public bool subtitle;

    //Color backgroundDefaultColor, handleDefaultColor;

    Toggle toggle ;

   Vector2 handlePosition ;

   void Awake ( ) {
      toggle = GetComponent <Toggle> ( ) ;

      handlePosition = uiHandleRectTransform.anchoredPosition ;

      //backgroundImage = uiHandleRectTransform.parent.GetComponent<Image>();
      //handleImage = uiHandleRectTransform.GetComponent<Image>();

      //backgroundDefaultColor = backgroundImage.color;
      //handleDefaultColor = handleImage.color;

      toggle.onValueChanged.AddListener (OnSwitch) ;

      if (toggle.isOn)
         OnSwitch (true) ;
   }

   void OnSwitch (bool on) {
      uiHandleRectTransform.anchoredPosition = on ? handlePosition * -1 : handlePosition ; 
      //backgroundImage.color = on ? backgroundActiveColor : backgroundDefaultColor ;
      //handleImage.color = on ? handleActiveColor : handleDefaultColor;
    }

   void OnDestroy ( ) {
      toggle.onValueChanged.RemoveListener (OnSwitch) ;
   }

    public void SaveData(ref GameData data) 
    {
        if (toggle.isOn)
        {
            data.subtitle = true;
        }
        else 
        {
            data.subtitle = false;
        }
    }
    public void LoadData( GameData data)
    {
        toggle.isOn = data.subtitle;
    }
}
