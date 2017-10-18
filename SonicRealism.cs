using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

[KSPAddon(KSPAddon.Startup.Flight, false)]
class SonicRealism : MonoBehaviour {
	public void Update() {
		var g = gain;
		foreach (var src in FindObjectsOfType(typeof(AudioSource)) as AudioSource[]) {
			var obj = src.gameObject;
			if (null == obj.GetComponent<Part>() || null == src.clip) continue;

			var filter = obj.GetComponent<FlatGainFilter>();
			if (null == filter) filter = obj.AddComponent<FlatGainFilter>();
			filter.gain = g;
		}
	}

	private static float gain { get {
		switch (CameraManager.Instance.currentCameraMode) {
		case CameraManager.CameraMode.Internal:
		case CameraManager.CameraMode.IVA:
		case CameraManager.CameraMode.Map:
			return 1f;
		default:
			var vessel = FlightGlobals.ActiveVessel;
			var ρ = vessel.atmDensity;
			if (0 == ρ) return 0f;
			var a = vessel.speedOfSound;
			var v = vessel.srf_velocity;
			var mach = v/a;
			var cosShockAngle = Mathf.Sqrt(1f - 1f/Vector3.Dot(mach, mach));
			var relCameraPos = vessel.transform.position - FlightCamera.fetch.transform.position;
			var machCosΦ = Vector3.Dot(mach, relCameraPos.normalized);
			var cosΦ = machCosΦ / mach.magnitude;
			var inShockCone = Vector3.Dot(mach, mach) < 1f || cosΦ > cosShockAngle;
			var dθByDφ = 1f + machCosΦ / Mathf.Sqrt(1f - Vector3.Dot(mach, mach) + machCosΦ * machCosΦ);
			var body = vessel.mainBody;
			var ρ_0 = body.GetDensity(body.GetPressure(0), body.GetTemperature(0));
			return inShockCone ? dθByDφ * (float)(ρ/ρ_0) : 0f;
		}
	} }
}

class FlatGainFilter: MonoBehaviour {
	void OnAudioFilterRead(float[] data, int channels) {
		for (int i = 0; i < data.Count(); i++) data[i] *= gain;
	}

	public float gain = 1f;
}
