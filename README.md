
![header](https://capsule-render.vercel.app/api?type=waving&color=gradient&customColorList=3&height=300&section=header&text=GATHER%20CLONE&fontSize=90&fontColor=FFF)

## 목차

| [🌷 프로젝트 미리 보기 🌷](#프로젝트-미리-보기) |
| :---: |
| [🌴 기능별 코드 보기 🌴](#기능별-코드) |
| [✏ DEVELOP ✏](#develop) |

<br>

* * *

## 프로젝트 미리 보기

[🌳 목차로 돌아가기 🌳](#목차)

- Start Scene
  - 닉네임, 캐릭터 선택
  ![startscene_닉네임_캐릭터_입력](https://github.com/j-miiin/Unity_Study_Sparta_2023/assets/62470991/0e9a7ef3-a89f-4fd5-8ca3-0bc86394139c)

- Main Scene
  - 맵 UI - 애니메이션 타일 사용
  ![mainscene_맵_ui](https://github.com/j-miiin/Unity_Study_Sparta_2023/assets/62470991/4a4fbcd7-eaeb-4f72-8554-40f677af4249)
  - 캐릭터 애니메이션
  ![mainscene_캐릭터_애니메이션_1](https://github.com/j-miiin/Unity_Study_Sparta_2023/assets/62470991/d9f65b40-3328-4e17-864f-003687aa5f63)
  ![mainscene_캐릭터_애니메이션_2](https://github.com/j-miiin/Unity_Study_Sparta_2023/assets/62470991/3a30d0e3-9acc-40e8-a468-79363134c2f5)
  - 참석 인원 보여주기
  ![mainscene_참석자_리스트](https://github.com/j-miiin/Unity_Study_Sparta_2023/assets/62470991/a9d83011-ccd3-4738-a47a-b2611582a23f)
  - 인게임 닉네임, 캐릭터 선택
  ![mainscene_인게임_닉네임_캐릭터_변경](https://github.com/j-miiin/Unity_Study_Sparta_2023/assets/62470991/1e6dc145-b64f-4acc-9425-2c119ce3dbb7)
  - NPC 대화
  ![MyScreenShot_0907_102444](https://github.com/j-miiin/Unity_Study_Sparta_2023/assets/62470991/5e0561f1-fa7d-481b-9e97-14d5d9bfe903)
  ![MyScreenShot_0907_102447](https://github.com/j-miiin/Unity_Study_Sparta_2023/assets/62470991/cac00ac5-2dae-4f46-8fef-4f5079d53d4d)
  ![MyScreenShot_0907_102505](https://github.com/j-miiin/Unity_Study_Sparta_2023/assets/62470991/d10bfe5e-e11d-4710-865d-6293be1f8b6e)
  ![MyScreenShot_0907_102508](https://github.com/j-miiin/Unity_Study_Sparta_2023/assets/62470991/f5e168fd-5ef3-4d7c-93aa-1780ee508508)
  - 카메라 기능 <br>
    ![screenshots_folder](https://github.com/j-miiin/Unity_Study_Sparta_2023/assets/62470991/cc6f932c-8f8a-401a-a570-9e83e5e2bb84)

<br>

* * *

<br>

## 기능별 코드

[🌳 목차로 돌아가기 🌳](#목차)

- Start Scene

| Script | 기능 |
| :---: | :---: |
| GetPlayerName  | [닉네임 입력 받기](https://github.com/j-miiin/Unity_Study_Sparta_2023/blob/3699bedab8d59c69838117944da01a65f7b4caa7/Assignment_Gather_Clone/Assets/Scripts/StartScene/GetPlayerName.cs#L8C14-L38) |
| PlayerCharacter  | [캐릭터 선택하기](https://github.com/j-miiin/Unity_Study_Sparta_2023/blob/3699bedab8d59c69838117944da01a65f7b4caa7/Assignment_Gather_Clone/Assets/Scripts/StartScene/PlayerCharacter.cs#L6C14-L40) |

<br>

- Main Scene

| Script | 기능 |
| :---: | :---: |
| CameraManager | [플레이어를 따라 카메라 이동](https://github.com/j-miiin/Unity_Study_Sparta_2023/blob/3699bedab8d59c69838117944da01a65f7b4caa7/Assignment_Gather_Clone/Assets/Scripts/MainScene/CameraManager.cs#L17-L28) |
| UIManager  | [참석 인원 보여주기](https://github.com/j-miiin/Unity_Study_Sparta_2023/blob/09685b7ee5458994f5ff0ca0bf75a78a6d2b28cc/Assignment_Gather_Clone/Assets/Scripts/MainScene/UIManager.cs#L147-L170) |
| GameManager | [참석 인원 보여주기 - GameManager - NPC 오브젝트 생성](https://github.com/j-miiin/Unity_Study_Sparta_2023/blob/3699bedab8d59c69838117944da01a65f7b4caa7/Assignment_Gather_Clone/Assets/Scripts/MainScene/GameManager.cs#L26-L38) |
| UIManager | [인게임에서 닉네임 변경하기](https://github.com/j-miiin/Unity_Study_Sparta_2023/blob/3699bedab8d59c69838117944da01a65f7b4caa7/Assignment_Gather_Clone/Assets/Scripts/MainScene/UIManager.cs#L185-L195) |
| | [플레이어 캐릭터 위에 닉네임 보여주기](https://github.com/j-miiin/Unity_Study_Sparta_2023/blob/3699bedab8d59c69838117944da01a65f7b4caa7/Assignment_Gather_Clone/Assets/Scripts/MainScene/UIManager.cs#L90) |
| Player | [인게임에서 닉네임 변경하기 - Player](https://github.com/j-miiin/Unity_Study_Sparta_2023/blob/3699bedab8d59c69838117944da01a65f7b4caa7/Assignment_Gather_Clone/Assets/Scripts/MainScene/Player.cs#L24-L28) |
| UIManager | [인게임에서 캐릭터 변경하기](https://github.com/j-miiin/Unity_Study_Sparta_2023/blob/3699bedab8d59c69838117944da01a65f7b4caa7/Assignment_Gather_Clone/Assets/Scripts/MainScene/UIManager.cs#L197-L207) |
| Player | [인게임에서 캐릭터 변경하기 - Player](https://github.com/j-miiin/Unity_Study_Sparta_2023/blob/3699bedab8d59c69838117944da01a65f7b4caa7/Assignment_Gather_Clone/Assets/Scripts/MainScene/Player.cs#L30-L49) |
| UIManager | [시간 표시하기](https://github.com/j-miiin/Unity_Study_Sparta_2023/blob/3699bedab8d59c69838117944da01a65f7b4caa7/Assignment_Gather_Clone/Assets/Scripts/MainScene/UIManager.cs#L88-L89) |
| Npc | [NPC와 대화하기](https://github.com/j-miiin/Unity_Study_Sparta_2023/blob/3699bedab8d59c69838117944da01a65f7b4caa7/Assignment_Gather_Clone/Assets/Scripts/MainScene/NpcScripts/Npc.cs#L18-L45) |
| UIManager | [NPC 대화 창 UI](https://github.com/j-miiin/Unity_Study_Sparta_2023/blob/3699bedab8d59c69838117944da01a65f7b4caa7/Assignment_Gather_Clone/Assets/Scripts/MainScene/UIManager.cs#L209-L226) |
| CameraManager | [사진 찍기](https://github.com/j-miiin/Unity_Study_Sparta_2023/blob/3699bedab8d59c69838117944da01a65f7b4caa7/Assignment_Gather_Clone/Assets/Scripts/MainScene/CameraManager.cs#L30-L57) |

<br>

## DEVELOP
### 더 구현하고 싶은 기능 및 코드 리팩토링
[🌳 목차로 돌아가기 🌳](#목차)
- 닉네임 입력 및 NPC 대화시 캐릭터 이동 제한
- 캐릭터 이동 (앞, 뒤) 애니메이션 추가
- INPC 인터페이스를 IInteractable 같은 행동과 관련된 인터페이스로 변경
- 카메라 촬영 기능 이펙트 추가
- Map UI 추가 -> 집 모양 타일로 NPC 구역 꾸미기
- 미니맵 기능 추가
- NPC 대화 또는 퀘스트 추가