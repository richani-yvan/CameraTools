# 🎥 Advanced Camera Systems for Unity

A collection of **modular camera systems** built for responsive gameplay and better visual feedbacks. 
This repository focuses on **camera behavior**, emphasizing smooth motion, readable framing, and expressive feedback without hard snapping or jitter.

---

## ✨ Included Systems

- **Asymptotic Camera Follow**
- **Perlin Noise based Camera Shake**
- **Dynamic Camera Framing for Points of Interest**

Each system is **independent** and designed to be **ready for use upon import**

---

## 📌 Asymptotic Camera Follow

### What it does
Smoothly follows a target using **weighted asymptotic equations**; the camera approaches the target *asymptotically*, without ever instantly snapping to it.

### Why asymptotic follow?
Traditional `Lerp`-based camera movement often:
- Does not adapt to target speed
- Can be prone to snaps or jitters
- Behaves inconsistently with different frame rates

### System Key Features
- Usable in **both 2D and 3D**
- **frame-rate independent**, works when **TimeScale != 1**
- Organic **adaptive follow speed**: slower when target is near, speeds up the farther it is
- Configurable positional offset and convergence weights 

---

## 📌 Perlin Noise based Camera Shake

### What it does
Generates **smoother, more organic camera shake** using Perlin noise instead of pure randomness.

### Why Perlin Noise?
- Produces shake motions that are more believable, and less jittery than traditional random camera shake
- Allows for different types of effects; dizziness, chaotic, stylized shakes...
- Works with both positional and rotational camera shakes, or both

### System Key Features
- Usable in **both 2D and 3D**
- Highly customizable parameters: Shake frequency and intensity, attenuation speeds, axis constraints (such as forward-only shake)
- Ability to save **parameter presets** in **`ShakeProfile` ScriptableObjects**, to be reused on iterated on **without code changes**

---

## 📌 Dynamic Camera Framing for Points of Interest

### What it does
Automatically adjusts the camera position to keep multiple **points of interest** in screen view. Works via weighted averaging of the Points of Interest based on their **importance** value, and their **distance** to the camera. 

### Why Dynamic Framing?
- Use cases include multiplayer gameplay, boss encounters, puzzles...
- Can create cinematic closeups by lerping a Point of Interest's Importance value
- Subtly guides the player's attention without a "handholding" feeling

### System Key Features
- Usable in **both 2D and 3D**
- Feels more intentional and avoids erratic movement when paired with **Asymptotic Camera Follow**
- Supports the changing of parameters mid-gameplay, such as Point of Interest Importance or the Camera's distance thresholds for ignoring/detecting points of interest

---

## 🧠 Design Philosophy

These systems were built with the following principles in mind:

- **Modularity** — each system can be used independently, and immediately upon import
- **Smoothness** — avoid jitter and snapping, friendly towards player feedbacks and experience
- **High flexibility** — many customizable public parameters 

---

## 🧪 Usage

Each system can be dropped into an existing project with minimal setup. Feel free to make use of the already existing prefabs from the .unitypackage
Public parameters are exposed for tuning in Prefabs and Scriptables.
No dependencies beyond Unity’s standard components.

---

