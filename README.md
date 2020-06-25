## OVRLayer Simple Example

Simple example of a compositor layer displaying a large texture (1988 3056) in Unity.

Controls:

- Thumbstick to recenter layer.
- Thumbstick up / down to move layer farther / closer.
- A button to toggle layer super sampling.

## Code

`adb logcat | findstr XXX` to see logging messages.

Relevant logic is contained in two files:

### [OverlayControls.cs](https://github.com/dmarcos/unityCompositorLayer/blob/master/unity/Assets/OverlayControls.cs#L43)

Input controls and texture initialization passed to OVROverlay. 

Set Mip Maps component property to `true` to generate texture mip maps.

### [OVROverlay.cs](https://github.com/dmarcos/unityCompositorLayer/blob/master/unity/Assets/Oculus/VR/Scripts/OVROverlay.cs) (Supplied by Oculus)

It initializes and submits layers.
