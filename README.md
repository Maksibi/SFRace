# SFRace - учебный проект в жанре аркадных гонок
Реализовано: рекордное время прохождения тестовой трассы. Сохранение рекорда в PlayerPrefs. автомобиль на WheelCollider, бёрнаут, дрифт, переключение передач. Настройка ходовых характеристик и поведения автомобиля через поля и кривые в редакторе, простая катсцена с пролетом камеры по точкам и возможностью пропуска (Cinemachine, Timeline). SFX (были использованы и отредактированы звуки со стоков), VFX (particles, postprocessing). Звуки автомобиля изменяются в зависимости от скорости/состояния авто. Визуальные эффекты также зависят от скорости ( Виньетка, FOV, motion blur, DOF). Была проделана работа с зависимостями в скриптах с помощью Zenject. Главное меню, настройки игры в главном меню.

Добавлено: Мультиплеер на Photon с созданием комнаты и присоединением по названию комнаты.

Планируется: несколько полноценных трасс, ///////мультиплеер на Photon////////, сохранения с помощью Json. Перенос характеристик автомобиля в ScriptableObject, несколько различных по хар-кам и внешнему виду автомобилей. Улучшение графики и звука.

# SFRace - a study project in the genre of arcade racing
Implemented: record time for completing the test track. Saving the record in PlayerPrefs. Car using WheelCollider, burnout, drift, gear shifting. Adjustment of vehicle dynamics and behavior through fields and curves in the editor, simple cutscene with camera flyby through points and the ability to skip (Cinemachine, Timeline). SFX (stock sounds used and edited), VFX (particles, post-processing). Car sounds vary depending on the speed/state of the vehicle. Visual effects also depend on speed (vignette, FOV, motion blur, DOF). Work with script dependencies using Zenject. Main menu, game settings in the main menu.

Added: Photon-based multiplayer with room creating by room name

Planned: multiple full-fledged tracks, ///////multiplayer using Photon///////, saving using JSON. Transferring car characteristics to ScriptableObject, several different cars with different characteristics and appearances. Improved graphics and sound.
