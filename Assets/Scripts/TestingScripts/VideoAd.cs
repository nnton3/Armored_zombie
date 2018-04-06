using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class VideoAd : MonoBehaviour {

	private RewardBasedVideoAd videoAd;
	bool play = false;

	void Start () {
		this.videoAd = RewardBasedVideoAd.Instance;
	}
	
	void Update () {
		if (videoAd.IsLoaded() && !play) {
			print ("work");
			videoAd.Show();
			play = true;
		}
	}

	public void PlayVideo() {
		string adUnitId = "ca-app-pub-3940256099942544/5224354917";
		AdRequest request = new AdRequest.Builder().Build();
		this.videoAd.LoadAd(request, adUnitId);
		play = false;
	}
}
