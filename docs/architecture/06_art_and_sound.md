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

#### **에셋 네이밍 규칙 (Naming Convention)**
에셋 관리 효율성을 위해 아래의 접두사(Prefix) 규칙을 엄격히 준수합니다.
* **Prefabs:** `PFB_[Category]_[Name]` (예: `PFB_Turret_Guardian`)
* **Materials:** `MAT_[Category]_[Name]` (예: `MAT_Turret_NeonBlue`)
* **Textures:** `TEX_[Name]_[Type]` (예: `TEX_Scouter_Emission`)
* **Shaders:** `SHD_[Name]` (예: `SHD_Dissolve`)
* **Audio:** `SFX_[Category]_[Name]` / `BGM_[State]_[Name]` (예: `SFX_Weapon_Railgun`)
* **VFX:** `VFX_[Category]_[Name]` (예: `VFX_Hit_MuzzleFlash`)

### **🎵 사운드 디자인 및 오디오 파이프라인 (Audio Production)**
* **음악 컨셉:** 
  * **Adaptive Music:** 웨이브 Phase에 따라 BGM의 레이어가 추가되거나 템포가 변경됨 (Phase 1: Lo-fi -> Phase 4: High-tempo EDM).
  * **Overclock Mode:** 저음역대 베이스와 사이렌을 강조하여 긴박감 극대화.
* **효과음(SFX) 제작:**
  * **사운드 소스:** 합성(Synthesizer) 기반의 기계음과 타격음 위주로 구성.
  * **타격감:** 샷건 형태의 공격에는 하이햇과 킥 사운드를 섞어 리듬감 있는 타격 피드백 제공.
* **구현 방식:**
  * **Audio Mixer:** Master, BGM, SFX 그룹으로 분리하여 실시간 볼륨 및 Pitch 제어.
  * **Spatial Audio:** 기본 2D 스테레오. VR 빌드에서는 Spatial Audio 활성화.
  * **Voice Budget:** 동일 SFX의 동시 재생 수를 제한하여 다중 처치/연사 상황에서도 소리 뭉개짐과 CPU 스파이크를 방지.
  * **Pooling:** 전투 SFX는 사전 준비된 `AudioSource` 풀을 사용하며, 런타임 핫패스에서 오브젝트를 생성하지 않음.

### **✨ Phase 8+ VFX 품질 기준 (VFX Quality Gates)**
* **가독성:** 보스 페이즈 전환, 적 사망, 오버클럭 진입 연출은 HUD와 공격 텔레그래프를 가리지 않아야 합니다.
* **모바일 폴백:** Bloom, Trail, Particle Density는 Low/Medium/High 품질 단계로 조정 가능해야 합니다.
* **풀 반환:** 파티클 시스템 반환 시 입자, 트레일, 서브 이미터, 라이트, 임시 머티리얼 상태를 초기화합니다.
* **예산:** 동시 활성 VFX 파티클은 `10,000`개 이하를 목표로 하며, 초과 연출은 GPU 기반 잔상/파편으로 대체합니다.
