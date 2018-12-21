using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.UI;

namespace wproj
{
	public class GetControllerPos : MonoBehaviour
	{
		public StatusViewer status_viewer;

		public SteamVROverlayer steam_vr_overlayer;
		public uint ControllerID = 1;

		public int count = 0;
		public int fase = 0;

		[Range(0f, 1.0f)]
		public float StopMagnitudeThreshold = 0.2f;

		[Range(1, 120)]
		public int StopTimeThreshold = 60;

		[Range(0.5f, 5f)]
		public float MagnitudeThreshold = 1.0f;

		[Range(0.5f, 5f)]
		public float YVelocityThreshold = 1.0f;

		[Range(0.1f, 0.9f)]
		public float RateThreshold = 0.2f;

		[Range(1, 120)]
		public int SwipeTimeThreshold = 10;

		public AudioSource audio_souce;
		public AudioClip audio_clip;

		public Quaternion GetRotation(TrackedDevicePose_t device_pose)
		{
			Quaternion ret = new Quaternion();

			var matrix = device_pose.mDeviceToAbsoluteTracking;
			//0 1 2 3
			//4 5 6 7
			//8 9 10 11
			ret.w = Mathf.Sqrt(Mathf.Max(0, 1 + matrix.m0 + matrix.m5 + matrix.m10)) / 2;
			ret.x = Mathf.Sqrt(Mathf.Max(0, 1 + matrix.m0 - matrix.m5 - matrix.m10)) / 2;
			ret.y = Mathf.Sqrt(Mathf.Max(0, 1 - matrix.m0 + matrix.m5 - matrix.m10)) / 2;
			ret.z = Mathf.Sqrt(Mathf.Max(0, 1 - matrix.m0 - matrix.m5 + matrix.m10)) / 2;
			return ret;
		}

		public Vector3 GetPosition(TrackedDevicePose_t device_pose)
		{
			Vector3 vector;
			var matrix = device_pose.mDeviceToAbsoluteTracking;
			vector.x = matrix.m3;
			vector.y = matrix.m7;
			vector.z = matrix.m11;
			return vector;
		}

		public Vector3 Hmd2Vec3(HmdVector3_t hmdVector3_T)
		{
			return new Vector3(hmdVector3_T.v0, hmdVector3_T.v1, hmdVector3_T.v2);
		}

		// Use this for initialization
		void Start()
		{
			ControllerID = steam_vr_overlayer.openvr.GetTrackedDeviceIndexForControllerRole(ETrackedControllerRole.RightHand);
			Debug.Log("[GetControllerPos]Used Controller ID = " + ControllerID);
		}

		// Update is called once per frame
		void Update()
		{
			TrackedDevicePose_t[] controller_pose = new TrackedDevicePose_t[16];


			steam_vr_overlayer.openvr.GetDeviceToAbsoluteTrackingPose(ETrackingUniverseOrigin.TrackingUniverseSeated, 0, controller_pose);

			Vector3 velo = Hmd2Vec3(controller_pose[ControllerID].vVelocity);

			//Debug.Log(velo);

			if (velo.magnitude < StopMagnitudeThreshold)
			{
				if (count++ > StopTimeThreshold)
				{
					fase = 1;
					Debug.Log("Stopped");
					if (count > 30) count = 30;
				}
			}
			else if (fase >= 1 && count > 0)
			{
				if (velo.magnitude > MagnitudeThreshold && velo.y < -YVelocityThreshold && Mathf.Abs(velo.x) < Mathf.Abs(velo.y) * RateThreshold && Mathf.Abs(velo.z) < Mathf.Abs(velo.y) * RateThreshold)
				{
					if (fase == 1) { fase = 2; count = 0; }
					count++;
					Debug.Log("Moving");
					if (count > SwipeTimeThreshold)
					{
						Action();
						fase = 0;
					}
				}
				else
				{
					count--;
					if (count <= 0)
					{
						fase = 0;
						Debug.Log("Failed.");
						status_viewer.setNg();
					}
				}
			}
		}

		void Action()
		{
			Debug.Log("Action");
			audio_souce.PlayOneShot(audio_clip);
			status_viewer.setOk();
		}
	}

}