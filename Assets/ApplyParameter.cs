using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

namespace wproj
{
	public class ApplyParameter : MonoBehaviour
	{

		public Slider slStopMagTh;
		public Slider slStopTimeTh;
		public Slider slSwipeMagTh;
		public Slider slSwipeVeloTh;
		public Slider slRateVeloTh;
		public Slider slSwipeTimeTh;

		public Slider slPosX;
		public Slider slPosY;
		public Slider slPosZ;

		public Text txStopMagTh;
		public Text txStopTimeTh;
		public Text txSwipeMagTh;
		public Text txSwipeVeloTh;
		public Text txRateVeloTh;
		public Text txSwipeTimeTh;

		public GetControllerPos get_controller_pos;
		public SteamVROverlayer steam_vr_overlayer;

		public void ChangeValue()
		{
			txStopMagTh.text = slStopMagTh.value.ToString();
			txStopTimeTh.text = slStopTimeTh.value.ToString();
			txSwipeMagTh.text = slSwipeMagTh.value.ToString();
			txSwipeVeloTh.text = slSwipeVeloTh.value.ToString();
			txRateVeloTh.text = slRateVeloTh.value.ToString();
			txSwipeTimeTh.text = slSwipeTimeTh.value.ToString();

			get_controller_pos.StopMagnitudeThreshold = slStopMagTh.value;
			get_controller_pos.StopTimeThreshold = (int)slStopTimeTh.value;
			get_controller_pos.MagnitudeThreshold = slSwipeMagTh.value;
			get_controller_pos.YVelocityThreshold = slSwipeVeloTh.value;
			get_controller_pos.RateThreshold = slRateVeloTh.value;
			get_controller_pos.SwipeTimeThreshold = (int)slSwipeTimeTh.value;

			steam_vr_overlayer.posX = slPosX.value;
			steam_vr_overlayer.posY = slPosY.value;
			steam_vr_overlayer.posZ = slPosZ.value;
		}

		// Use this for initialization
		void Start()
		{
			ChangeValue();
		}
	}
}