//using UnityEngine;
//using System.Collections;
//
//
//public class MainMenuManager : MonoBehaviour
//{
//	
//	public UIToolkit manager;
//	
//	void Start()
//	{	
////		var bg = UIStateSprite.create( manager, "BG.png" , 0 , 0 , 10 );
////		bg.positionCenter();
////		
////		var donChar = UIStateSprite.create( manager , "Don.png" , 0 , 0 , 9 );
////		donChar.positionFromBottomLeft( 0 , 0 );
////		donChar.hidden = true;
////		StartCoroutine( animatePosition( donChar , new Vector3( donChar.position.x - donChar.width , donChar.position.y , 0 ) , 0.3f , 0.5f , Easing.Exponential.easeIn ) );
////		
////		var mikeChar = UIStateSprite.create( manager , "Mike.png" , 0 , 0 , 8 );
////		mikeChar.positionFromBottomLeft( 0 , 0 );
////		mikeChar.hidden = true;
////		StartCoroutine( animatePosition( mikeChar , new Vector3( mikeChar.position.x - mikeChar.width , mikeChar.position.y - mikeChar.height , 0 ) , 0.3f , 0.9f , Easing.Exponential.easeIn ) );
////		
////		var ralphChar = UIStateSprite.create( manager , "Ralph.png" , 0 , 0 , 7 );
////		ralphChar.positionFromBottomRight( 0.2f , 0 );
////		ralphChar.hidden = true;
////		StartCoroutine( animatePosition( ralphChar , new Vector3( ralphChar.position.x + ralphChar.width , ralphChar.position.y , 0 ) , 0.3f , 0.7f , Easing.Exponential.easeIn ) );
////		
////		var leoChar = UIStateSprite.create( manager , "Leo.png" , 0 , 0 , 6 );
////		leoChar.positionFromBottomRight( 0 , 0 );
////		leoChar.hidden = true;
////		StartCoroutine( animatePosition( leoChar , new Vector3( leoChar.position.x + leoChar.width , leoChar.position.y , 0 ) , 0.3f , 1.1f , Easing.Exponential.easeIn ) );
////		
////		var title = UIStateSprite.create( manager, "Title.png" , 0 , 0 , 5 );
////		title.positionFromCenter( -0.3f , 0 );
////		title.hidden = true;
////		StartCoroutine( animatePosition( title , new Vector3( title.position.x , title.height , 0 ) , 0.3f , 1.3f , Easing.Exponential.easeIn ) );
////		
////		var playButton = UIButton.create( manager , "PlayUp.png", "PlayDown.png", 0, 0 , 4 );
////		playButton.positionFromCenter( 0.05f  , 0 );
////		playButton.hidden = true;
////		StartCoroutine( animateScale( playButton , Vector3.zero , Vector3.one , 0.6f , 1.5f , Easing.Bounce.easeOut ) );
////		
////		var storeButton = UIButton.create( manager , "StoreUp.png", "StoreDown.png", 0 , 0 , 3 );
////		storeButton.positionFromCenter( 0.2f , 0 );
////		storeButton.hidden = true;
////		StartCoroutine( animateScale( storeButton , Vector3.zero , Vector3.one , 0.6f , 1.6f , Easing.Bounce.easeOut ) );
////		
////		var gcButton = UIButton.create( manager , "GCUp.png", "GCDown.png", 0 , 0 , 2 );
////		gcButton.positionFromCenter( 0.4f , 0 );
////		gcButton.hidden = true;
////		StartCoroutine( animateScale( gcButton , Vector3.zero , Vector3.one , 0.6f , 1.7f , Easing.Bounce.easeOut ) );
////		
////		var settingButton = UIButton.create( manager , "OptionUp.png", "OptionDown.png" , 0 , 0 , 1 );
////		settingButton.positionFromTopRight( 0 , 0.02f );
////		settingButton.hidden = true;
////		StartCoroutine( animateScale( settingButton , Vector3.zero , Vector3.one , 0.3f , 1.8f , Easing.Exponential.easeIn ) );
//		
//		var bg = UIStateSprite.create( manager, "bg.png" , 0 , 0 , 11 );
//		bg.positionCenter();
//		
//		var mikeChar = UIStateSprite.create( manager , "mike.png" , 0 , 0 , 10 );
//		mikeChar.pixelsFromCenter( -20 , -90 );
//		
//		var leoChar = UIStateSprite.create( manager , "leo.png" , 0 , 0 , 9 );
//		leoChar.pixelsFromCenter( 103 , 21 );
//		
//		var raphChar = UIStateSprite.create( manager , "raph.png" , 0 , 0 , 8 );
//		raphChar.pixelsFromCenter( -112 , 60 );
//		
//		var donChar = UIStateSprite.create( manager , "don.png" , 0 , 0 , 7 );
//		donChar.pixelsFromCenter( -11 , 155 );
//		
//		var logo = UIStateSprite.create( manager, "logo.png" , 0 , 0 , 6 );
//		logo.pixelsFromTopLeft( 13 , 8 );
//		
//		var setting = UIButton.create( manager, "setting.png", "setting.png" , 0 , 0 , 5 );
//		setting.pixelsFromTopRight( 0 , 0 );
//		
//		var play = UIButton.create( manager, "play.png" , "play.png" , 0 , 0 , 4 );
//		play.onTouchUpInside += UIFunctions.NextButton;
//		play.pixelsFromBottomLeft( 0 , 0 );
//		
//		var rightBase = UIStateSprite.create( manager, "right_base.png" , 0 , 0 , 3 );
//		rightBase.pixelsFromBottomRight( -1 , 0 );
//		
//		var lair = UIButton.create( manager, "lair.png" , "lair.png" , 0 , 0 , 2 );
//		lair.pixelsFromBottomRight( 48 , 11 );
//		
//		var training = UIButton.create( manager , "training.png" , "training.png" , 0 , 0 , 1 );
//		training.onTouchUpInside += UIFunctions.GoToAR;
//		training.pixelsFromBottomRight( 7 , 13 );
//	}
//	
//	private IEnumerator animatePosition( UISprite sprite, Vector3 pos, float duration , float delay , System.Func<float, float> ease )
//	{
//		yield return new WaitForSeconds( delay );
//		sprite.hidden = false;
//		var ani = sprite.positionFrom( duration, pos , ease );	
//	}
//	
//	private IEnumerator animateScale( UISprite sprite, Vector3 scaleStart, Vector3 scaleStop , float duration , float delay , System.Func<float, float> ease )
//	{
//		yield return new WaitForSeconds( delay );
//		sprite.hidden = false;
//		var ani = sprite.scaleFromTo( duration, scaleStart , scaleStop , ease );
//	}
//}

