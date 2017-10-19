# Sonic Realism — a mod for Kerbal Space Program

Modifies the in-flight sound to be more realistic:
* Makes the amplitude what an actual comoving listener would hear at the position of the camera — if the craft is supersonic, you will not hear any sound outside the shock cone, and will hear louder close to the shock
* Mutes the sound by atmospheric density — the thinner the air, the quieter the sound
* Has no effect in IVA mode

The following shell scripts all assume `KSP_ROOT` is the directory where KSP is installed.

## Building

```sh
./build.sh
```

## Installing

```sh
cp SonicRealism.dll $KSP_ROOT/Plugins/
```

## Feature Goals

* Per-frequency Stokes attenuation filter, including effect of airspeed on effective path length
