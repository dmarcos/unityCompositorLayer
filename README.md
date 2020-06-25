## OVRLayer Simple Example

<img src="https://user-images.githubusercontent.com/39342/85676164-34208300-b67b-11ea-9b9d-8ba3ccd0f333.jpg" width="320">

Simple example of a compositor layer displaying a large texture (1988x3056) in Unity.

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
