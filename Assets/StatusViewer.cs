using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace wproj
{
	public class StatusViewer : MonoBehaviour
	{

		public GetControllerPos get_controller_pos;
		public GameObject stop_panel;
		public GameObject switch_panel;
		public GameObject move_panel;
		public GameObject ok_panel;
		public GameObject failed_panel;

		int count = 0;
		bool is_ok = false;
		public void setOk() { is_ok = true; count = 60; }
		public void setNg() { is_ok = false; count = 60; }

		// Update is called once per frame
		void Update()
		{
			stop_panel.SetActive(false);
			switch_panel.SetActive(false);
			move_panel.SetActive(false);
			ok_panel.SetActive(false);
			failed_panel.SetActive(false);
			if (get_controller_pos.fase == 0 && get_controller_pos.count >= 30) stop_panel.SetActive(true);
			else if (get_controller_pos.fase == 1) switch_panel.SetActive(true);
			else if (get_controller_pos.fase == 2) move_panel.SetActive(true);
			if(count > 0)
			{
				if (is_ok) ok_panel.SetActive(true);
				else failed_panel.SetActive(true);
				count--;
			}
		}
	}
}
