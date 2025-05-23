# Sparta_Skills(스파르타 숙련주차 개인과제)
스파르타 숙련주차 개인과제 SpartaDundgeon입니다

## 프로젝트 소개  
 - 개발 기간 2025. 05.16 ~ 25.5.23 16:00까지
- SpartaDungeon 개발을 통해 Unity3D에 해대 학습하고 Unity3D의 캐릭터 이동과 물리 처리를 직접 구현
- 참고 게임 : Super Mario Wii Galaxy Adventure, 강의 내용 - 3D Survival

## 필수 기능

<details><summary><h3>기본 이동 및 점프</h3></summary> 

 - PlayerController와 InputSystem을 이용해서 작성
강의 내용에 있는 내용을 기초로 OnMove와 OnLook을 사용하여 구현
 - WASD로 이동/ 마우스 위치로 시선 고정
</details>


<details><summary><h3>체력바 UI
</h3></summary>

 - 바 형태의 체력바가 아닌 참고 게임 SuperMario Wii GalaxyAdventure의 원형 체력을 사용하여 대미지 구현
 - 체력이 닳을 때 색깔이 바뀌는 것은 구현하지 못한 것이 아쉬움
</details>

<details><summary><h3>동적 환경 조사
</h3></summary>

 - 강의 내용에 Ray와 Raycast를 사용하여 마우스 방향에 있는 아이템 오브젝트의 정보를 전달하는 패널 구현
 - CrossHair는 구현하지 않아 조준에 어려움이 있을 수 있음
</details>

<details><summary> <h3>점프대
</h3></summary>

 - RigidBody와 ForceMode Impulse를 사용하여 순간적으로 높은 점프력을 낼 수 있는 점프대를 구현
 
</details>


<details><summary><h3>아이템 데이터
</h3></summary>

 -  강의에 나온 내용을 이용하여 ScriptableObject를 이용하여 아이템 데이터의 구성을 함

 - 아이템의 종류를 나누는 데 어려움을 겪음
</details>

<details><summary><h3>아이템 사용
</h3></summary>

 - 동적인 인벤토리를 사용하지 않고 passive형태로 왼쪽 상단에 Consumabl 아이템 보유 여부를 나타내어 줌
 -  키보드의 1, 2, 3을 입력을 이용해 각 칸에 있는 아이템을 사용
 - 인벤토리 - 마우스로 사용을 하지 않고 키보드 입력을 이용해 만드는 과정이 까다로웠음

</details>


## 도전 기능

<details><summary><h3>추가UI
</h3></summary>

 - 좌측 상단에 나타내는 UI는 Consumable아이템에 대한 정보를 나타내고
 - 화면 하단에 SuperMario에 쓰이는 코인, 수집형 Star, 잔여 Life를 나타낼 수 있게 UI 제작

</details> 

<details><summary> <h3>3인칭 시점
</h3></summary>

 - CameraContainer에 TPSCamera를 추가하여 3인칭 시점을 구현
 - PlayerController의 Look을 최대한 그대로 사용하여 마인크래프트의 3인칭 모드처럼 보는 각도가 1인칭과 거의 유사하게 유지되어 어색함을 줄임

</details> 

<details><summary> <h3>다양한 아이템 구현
</h3></summary>

 - 플레이어를 PowerUp하게 하는 버섯과 꽃 아이템
 - 먹는 즉시 체력을 회복시켜주거나 업적을 달성하는 데 필요한 Resource아이템

</details>

<details><summary> <h3>애니메이션
</h3></summary>

 - 플레이어는 구체적인 오브젝트가 없으므로 강의 영상에 EquipPrefab의 애니메이션과 카메라를 이용하여 구현
 - HandCamera는 DepthOnly상태로 Sphere두개를 붙여 손처럼움직이게 animation 제작

</details>




