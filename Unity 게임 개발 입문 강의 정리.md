## Unity 게임 개발 입문 1주차
### Asset, Scene, GameObject
- __Asset(에셋)__
  - 게임에 필요한 모든 리소스(이미지, 사운드, 모델, 코드)를 의미
  - 프로젝트의 에셋 폴더에 저장되며, 유니티에서 사용 가능한 형식으로 가져와서 게임에 활용
- __Scene(씬)__
  - 게임의 각 장면 또는 화면을 의미
  - 유니티에서 게임은 하나 이상의 씬으로만 구성되며, 각 씬은 게임의 특정 부분을 담당
- __GameObject(게임 오브젝트)__
  - 씬에 배치되는 모든 요소
  - 에셋을 이용하여 씬에 생성되며, 게임의 동작과 상호작용 담당
  - 계층 구조로 구성되며, 부모-자식 관계를 가짐

<br>

### 게임 개발 맛보기 PONG
- Material
  - Sprite : 눈에 보여지는 재질 (색, 모양 등)
  - Rigidbody : 물질적인 재질
- Box Collider - Is Trigger
  - Collision 충돌 : 실제로 충돌이 일어나고 충돌 감지
  - Trigger 충돌 : 실제 충돌은 일어나지 않지만 충돌 감지
- Canvas Scaler에서 Scale Mode는 __Scale With Screen Size__로 설정하는 것 잊지 말기!
  - 사이즈는 x 1920 y 1080
  
  
<br>

### 스크립트 라이프 사이클
- __Awake__ : 게임 오브젝트가 생성될 때 호출
- __Start__ : 게임 오브젝트가 활성화되어 게임 루프가 시작될 때 호출
- __Update__ : 매 프레임마다 호출
- __FixedUpdate__ : 물리 엔진 업데이트 시 호출
- __LateUpdate__ : Update 메서드 호출 이후에 호출
- __OnEnable__ : 게임 오브젝트가 활성화될 때 호출
- __OnDisable__ : 게임 오브젝트가 비활성화될 때 호출
- __OnDestroy__ : 게임 오브젝트가 파괴될 때 호출

<br>

### 컴포넌트
- 게임 오브젝트에 부착되는 독립적인 기능 모듈
- 각 컴포넌트는 특정한 작업을 수행하거나 기능을 제공
- __Transform__ : 게임 오브젝트의 위치, 회전, 크기 조정
- __Rigidbody__ : 물리적인 효과를 게임 오브젝트에 적용
- __Collider__ : 충돌 감지를 처리
- __SpriteRenderer__ : 2D 그래픽을 표시하는데 사용
- __AudioSource__ : 사운드를 재생

### TopDown Shooting
- PPU (Pixels Per Unit) 값이 높아질수록 고해상도 -> 성능에 영향
- 부모-자식 관계를 갖는 오브젝트는 부모의 좌표, 크기, 회전 값이 적용됨
- World 좌표, Local 좌표
  - Local 좌표는 부모로부터의 상대적 좌표
  - 부모가 없는 오브젝트는 World 좌표와 Local 좌표가 같음
- Time.deltaTime
  - 이전 프레임과 현재 프레임 사이의 시간
  - 프레임이 다른 컴퓨터 간 이동속도를 맞춰주기 위해 사용
  ```cs
  void Update() {
    float x = Input.GetAxis("Horizontal");
    float y = Input.GetAxis("Vertical");
    
    transform.position += new Vector3(x, y) * Time.deltaTime;
  }
  ```
- GetAxisRaw : 0, 1, -1의 값만 줌
  - GetAxis는 0부터 1까지 도달하는 값을 줌
- 인스펙터 창에서 값을 조절하려면 public으로 선언
  ```cs
  public float speed = 5f;
  ```
  - 중요한 값일 때는 SerializedField를 사용해서 동기화
    ```cs
    [SerializedField] private float speed = 5f;
    ```
- __Input System__
  - Package Manager에서 다운
  - Input 폴더 안에서 Create - InputActions 
  - Keyboard랑 Mouse 추가한 Scheme 생성
  - Action Properties - ActionType은 Value, Control Type은 Vector2로 설정
  - + Up/Down/Left/Right Composite 추가
  - Path에 방향키 설정 WASD
 
- normalized
  - 단위 벡터로 만듦
  - UP 키와 Down 키를 동시에 누르면 대각선 방향으로 가게 되는데, normarlize를 안 하면 대각선 방향만 빠르게 움직이게 됨
  ```cs
  Vector2 moveInput = value.Get<Vector2>().normalized;
  ```
- WorldPoint 좌표 구하기
  - 어떤 벡터 A에서 다른 벡터 B를 빼면 B -> A 로 향하는 값이 나옴
  - 그 벡터를 normalized -> 단위 벡터로 만듦 : 나한테서 마우스 포인터를 바라보는 방향
  - newAim.magnitude : 벡터의 크기 (단위 벡터이므로 1)
  ```cs
  Vector2 newAim = value.Get<Vector2>();
  Vector2 worldPos = _camera.ScreenToWorldPoint(newAim);
  newAim = (worldPos - (Vector2)transform.position).normalized;
  
  if (newAim.magnitude >= .9f)
  {
    CallLookEvent(newAim);
  }
  ```
- __GetComponent__ : 현재 스크립트가 달려있는 같은 오브젝트에서 찾아오겠다는 의미

- FixedUpdate : 보통의 물리처리가 끝난 후에 호출됨 -> Update보다 호출 느림

- __Tilemap__
  - Tilemap Gameobject : 타일맵 구조를 구성하는데 사용
    - Tilemap Grid의 자식으로 위치, 특정 타일의 배치를 관리
  - Grid GameObject : 모든 타일맵이 위치하는 기본 격자를 나타냄
  - Tilemap Renderer : Tilemap의 모양을 실제로 그리는 역할
  - Tilemap Collider 2D : Tilemap에 물리적 경계를 추가하는데 사용
    - 게임 캐릭터가 타일맵 환경과 상호작용 가능
  - Tile Assets : 개별 타일의 모양과 동작을 정의
    - Tileset : 여러 개의 타일

- Atan2
  - x, y에 대하여 arctan(아크탄젠트, 탄젠트의 역삼각함수) 값을 구함 -> 벡터의 각도
  - Atan에서 y/0일 때 버그가 발생하는 것을 방지하여 인자를 두 개 받는 Atan2가 생김
  - Mathf.Rad2Deg를 곱해서 radian 값을 degree 값으로 변환 -> Euler 함수 사용 가능
  ```cs
  float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
  ```
  
- 캐릭터를 기준으로 활 각도의 절댓값이 90을 넘어가면 활을 뒤집음 -> 캐릭터의 방향도 뒤집음
  - flipY는 Y축을 방향으로 뒤집는 함수
  ```cs
  armRenderer.flipY = Mathf.Abs(rotZ) > 90f;
  characterRenderer.flipX = armRenderer.flipY;
  armPivot.rotation = Quaternion.Euler(0, 0, rotZ); // 실제로 무기 회전
  ```
  
- Awake에서 Component에 대한 준비를 하고, 그 이후에 Start나 다른 함수에서 사용하는 것이 좋음