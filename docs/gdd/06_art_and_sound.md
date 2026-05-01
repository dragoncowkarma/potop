## **6. 아트 및 사운드 기획 (Art & Sound)**

### **🎨 시각적 스타일 및 제작 공정 (Art Production)**
* **핵심 컨셉:** **"Neon Cyber Minimalism"**
  * 고대비 네온 컬러와 단순화된 기하학적 형태의 결합.
  * 광원 효과(Bloom)와 잔상(Trail)을 활용한 속도감 강조.
* **제작 파이프라인:**
  * **Modeling:** Blender를 사용한 Low-poly 모델링. 텍스처는 가급적 배제하고 **Emission Color**와 **Vertex Color** 위주로 작업.
  * **Shaders:** Unity URP **Shader Graph**를 활용한 커스텀 쉐이더 제작. (Rim Light, Glitch, Dissolve 이펙트 구현)
  * **VFX:** **VFX Graph**와 **Particle System** 혼용. 적 파괴 시 발생하는 파편은 GPU 기반 Particle로 수천 개를 효율적으로 연출.
  * **UI:** Adobe XD/Figma 디자인 후 **UI Toolkit (USS/UXML)**으로 구현.

### **🎵 사운드 디자인 및 오디오 파이프라인 (Audio Production)**
* **음악 컨셉:** 
  * **Adaptive Music:** 웨이브 Phase에 따라 BGM의 레이어가 추가되거나 템포가 변경됨 (Phase 1: Lo-fi -> Phase 4: High-tempo EDM).
  * **Overclock Mode:** 저음역대 베이스와 사이렌을 강조하여 긴박감 극대화.
* **효과음(SFX) 제작:**
  * **사운드 소스:** 합성(Synthesizer) 기반의 기계음과 타격음 위주로 구성.
  * **타격감:** 샷건 형태의 공격에는 하이햇과 킥 사운드를 섞어 리듬감 있는 타격 피드백 제공.
* **구현 방식:**
  * **Audio Mixer:** Master, BGM, SFX 그룹으로 분리하여 실시간 볼륨 및 Pitch 제어.
  * **Spatial Audio:** 3D 공간 음향은 지양하고, 하이퍼캐주얼의 직관성을 위해 2D/Stereo 위주로 믹싱.
