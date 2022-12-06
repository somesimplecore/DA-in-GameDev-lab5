# Создание индивидуальной системы достижения пользователя и ее интеграция в пользовательский интерфейс
Отчет по лабораторной работе #5 выполнил(а):
- Паханов Александр Александрович
- РИ-300018

Отметка о выполнении заданий (заполняется студентом):

| Задание | Выполнение | Баллы |
| ------ | ------ | ------ |
| Задание 1 | * | 60 |
| Задание 2 | * | 20 |
| Задание 3 | * | 20 |

знак "*" - задание выполнено; знак "#" - задание не выполнено;

Работу проверили:
- к.т.н., доцент Денисов Д.В.
- к.э.н., доцент Панов М.А.
- ст. преп., Фадеев В.О.

[![N|Solid](https://cldup.com/dTxpPi9lDf.thumb.png)](https://nodesource.com/products/nsolid)

[![Build Status](https://travis-ci.org/joemccann/dillinger.svg?branch=master)](https://travis-ci.org/joemccann/dillinger)

Структура отчета

- Данные о работе: название работы, фио, группа, выполненные задания.
- Цель работы.
- Задание 1.
- Код реализации выполнения задания. Визуализация результатов выполнения (если применимо).
- Задание 2.
- Код реализации выполнения задания. Визуализация результатов выполнения (если применимо).
- Задание 3.
- Код реализации выполнения задания. Визуализация результатов выполнения (если применимо).
- Выводы.
- ✨Magic ✨

## Цель работы
создание интерактивного приложения с рейтинговой системой пользователя и интеграция игровых сервисов в готовое приложение.

## Задание 1
### Используя видео-материалы практических работ 1-5 повторить реализацию функционала.
### Ход работы:

- Практическая работа «Интеграции авторизации с помощью Яндекс SDK»

Для проверки подключения YandexSDK и авторизации пользователя, при необходимости, напишем следующий код:

```C#
using UnityEngine;
using YG;

public class CheckConnectYG : MonoBehaviour
{
    private void OnEnable() => YandexGame.GetDataEvent += CheckSDK;
    private void OnDisable() => YandexGame.GetDataEvent -= CheckSDK;

    void Start()
    {
        if(YandexGame.SDKEnabled)
        {
            CheckSDK();
        }
    }
    
    public void CheckSDK()
    {
        if (YandexGame.auth)
        {
            Debug.Log("User auth ok");
        }
        else
        {
            YandexGame.AuthDialog();
            Debug.Log("User not auth");
        }
    }
}
```

Далее создадим пустой объект на сцене под названием YandexManager и прицепим к нему написанный выше скрипт. Теперь на платформе Яндекс Игры, если произойдет успешное подключение к YandexSDK, то будет проверка пользователя на авторизацию. Если она не была воспроизведена ранее, то всплывет окно авторизации. Также в консоль будет выводится состояние авторизации пользователя.

- Практическая работа «Сохранение данных пользователя на платформе Яндекс Игры»

Нам необходимо сохранять данные о количестве очков игрока. Для этого в уже существующий скрипт SavesYG добавим следующее публичное поле:

```C#
public int score;
```

Для сохранения прогресса на серверах яндекс и вывода в консоль прогресса, добавим следующие 2 метода в скрипт DragonPicker, т.к. в нем происходит логика проигрыша:

```C#
public void GetLoadSave()
{
    Debug.Log(YandexGame.savesData.score);
}

public void SaveData(int currentScore)
{
    YandexGame.savesData.score = currentScore;
    YandexGame.SaveProgress();
}
```

Будем вызывать эти методы после того, как наше здоровье упадет до 0:

```C#
if (shieldList.Count == 0)
{
    GameObject scoreGO = GameObject.Find("Score");
    scoreGT = scoreGO.GetComponent<TextMeshProUGUI>();
    SaveData(int.Parse(scoreGT.text));
    SceneManager.LoadScene("_0Scene");
    GetLoadSave();
}
```

- Практическая работа «Сбор данных об игроке и вывод их в интерфейсе»

Реализуем систему, в которой мы будем выводить лучший счет игрока в главном меню. Для этого добавим публичное поле в скрипт SavesYG:

```C#
public int bestScore;
```

Мы будем обновлять эту переменную в том случае, если текущий счет стал больше лучшего. Для этого в скрипте DragonPicker в методе SaveData допишем следующие строчки кода:

```C#
if(currentScore > YandexGame.savesData.bestScore)
{
    YandexGame.savesData.bestScore = currentScore;
}
```

Чтобы значение отображалось, в скрипте CheckConnectYG добавим следующее поле:

```C#
private TextMeshProUGUI bestScore;
```

А в метод CheckSDK добавим следующие строчки:

```C#
bestScore = GameObject.Find("BestScore").GetComponent<TextMeshProUGUI>();
bestScore.text = "Best score: " + YandexGame.savesData.bestScore.ToString();
```

На нашей сцене в канвасе создадим TMPro объект с названием "BestStore", где будет выводится наш лучший счет. Главное меню изменилось следующим образом:

![](/Pics/z1_1.jpg)

Теперь добавим имя игрока над персонажем на игровом поле. Для этого в канвасе создадим TMPro объект с названием "PlayerName", а в скрипт добавим следующее поле:

```C#
public TextMeshProUGUI playerName;
```

А в метод GetLoadSave следующие строчки:

```C#
playerName = GameObject.Find("PlayerName").GetComponent<TextMeshProUGUI>();
playerName.text = YandexGame.playerName;
```

Теперь наше игровое поле имеет следующий вид:

![](/Pics/z1_2.jpg)

- Практическая работа «Интеграция таблицы лидеров»

Для того, чтобы лучший результат пользователя заносился в таблицу лидеров, добавим следующую строчку в скрипте DragonPicker, когда у игрока кончаются жизни:

```C#
YandexGame.NewLeaderboardScores("TopPlayerScore", YandexGame.savesData.bestScore);
```

На платформе Яндекс Игры в разделе лидербордов добавим новый с техническим названием "TopPlayerScore". Таким образом, наш лидерборд работает.

- Практическая работа «Интеграция системы достижений в проект»

Сделаем в главном меню кнопку с достижениями и само поле для достижений:

![](/Pics/z1_3.jpg)

![](/Pics/z1_4.jpg)

Далее в скрипт SavesYG добавим следующую строчку:

```C#
public List<string> achievements = new List<string>()
```

Затем в скрипт CheckConnectYG добавим следующее поле, в котором будет храниться ссылка на TMPro наших достижений:

```C#
public TextMeshProUGUI achievements;
```

Затем в окне инспектора мы выберем наш объект, куда записываются достижения. Мы реализуем это таким образом, потому что метод GameObject.Find() не может находить объекты, если они отключены.

Теперь добивим строчки кода, чтобы поле достижений заполнялось теми, которые есть у игрока:

```C#
if(YandexGame.savesData.achievements.Count == 0)
{

}
else
{
    foreach(var value in YandexGame.savesData.achievements)
    {
        achievements.text = achievements.text + value + '\n';
    }
}
```

В скрипте DragonPicker, когда у нас будут кончаться щиты, будем делать проверку на то, есть ли у игрока достижение на первое поражение. Если нет, то мы его добавляем с помощью следующих строчек:

```C#
if(!YandexGame.savesData.achievements.Contains("Береги щиты!"))
{
    YandexGame.savesData.achievements.Add("Береги щиты!");  
}    
```

Теперь тестируем наш код и, при поражении, в окне с достижениями получаем следующий результат:

![](/Pics/z1_5.jpg)


## Задание 2
### Описать не менее трех дополнительных функций Яндекс SDK, которые могут быть интегрированы в игру.
### Ход Работы:

- YandexGame.playerPhoto
С помощью данного метода можно получить фото пользователя. Эту картинку потом можно применять как угодно. К примеру, можно ее привязать к игровому персонажу или по другому внедрить в игровой процесс.

- YandexGame.RewVideoShow()
Через вызов этого метода можно показывать рекламу за вознаграждение. За просмотр рекламы можно давать дополнительные ресурсы, уникальное оружие или интересные скины.

- YandexGame.PromptShow()
Этот метод вызывает диалоговое окно, в котором будет предложено установить ярлык. Данная функция может увеличить посещение игры пользователями, т.к. будет легче и быстрее открыть игру.

## Задание 3
### Добавить в меню Option возможность изменения громкости (от 0 до 100%) фоновой музыки в игре.
### Ход работы:

Создадим Audio Mixer, через который мы будем настраивать громкость музыки в главном меню и на игровом поле. В настройках Audio Source у камер поменяем Output на наш Audio Mixer:

![](/Pics/z3_1.jpg)

Затем создадим в меню Options слайдер и заголовок:

![](/Pics/z3_2.jpg)

Для регулировки звука напишем следующий скрипт:
```C#
using UnityEngine;
using UnityEngine.Audio;

public class VolumeSettings : MonoBehaviour
{
    public AudioMixer audioMixer;

    public void SetVolume(float volNum)
    {
        audioMixer.SetFloat("Music", volNum);
    }
}
```

Добаим этот скрипт к объекту MusicSettings и в поле добавим наш AudioMixer:

![](/Pics/z3_3.jpg)

Теперь, передвигая ползунок в настройках, мы регулируем звук. Это можно заметить, если на двух скриншотах ниже сравнить положения ползунка на слайдере и на звуковом интерфейсе AudioMixer снизу.

![](/Pics/z3_4.jpg)

![](/Pics/z3_5.jpg)

## Выводы

Мы добавили звуки в игру, создали главное меню, в котором можно настроить громкость музыки или начать игру. Также познакомились с удобной платформой mixamo.com, на которой есть большое количество 3D персонажей и анимации к ним. Оттуда мы и взяли модельку, чтобы украсить наше игровое поле. Также в Unity имеется возможность добавления пучка света, с помощью которого мы сделали нашего персонажа более эффектным. 

| Plugin | README |
| ------ | ------ |
| Dropbox | [plugins/dropbox/README.md][PlDb] |
| GitHub | [plugins/github/README.md][PlGh] |
| Google Drive | [plugins/googledrive/README.md][PlGd] |
| OneDrive | [plugins/onedrive/README.md][PlOd] |
| Medium | [plugins/medium/README.md][PlMe] |
| Google Analytics | [plugins/googleanalytics/README.md][PlGa] |

## Powered by

**BigDigital Team: Denisov | Fadeev | Panov**
