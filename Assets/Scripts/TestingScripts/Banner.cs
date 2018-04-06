using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class Banner : MonoBehaviour {
	
	private const string banner1 = "ca-app-pub-3940256099942544/6300978111";
	private const string banner2 = "ca-app-pub-9054306574918949/6390441049";
	private BannerView bannerView;


	void Start () {
		MobileAds.Initialize (banner1);
		MobileAds.Initialize (banner2);

		this.Banners ();
	}
	
	void Update () {

	}

	private void Banners() {
		bannerView = new BannerView (banner1, AdSize.Banner, AdPosition.Top);
		AdRequest request = new AdRequest.Builder().Build();
		bannerView.LoadAd (request);
		bannerView.OnAdFailedToLoad += HandleOnAdFailedToLoad;
		print ("work");
	}

	public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
	{
		MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
			+ args.Message);
	}
}
