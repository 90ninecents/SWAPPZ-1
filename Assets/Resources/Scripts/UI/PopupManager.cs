using UnityEngine;
using System.Collections;


public class PopupManager : MonoBehaviour
{
	
	public UIToolkit manager;
	private int scaleFactor;
	
	void Start()
	{	
		scaleFactor = UI.scaleFactor;
			
		var bg = UIStateSprite.create( manager , "blackBG.png" , 0 , 0 , 10 );
		
		var whiteSmear = UIStateSprite.create( manager , "whiteSmear.png" , -40*scaleFactor , -15*scaleFactor , 9 );
		whiteSmear.hidden = true;
		StartCoroutine( animateScale( whiteSmear , Vector3.zero , Vector3.one , 0.05f , 1.8f , Easing.Exponential.easeOut ) );
		
		
		var star1 = UIStateSprite.create( manager , "distressedStar.png" , 225*scaleFactor , 85*scaleFactor , 8 );
		star1.hidden = true;
		StartCoroutine( animateScale( star1 , Vector3.zero , Vector3.one , 0.05f , 1.7f , Easing.Exponential.easeOut ) );
		
		var brick = UIStateSprite.create( manager , "greenDistressedBricks.png" , 19*scaleFactor , 7*scaleFactor , 7 );
		brick.hidden = true;
		StartCoroutine( animateScale( brick , Vector3.zero , Vector3.one , 0.05f , 1.6f , Easing.Exponential.easeOut ) );
		
		var whiteBg = UIStateSprite.create( manager , "whiteBG.png" , 100*scaleFactor , 20*scaleFactor , 6 );
		
		var circle = UIStateSprite.create( manager , "whiteDistressedCircle.png" , 10*scaleFactor , 8*scaleFactor , 5 );
		
		var yellowSmear = UIStateSprite.create( manager , "yellowSmear.png" , 30*scaleFactor , 90*scaleFactor , 4 );
		yellowSmear.hidden = true;
		StartCoroutine( animateScale( yellowSmear , Vector3.zero , Vector3.one , 0.05f , 1.5f , Easing.Exponential.easeOut ) );
		
		var emblem = UIStateSprite.create( manager , "symbol.png" , 1*scaleFactor, 15*scaleFactor , 3 );
		
		var star2 = UIStateSprite.create( manager , "distressedStar.png" , 250*scaleFactor , 45*scaleFactor , 2 );
		star2.eulerAngles = new Vector3(0, 0, 175);
		star2.hidden = true;
		StartCoroutine( animateScale( star2 , Vector3.zero , Vector3.one , 0.05f , 1.8f , Easing.Exponential.easeOut ) );
		
		var splinter = UIStateSprite.create( manager , "splinter.png" ,  15*scaleFactor , -20*scaleFactor , 1 );
		splinter.hidden = true;
		centerize( splinter );
		StartCoroutine( animateScale( splinter , Vector3.zero , Vector3.one , 0.5f , 2f , Easing.Exponential.easeIn ) );
		
		var hBox = new UIAbsoluteLayout();
		hBox.addChild( bg , whiteSmear , star1 , brick , whiteBg , circle , yellowSmear , emblem , star2 , splinter );
		hBox.matchSizeToContentSize();
		hBox.positionCenter();
		StartCoroutine( animatePosition( hBox , new Vector3( hBox.position.x , -Screen.height - hBox.height , 0 ) , hBox.position ,  0.3f , 1f , Easing.Exponential.easeOut ) );
	}
	
	private IEnumerator animatePosition( UIAbstractContainer container, Vector3 posStart, Vector3 posStop, float duration , float delay , System.Func<float, float> ease )
	{
		container.position = posStart;
		yield return new WaitForSeconds( delay );
		container.positionTo( duration, posStop , ease );
	}
	
	private IEnumerator animatePosition( UISprite sprite, Vector3 pos, float duration , float delay , System.Func<float, float> ease )
	{
		yield return new WaitForSeconds( delay );
		sprite.hidden = false;
		sprite.positionFrom( duration, pos , ease );	
	}
	
	private IEnumerator animateScale( UIAbstractContainer container, Vector3 scaleStart, Vector3 scaleStop , float duration , float delay , System.Func<float, float> ease )
	{
		yield return new WaitForSeconds( delay );
		container.scaleFromTo( duration, scaleStart , scaleStop , ease );
	}
	
	private IEnumerator animateScale( UISprite sprite, Vector3 scaleStart, Vector3 scaleStop , float duration , float delay , System.Func<float, float> ease )
	{
		yield return new WaitForSeconds( delay );
		sprite.hidden = false;
		sprite.scaleFromTo( duration, scaleStart , scaleStop , ease );
	}
	
	private void centerize( UISprite sprite )
	{
		UIAnchorInfo anchorInfo = sprite.anchorInfo;
		anchorInfo.UIxAnchor = UIxAnchor.Center;
        anchorInfo.UIyAnchor = UIyAnchor.Center;
        sprite.anchorInfo = anchorInfo;
	}
}
