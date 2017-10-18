#!/bin/sh
mcs -lib:"$KSP_ROOT/KSP_Data/Managed" -langversion:6 -r:Assembly-CSharp.dll -r:Assembly-CSharp-firstpass.dll -r:UnityEngine.dll -r:UnityEngine.UI.dll -r:mscorlib.dll -t:library -o:SonicRealism.dll *.cs
