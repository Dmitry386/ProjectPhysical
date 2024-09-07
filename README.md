# Описание
- Созданы сцены и передвижение персонажа.
- Настроен пользовательский интерфейс (UI) и анимации.
- Система инвентаря.
- Расставлены подбираемые предметы.
- Система сохранения инвентаря.

# Модульность
Некоторые системы являются отдельными Packages для возможности их повторного использования:
- DVContainer (контейнеры, инвентарь)
- DVParameters (параметры персонажа)
- DVRigidbodyCharacterController (контроллер персонажа на основе Rigidbody)
- DVSceneSystem (система загрузки уровней/сцен)
- DVUnityUtilities (утилиты)

# Сторонние ассеты
- GPVFX_POTIONS_PACK
- GrassFlowers
- Joystick Packages
- SledgeHammer
- Unity-AssetsCreator
- Books

# Принцип загрузки сцен
В игре используются 4 сцены: 
- Bootstrap (стартовая сцена, на ней находятся компоненты, которые не должны удаляться при смене сцен и загружаются 1 раз)
- Menu (пустая сцена, активируется при выходе в главное меню)
- Gameplay (геймплейные системы и HUD)
- Location (место спавна, локация)

При старте игры загружаются Gameplay (single) и Location (additive) сцены.

# Демонстрация
https://youtu.be/N3nJAnKSSjs

#
[![IMAGE ALT TEXT HERE](https://img.youtube.com/vi/N3nJAnKSSjs/0.jpg)](https://www.youtube.com/watch?v=N3nJAnKSSjs)