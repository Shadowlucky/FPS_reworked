# FPS Reworked

## Русский

**FPS Reworked** — учебный single-player FPS-проект на **Unity**, созданный для демонстрации навыков разработки игровых систем на **C#**.

Проект включает боевую систему, несколько типов противников, навигацию на основе **NavMesh**, босса с несколькими фазами, интерактивные объекты и базовые игровые системы.

> Проект создан как демонстрационный портфолио-проект.

---

### 🎮 Демонстрация

#### Gameplay

[Смотреть видео на OneDrive](https://1drv.ms/v/c/3fb6aa97da445fa8/IQCvyrOozujSS6rd684tQEjoAXgqd2T7NsT8g5XEMSFiETk)

#### Готовая сборка

Готовая Windows-сборка доступна в [релизе](https://github.com/Shadowlucky/FPS_reworked/releases/tag/1.0.0).

Сборка позволяет ознакомиться с проектом без необходимости открывать исходники в Unity.

---

### ✨ Основные возможности

#### Игрок

* FPS-контроллер на основе `CharacterController`.
* Передвижение, прыжок и гравитация.
* Бег и ходьба.
* Управление камерой мышью.
* Переключение оружия.
* Использование аптечек.
* Взаимодействие с игровыми объектами.

Unity `CharacterController` используется для перемещения персонажа с учётом столкновений без необходимости управлять движением игрока через `Rigidbody`.

#### Оружие и стрельба

В игре реализованы два вида оружия:

* пистолет;
* автомат.

Основные возможности:

* физические снаряды на основе `Rigidbody`;
* система боезапаса;
* перезарядка;
* переключение оружия;
* различные параметры оружия;
* направление стрельбы относительно камеры игрока.

#### Здоровье и урон

* Универсальная система здоровья.
* Получение и нанесение урона.
* Смерть персонажей.
* Лечение с помощью аптечек.
* Индикаторы здоровья противников.
* Отдельная настройка размера индикатора здоровья для разных типов врагов.

#### Враги и AI

В игре представлены:

* турели;
* дроны;
* босс.

Для перемещения противников используется **Unity NavMesh**. Навигационная система Unity позволяет создавать NavMesh и использовать NavMesh Agent для перемещения персонажей по проходимым областям уровня.

Противники используют заданные точки перемещения, а боевые объекты дополнительно используют системы наведения и стрельбы.

#### Босс

Босс имеет отдельную систему фаз.

При снижении здоровья до определённого значения начинается вторая фаза:

1. Появляются дополнительные дроны.
2. Активируется защитный щит.
3. Босс продолжает бой вместе с дополнительными противниками.
4. После уничтожения всех дронов щит отключается.

Таким образом, бой с боссом состоит из нескольких связанных игровых механик, а не только из обычного уменьшения полоски здоровья.

#### Взаимодействие

Для взаимодействия с игровыми объектами используется интерфейс `IInteractable`.

Система применяется к различным интерактивным объектам, включая:

* аптечку;
* лифт;
* точку завершения уровня.

Игрок должен получить ключ-карту и вернуться к точке выхода для завершения уровня.

#### Игровое окружение

* Интерактивные двери.
* Лифт.
* Ключ-карта.
* Точки перемещения противников.
* Триггеры взаимодействия.

#### Игровой интерфейс

* Главное меню.
* Pause.
* Экран победы.
* Экран поражения.
* Отображение боезапаса.
* Отображение количества аптечек.
* Индикаторы здоровья.
* Подсказки взаимодействия.
* Индикация получения ключ-карты.

---

### 🎮 Управление

| Действие             | Клавиша      |
| -------------------- | ------------ |
| Движение             | `W A S D`    |
| Обзор                | Мышь         |
| Прыжок               | `Space`      |
| Бег                  | `Left Shift` |
| Ходьба               | `Left Ctrl`  |
| Стрельба             | ЛКМ          |
| Перезарядка          | `R`          |
| Использовать аптечку | `H`          |
| Взаимодействие       | `E`          |
| Пистолет             | `1`          |
| Автомат              | `2`          |
| Пауза                | `Esc`        |

---

### 🛠️ Технологии

* **Unity 2022.3.62f3**
* **C#**
* **Unity NavMesh**
* **TextMesh Pro**

---

### 📁 Структура проекта

Основная структура `Assets` организована следующим образом:

```text
Assets/
├── Art/
│   ├── Audio/
│   ├── Fonts/
│   ├── Images/
│   ├── Materials/
│   ├── Models/
│   └── Textures/
│
├── TextMesh Pro/
│
├── ThirdParty/
│
└── _Project/
    ├── Animations/
    ├── Prefabs/
    ├── Scenes/
    └── Scripts/
```

#### `Art`

Содержит основные художественные ресурсы проекта: аудио, шрифты, изображения, материалы, модели и текстуры.

#### `ThirdParty`

Содержит сторонние ассеты, используемые для оформления окружения, оружия, интерфейса и игровых объектов.

#### `_Project`

Содержит непосредственно проектные ресурсы:

* анимации;
* префабы;
* игровые сцены;
* C#-скрипты.

---

### 🧩 Основные скрипты

| Скрипт             | Назначение                                                         |
| ------------------ | ------------------------------------------------------------------ |
| `Player.cs`        | Управление игроком, движением, оружием и игровыми взаимодействиями |
| `Fire.cs`          | Стрельба и перезарядка                                             |
| `Health.cs`        | Система здоровья, урона и лечения                                  |
| `Boss.cs`          | Управление фазами босса, щитом и дронами                           |
| `MoveEnemy.cs`     | Перемещение противников между заданными точками                    |
| `Interactor.cs`    | Поиск и обработка доступных взаимодействий                         |
| `IInteractable.cs` | Общий интерфейс интерактивных объектов                             |
| `Elevator.cs`      | Управление лифтом                                                  |
| `EndTrigger.cs`    | Завершение уровня                                                  |
| `Medkit.cs`        | Взаимодействие с аптечкой                                          |
| `GameManager.cs`   | Глобальное состояние игры и параметры текущего оружия              |
| `Settings.cs`      | Настройки движения и управления                                    |

---

### ▶️ Запуск проекта из исходников

#### Требования

* Unity Hub
* Unity **2022.3.62f3**

#### Запуск

1. Клонировать репозиторий.
2. Открыть проект через Unity Hub.
3. Выбрать Unity **2022.3.62f3**.
4. Открыть игровую сцену из `_Project/Scenes`.
5. Запустить сцену через Unity Editor.

Необходимые зависимости проекта находятся в `Packages`.

---

### 📌 Статус проекта

**Версия:** `1.0.0`
**Статус:** завершённый демонстрационный проект.

Основная цель проекта — продемонстрировать навыки разработки игровых механик, работы с Unity и организации C#-кода.

---

### 👤 Автор

**Shadowlucky**

[GitHub](https://github.com/Shadowlucky)

---

## English

**FPS Reworked** is an educational single-player FPS project built with **Unity**, created to demonstrate game system development skills in **C#**.

The project includes a combat system, several enemy types, **NavMesh**-based navigation, a multi-phase boss, interactive objects, and basic game systems.

> The project was created as a demonstration portfolio project.

---

### 🎮 Demonstration

#### Gameplay

[Watch the video on OneDrive](https://1drv.ms/v/c/3fb6aa97da445fa8/IQCvyrOozujSS6rd684tQEjoAXgqd2T7NsT8g5XEMSFiETk)

#### Ready build

A ready Windows build is available in the [release](https://github.com/Shadowlucky/FPS_reworked/releases/tag/1.0.0).

The build allows you to get acquainted with the project without the need to open the source files in Unity.

---

### ✨ Key features

#### Player

* FPS controller based on `CharacterController`.
* Movement, jumping, and gravity.
* Sprinting and walking.
* Mouse camera control.
* Weapon switching.
* Medkit usage.
* Interaction with game objects.

Unity `CharacterController` is used to move the character with collision detection without the need to control player movement via `Rigidbody`.

#### Weapons and shooting

The game features two types of weapons:

* pistol;
* assault rifle.

Key features:

* physical projectiles based on `Rigidbody`;
* ammunition system;
* reloading;
* weapon switching;
* various weapon parameters;
* shooting direction relative to the player's camera.

#### Health and damage

* Universal health system.
* Receiving and dealing damage.
* Character death.
* Healing with medkits.
* Enemy health indicators.
* Separate configuration of health indicator size for different enemy types.

#### Enemies and AI

The game features:

* turrets;
* drones;
* a boss.

**Unity NavMesh** is used for enemy movement. Unity's navigation system allows creating a NavMesh and using NavMesh Agent to move characters across walkable areas of the level.

Enemies use predefined movement points, and combat objects additionally use targeting and shooting systems.

#### Boss

The boss has a separate phase system.

When health drops to a certain value, the second phase begins:

1. Additional drones appear.
2. A protective shield is activated.
3. The boss continues to fight along with additional enemies.
4. After all drones are destroyed, the shield is disabled.

Thus, the boss fight consists of several interconnected game mechanics, not just a regular health bar reduction.

#### Interaction

The `IInteractable` interface is used for interaction with game objects.

The system is applied to various interactive objects, including:

* medkit;
* elevator;
* level completion point.

The player must obtain a keycard and return to the exit point to complete the level.

#### Game environment

* Interactive doors.
* Elevator.
* Keycard.
* Enemy movement points.
* Interaction triggers.

#### Game UI

* Main menu.
* Pause.
* Victory screen.
* Defeat screen.
* Ammo display.
* Medkit count display.
* Health indicators.
* Interaction prompts.
* Keycard acquisition indication.

---

### 🎮 Controls

| Action              | Key          |
| ------------------- | ------------ |
| Movement            | `W A S D`    |
| Look                | Mouse        |
| Jump                | `Space`      |
| Sprint              | `Left Shift` |
| Walk                | `Left Ctrl`  |
| Shoot               | LMB          |
| Reload              | `R`          |
| Use medkit          | `H`          |
| Interact            | `E`          |
| Pistol              | `1`          |
| Assault rifle       | `2`          |
| Pause               | `Esc`        |

---

### 🛠️ Technologies

* **Unity 2022.3.62f3**
* **C#**
* **Unity NavMesh**
* **TextMesh Pro**

---

### 📁 Project structure

The main structure of `Assets` is organized as follows:

```text
Assets/
├── Art/
│   ├── Audio/
│   ├── Fonts/
│   ├── Images/
│   ├── Materials/
│   ├── Models/
│   └── Textures/
│
├── TextMesh Pro/
│
├── ThirdParty/
│
└── _Project/
    ├── Animations/
    ├── Prefabs/
    ├── Scenes/
    └── Scripts/
```

#### `Art`

Contains the main art assets of the project: audio, fonts, images, materials, models, and textures.

#### `ThirdParty`

Contains third-party assets used for environment, weapons, UI, and game objects.

#### `_Project`

Contains the project's own assets:

* animations;
* prefabs;
* game scenes;
* C# scripts.

---

### 🧩 Main scripts

| Script             | Purpose                                                         |
| ------------------ | --------------------------------------------------------------- |
| `Player.cs`        | Player control, movement, weapons, and game interactions        |
| `Fire.cs`          | Shooting and reloading                                          |
| `Health.cs`        | Health, damage, and healing system                              |
| `Boss.cs`          | Boss phases, shield, and drones control                         |
| `MoveEnemy.cs`     | Enemy movement between predefined points                        |
| `Interactor.cs`    | Finding and handling available interactions                     |
| `IInteractable.cs` | Common interface for interactive objects                        |
| `Elevator.cs`      | Elevator control                                                |
| `EndTrigger.cs`    | Level completion                                                |
| `Medkit.cs`        | Medkit interaction                                              |
| `GameManager.cs`   | Global game state and current weapon parameters                 |
| `Settings.cs`      | Movement and control settings                                   |

---

### ▶️ Running the project from source

#### Requirements

* Unity Hub
* Unity **2022.3.62f3**

#### Launch

1. Clone the repository.
2. Open the project via Unity Hub.
3. Select Unity **2022.3.62f3**.
4. Open the game scene from `_Project/Scenes`.
5. Run the scene via Unity Editor.

The project's required dependencies are located in `Packages`.

---

### 📌 Project status

**Version:** `1.0.0`
**Status:** completed demonstration project.

The main goal of the project is to demonstrate skills in developing game mechanics, working with Unity, and organizing C# code.

---

### 👤 Author

**Shadowlucky**

[GitHub](https://github.com/Shadowlucky)
