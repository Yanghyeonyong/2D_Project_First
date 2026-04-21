# KingOfSky

King Of The Sky는 Unity 2D 기반의 로그라이크 액션 게임입니다. 플레이어는 여러 스테이지를 진행하며 적을 처치하고, 다양한 능력을 획득하며 성장하는 구조로 설계되어 있습니다. 게임은 상태 머신 기반의 플레이어 시스템, MVC 패턴의 적 시스템, 그리고 다양한 게임 메커니즘으로 구성되어 있습니다.

## 기술 스택

- Unity 2022.3 LTS
- C# 스크립트 기반
- MVC 패턴 (Model-View-Controller)
- 상태 머신 (State Machine) 패턴
- 싱글턴 패턴 (Game Manager)

## 핵심 기술 소개

### 1. 시스템 관리

#### GameManager.cs
- 싱글턴 패턴으로 전체 게임 상태를 관리합니다.
- 현재 스테이지, 플레이어 참조, 플레이어 데이터, BGM 재생을 제어합니다.
- 무적 상태(Invincible) 관리를 통해 플레이어 보호 기능을 제공합니다.

#### SceneChanger.cs
- 씬 전환을 담당하는 매니저입니다.

#### UIManager.cs
- 게임 전체 UI 관리를 수행합니다.

#### AudioManager.cs / SoundManager.cs
- BGM과 효과음 재생을 담당합니다.

### 2. 플레이어 시스템 (MVC 패턴)

#### PlayerModel.cs
- 플레이어 데이터를 관리하는 Model 계층입니다.
- **주요 속성**: 
  - HP/MP, 공격력, 방어력, 이동속도, 점프력
  - 골드, 점수
- **이벤트**: 체력 변화, 점프 발생 시 이벤트를 발생시킵니다.

#### PlayerView.cs
- 플레이어의 UI를 표현하는 View 계층입니다.
- HP, MP 바 등 플레이어 정보를 화면에 표시합니다.

#### PlayerController_State.cs
- 플레이어 입력을 받아 상태를 관리하는 Controller 계층입니다.
- 상태 머신 기반으로 다양한 액션을 제어합니다.

#### PlayerModel_Dongeon.cs
- 던전 진행용 플레이어 데이터 모델입니다.

#### 플레이어 상태 (State 패턴)
- **IdleState**: 정지 상태
- **RunState**: 이동 상태
- **AttackState**: 공격 상태
- **JumpState**: 점프 상태
- **CrouchState**: 웅크리기 상태
- **SkillState**: 스킬 발동 상태

### 3. 적 시스템 (MVC 패턴)

#### EnemyModel.cs
- 적의 데이터를 관리하는 Model 계층입니다.
- HP, 공격력, 방어력, 이동속도 등 적의 기본 정보를 보유합니다.

#### EnemyView.cs
- 적의 UI를 표현하는 View 계층입니다.
- HP 바 등 적의 정보를 시각적으로 표시합니다.

#### EnemyController.cs
- 적의 행동을 제어하는 Controller 계층입니다.
- 적의 초기화, 공격, 피격 등을 관리합니다.

#### EnemySpawner.cs
- 적을 활성화하는 스폰 시스템입니다.
- 해당 스포너에 등록된 적들을 한 번에 활성화합니다.

#### NavAgent2D.cs
- 2D 네비게이션 에이전트로 적의 이동을 관리합니다.

#### 적 행동 (AI Behaviour)
- **DetectPlayerAction**: 플레이어 감지 행동
- **ChasePlayerAction**: 플레이어 추격 행동
- **AttackPlayerAction**: 플레이어 공격 행동
- **PatrolAction**: 패트롤 행동
- **TryInOrderSequence**: 여러 행동을 순차적으로 시도하는 시퀀서

### 4. 투사체 시스템

#### Bullet.cs
- 발사체의 기본 로직을 정의합니다.
- 이동, 충돌 판정, 데미지 처리를 담당합니다.

#### BulletManager.cs
- 발사체들을 관리하는 매니저입니다.
- 발사체의 생성과 폐기를 제어합니다.

### 5. 맵 시스템

#### BackGroundMapPool.cs
- 배경 맵 타일의 풀링을 관리합니다.
- 효율적인 메모리 사용을 위해 맵을 재사용합니다.

### 6. UI 시스템