using UnityEngine;
using System.Collections;


public class MainMenuManager : MonoBehaviour
{
	public float animationSpeed = 1.5f;
	public UIToolkit manager;
	
	void Start()
	{		
		manager = UIToolkit.instance;
		// IMPORTANT: depth is 1 on top higher numbers on the bottom.  This means the lower the number is the closer it gets to the camera.
		
		var bg = UIStateSprite.create( manager, "BG.png" , 0 , 0 , 10 );
		bg.positionCenter();
		
		var donChar = UIStateSprite.create( manager , "Don.png" , 0 , 0 , 9 );
		donChar.positionFromBottomLeft( 0 , 0 );
		donChar.hidden = true;
		StartCoroutine( animatePosition( donChar , new Vector3( donChar.position.x - donChar.width , donChar.position.y , 0 ) , 0.3f / animationSpeed , 0.5f / animationSpeed, Easing.Exponential.easeIn ) );
		
		var mikeChar = UIStateSprite.create( manager , "Mike.png" , 0 , 0 , 8 );
		mikeChar.positionFromBottomLeft( 0 , 0 );
		mikeChar.hidden = true;
		StartCoroutine( animatePosition( mikeChar , new Vector3( mikeChar.position.x - mikeChar.width , mikeChar.position.y - mikeChar.height , 0 ) , 0.3f / animationSpeed, 0.9f / animationSpeed, Easing.Exponential.easeIn ) );
		
		var ralphChar = UIStateSprite.create( manager , "Ralph.png" , 0 , 0 , 7 );
		ralphChar.positionFromBottomRight( 0.2f , 0 );
		ralphChar.hidden = true;
		StartCoroutine( animatePosition( ralphChar , new Vector3( ralphChar.position.x + ralphChar.width , ralphChar.position.y , 0 ) , 0.3f  / animationSpeed, 0.7f  / animationSpeed, Easing.Exponential.easeIn ) );
		
		var leoChar = UIStateSprite.create( manager , "Leo.png" , 0 , 0 , 6 );
		leoChar.positionFromBottomRight( 0 , 0 );
		leoChar.hidden = true;
		StartCoroutine( animatePosition( leoChar , new Vector3( leoChar.position.x + leoChar.width , leoChar.position.y , 0 ) , 0.3f  / animationSpeed, 1.1f  / animationSpeed, Easing.Exponential.easeIn ) );
		
		var title = UIStateSprite.create( manager, "Title.png" , 0 , 0 , 5 );
		title.positionFromCenter( -0.3f , 0 );
		title.hidden = true;
		StartCoroutine( animatePosition( title , new Vector3( title.position.x , title.height , 0 ) , 0.3f / animationSpeed , 1.3f / animationSpeed , Easing.Exponential.easeIn ) );
		
		
		
		var playButton = UIButton.create( manager , "PlayUp.png", "PlayDown.png", 0, 0 , 4 );
		playButton.positionFromCenter( 0 , 0 );
		playButton.hidden = true;
		playButton.onTouchUpInside += UIFunctions.NextButton;
		StartCoroutine( animateScale( playButton , Vector3.zero , Vector3.one , 0.6f  / animationSpeed, 1.5f  / animationSpeed, Easing.Bounce.easeOut ) );
		
		
		var storeButton = UIButton.create( manager , "StoreUp.png", "StoreDown.png", 0 , 0 , 3 );
		storeButton.positionFromCenter( 0.15f , 0 );
		storeButton.hidden = true;
		StartCoroutine( animateScale( storeButton , Vector3.zero , Vector3.one , 0.6f  / animationSpeed, 1.6f  / animationSpeed, Easing.Bounce.easeOut ) );
		
		
		var gcButton = UIButton.create( manager , "GCUp.png", "GCDown.png", 0 , 0 , 2 );
		gcButton.positionFromCenter( 0.4f , 0 );
		gcButton.hidden = true;
		StartCoroutine( animateScale( gcButton , Vector3.zero , Vector3.one , 0.6f / animationSpeed , 1.7f  / animationSpeed, Easing.Bounce.easeOut ) );
		
		
		var settingButton = UIButton.create( manager , "OptionUp.png", "OptionDown.png" , 0 , 0 , 1 );
		settingButton.positionFromTopRight( 0 , 0.02f );
		settingButton.hidden = true;
		settingButton.onTouchUpInside += UIFunctions.MuteButton;
		StartCoroutine( animateScale( settingButton , Vector3.zero , Vector3.one , 0.3f / animationSpeed , 1.8f  / animationSpeed, Easing.Exponential.easeIn ) );
	}
	
	private IEnumerator animatePosition( UISprite sprite, Vector3 pos, float duration , float delay , System.Func<float, float> ease )
	{
		yield return new WaitForSeconds( delay );
		sprite.hidden = false;
		var ani = sprite.positionFrom( duration, pos , ease );	
	}
	
	private IEnumerator animateScale( UISprite sprite, Vector3 scaleStart, Vector3 scaleStop , float duration , float delay , System.Func<float, float> ease )
	{
		yield return new WaitForSeconds( delay );
		sprite.hidden = false;
		var ani = sprite.scaleFromTo( duration, scaleStart , scaleStop , ease );
	}
}