#### LevelUp.cs
- 플레이어 레벨 업 UI와 로직을 담당합니다.

#### Score.cs
- 점수 표시 및 관리를 담당합니다.

#### Shopping.cs
- 상점 UI와 구매 로직을 담당합니다.

### 7. 데이터 관리

#### GameData.cs
- 게임 전체 데이터를 관리합니다.

#### PlayerData.cs / PlayerData.asset
- 플레이어 기본 설정 데이터를 저장합니다.

#### PlayerData_JSON.cs
- JSON 형식의 플레이어 데이터 저장/로드를 담당합니다.

## 기술 결정 기록

### ADR-001. MVC 패턴 도입

- **배경**: 플레이어와 적의 데이터, UI, 로직이 뒤섞여 있어 유지보수가 어려웠습니다.
- **결정**: Model(데이터), View(UI), Controller(로직)를 분리하였습니다.
- **이유**: 각 계층의 책임을 명확히 하여 확장성과 테스트 용이성을 높였습니다.

### ADR-002. 상태 머신 패턴 (플레이어)

- **배경**: 플레이어의 다양한 액션(대기, 이동, 공격, 점프, 웅크리기, 스킬)을 효율적으로 관리해야 했습니다.
- **결정**: 상태 머신 패턴을 도입하여 각 상태를 별도의 클래스로 구현했습니다.
- **이유**: 상태 간 전환이 명확하고 새로운 상태 추가가 용이합니다.

### ADR-003. AI Behaviour 시스템 (적)

- **배경**: 적의 다양한 행동(감지, 추격, 공격, 패트롤)을 조합해야 했습니다.
- **결정**: 행동(Action) 기반 AI 시스템을 도입하여 행동들을 조합 가능하게 했습니다.
- **이유**: 행동을 조합하면 다양한 적 타입을 쉽게 구성할 수 있습니다.

### ADR-004. 2D 네비게이션 (NavAgent2D)

- **배경**: Unity의 기본 NavMesh는 3D 기반이어서 2D 게임에 부적합했습니다.
- **결정**: 커스텀 NavAgent2D를 구현하여 2D 이동을 관리했습니다.
- **이유**: 2D 환경에 최적화된 네비게이션을 제공할 수 있습니다.

### ADR-005. 싱글턴 패턴 (GameManager)

- **배경**: 게임 전체 상태를 어디서나 접근 가능해야 했습니다.
- **결정**: GameManager를 싱글턴으로 구현했습니다.
- **이유**: 전역 상태 관리가 필요하며, 게임 중 단 하나만 존재해야 합니다.

## 핵심 게임 흐름

1. **게임 시작**: 
   - `GameManager`가 현재 스테이지와 플레이어 데이터를 초기화합니다.
   - 플레이어 프리팹을 인스턴스화합니다.

2. **적 생성**:
   - `EnemySpawner`가 활성화되면 등록된 적들을 모두 활성화합니다.
   - 각 적은 `EnemyController`와 함께 게임 세계에 배치됩니다.

3. **플레이어 조작**:
   - `PlayerController_State`가 입력을 받습니다.
   - 현재 상태에 따라 적절한 `IPlayerState` 구현체가 실행됩니다.
   - `PlayerModel`의 데이터가 변화하고 `PlayerView`가 업데이트됩니다.

4. **적 AI**:
   - `EnemyController`는 등록된 AI Behaviour들을 실행합니다.
   - 플레이어 감지 → 추격 → 공격 순으로 진행됩니다.
   - `NavAgent2D`가 적의 이동을 처리합니다.

5. **전투**:
   - 플레이어와 적이 공격할 때 `Bullet`이 발사됩니다.
   - `BulletManager`가 발사체를 관리합니다.
   - 충돌 시 데미지가 적용되고 `PlayerModel` 또는 `EnemyModel`의 HP가 감소합니다.

6. **진행**:
   - 적을 모두 처치하면 다음 스테이지로 진행합니다.
   - `GameManager`의 `curStage`가 증가하고 새 씬이 로드됩니다.

7. **성장**:
   - 적 처치 시 골드와 경험을 획득합니다.
   - `LevelUp`, `Shopping` 시스템으로 능력을 강화할 수 있습니다.
   - `PlayerData_JSON`에 진행 상황이 저장됩니다.

