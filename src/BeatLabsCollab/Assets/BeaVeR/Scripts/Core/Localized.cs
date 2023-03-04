using BeaVeR.Models.BeatSaber;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace BeaVeR.Core
{
  /// <remarks>
  /// https://codefile.io/f/cTEYNSscAVnWbNYLxL4v
  /// </remarks>
  public static class Localized
  {
    private const string _UnknownString = "_UNKNOWN_";
    private const SystemLanguage _FallbackLanguage = SystemLanguage.English;

    private static readonly SystemLanguage _ActualLanguage = GetActualLanguage(Application.systemLanguage);
    //private static readonly SystemLanguage _ActualLanguage = GetActualLanguage(SystemLanguage.English);
    //private static readonly SystemLanguage _ActualLanguage = GetActualLanguage(SystemLanguage.Polish);
    //private static readonly SystemLanguage _ActualLanguage = GetActualLanguage(SystemLanguage.ChineseSimplified);
    //private static readonly SystemLanguage _ActualLanguage = GetActualLanguage(SystemLanguage.German);
    //private static readonly SystemLanguage _ActualLanguage = GetActualLanguage(SystemLanguage.Russian);
    //private static readonly SystemLanguage _ActualLanguage = GetActualLanguage(SystemLanguage.Korean);
    //private static readonly SystemLanguage _ActualLanguage = GetActualLanguage(SystemLanguage.Japanese);
    //private static readonly SystemLanguage _ActualLanguage = GetActualLanguage(SystemLanguage.Romanian);
    //private static readonly SystemLanguage _ActualLanguage = GetActualLanguage(SystemLanguage.Finnish);

    private static SystemLanguage GetActualLanguage(SystemLanguage language)
    {
      // fall back all Chinese variants to Chinese Simplified
      if (language == SystemLanguage.Chinese || language == SystemLanguage.ChineseTraditional)
      {
        language = SystemLanguage.ChineseSimplified;
      }

      return language;
    }

    public static string WelcomeTitle => GetLocalizedString(nameof(WelcomeTitle));
    public static string WelcomeText_Part_1 => GetLocalizedString(nameof(WelcomeText_Part_1));
    public static string WelcomeText_Part_2 => GetLocalizedString(nameof(WelcomeText_Part_2));
    public static string WelcomeText_DisclaimerHeader => GetLocalizedString(nameof(WelcomeText_DisclaimerHeader));
    public static string WelcomeText_Part_3 => GetLocalizedString(nameof(WelcomeText_Part_3));
    public static string LetsGoButtonText => GetLocalizedString(nameof(LetsGoButtonText));
    public static string BuildInfoTextTemplate => GetLocalizedString(nameof(BuildInfoTextTemplate));

    public static string SearchHeaderText => GetLocalizedString(nameof(SearchHeaderText));
    public static string SearchPlaceholderText => GetLocalizedString(nameof(SearchPlaceholderText));
    public static string SearchSongsHeaderText => GetLocalizedString(nameof(SearchSongsHeaderText));
    public static string SearchPlaylistsHeaderText => GetLocalizedString(nameof(SearchPlaylistsHeaderText));

    public static string SearchButtonText => GetLocalizedString(nameof(SearchButtonText));
    public static string ClearButtonText => GetLocalizedString(nameof(ClearButtonText));
    public static string SelectListItemButtonText => GetLocalizedString(nameof(SelectListItemButtonText));

    public static string EmptySongName => GetLocalizedString(nameof(EmptySongName));
    public static string EmptyPlaylistName => GetLocalizedString(nameof(EmptyPlaylistName));
    public static string EmptySongItemLine1 => GetLocalizedString(nameof(EmptySongItemLine1));
    public static string EmptyPlaylistItemLine1 => GetLocalizedString(nameof(EmptyPlaylistItemLine1));

    public static string BusyText_Searching => GetLocalizedString(nameof(BusyText_Searching));
    public static string BusyText_Downloading => GetLocalizedString(nameof(BusyText_Downloading));
    public static string BusyText_Loading => GetLocalizedString(nameof(BusyText_Loading));

    public static string FoundItemsTitle_FoundSongs => GetLocalizedString(nameof(FoundItemsTitle_FoundSongs));
    public static string FoundItemsTitle_FoundPlaylists => GetLocalizedString(nameof(FoundItemsTitle_FoundPlaylists));

    public static string TabButtonText_SearchSongs => GetLocalizedString(nameof(TabButtonText_SearchSongs));
    public static string TabButtonText_SearchPlaylists => GetLocalizedString(nameof(TabButtonText_SearchPlaylists));

    public static string DifficultyHeaderText => GetLocalizedString(nameof(DifficultyHeaderText));
    public static string GameModeHeaderText => GetLocalizedString(nameof(GameModeHeaderText));
    public static string PlayButtonText => GetLocalizedString(nameof(PlayButtonText));

    public static string Difficulty_ExpertPlus => GetLocalizedString(nameof(Difficulty_ExpertPlus));
    public static string Difficulty_Expert => GetLocalizedString(nameof(Difficulty_Expert));
    public static string Difficulty_Hard => GetLocalizedString(nameof(Difficulty_Hard));
    public static string Difficulty_Normal => GetLocalizedString(nameof(Difficulty_Normal));
    public static string Difficulty_Easy => GetLocalizedString(nameof(Difficulty_Easy));

    public static string GameModeDisplayName_Classic => GetLocalizedString(nameof(GameModeDisplayName_Classic));
    public static string GameModeDisplayName_ChillPill => GetLocalizedString(nameof(GameModeDisplayName_ChillPill));
    public static string GameModeDisplayName_TimingAttack => GetLocalizedString(nameof(GameModeDisplayName_TimingAttack));

    public static string GamePausedHeaderText => GetLocalizedString(nameof(GamePausedHeaderText));
    public static string LevelClearedHeaderText => GetLocalizedString(nameof(LevelClearedHeaderText));
    public static string MenuButtonText => GetLocalizedString(nameof(MenuButtonText));
    public static string ResumeButtonText => GetLocalizedString(nameof(ResumeButtonText));

    public static string PlaylistStats_MapsCount => GetLocalizedString(nameof(PlaylistStats_MapsCount));
    public static string PlaylistStats_AverageScore => GetLocalizedString(nameof(PlaylistStats_AverageScore));
    public static string ViewPlaylistSongsButtonText => GetLocalizedString(nameof(ViewPlaylistSongsButtonText));
    public static string PlaylistLabel => GetLocalizedString(nameof(PlaylistLabel));

    public static string GameScoreLabel_Accuracy => GetLocalizedString(nameof(GameScoreLabel_Accuracy));
    public static string GameScoreLabel_BlocksHitCount => GetLocalizedString(nameof(GameScoreLabel_BlocksHitCount));
    public static string GameScoreLabel_BlocksMissedCount => GetLocalizedString(nameof(GameScoreLabel_BlocksMissedCount));

    public static string GetDifficultyDisplayName(string difficultyName)
    {
      Difficulty difficulty;

      if (Enum.TryParse<Difficulty>(difficultyName, out difficulty))
      {
        switch (difficulty)
        {
          case Difficulty.ExpertPlus: return Difficulty_ExpertPlus;
          case Difficulty.Expert: return Difficulty_Expert;
          case Difficulty.Hard: return Difficulty_Hard;
          case Difficulty.Normal: return Difficulty_Normal;
          case Difficulty.Easy: return Difficulty_Easy;
          default: throw new ArgumentOutOfRangeException($"Unknown difficulty: '{difficulty}'.");
        }
      }

      return difficultyName;
    }

    private static string GetLocalizedString(string name)
    {
      if (string.IsNullOrEmpty(name))
      {
        throw new ArgumentNullException(nameof(name));
      }

      if (_LocalizedStringsByName.TryGetValue(name, out var localizedStringsByLanguage))
      {
        string localizedString;

        if (localizedStringsByLanguage.TryGetValue(_ActualLanguage, out localizedString))
        {
          return localizedString;
        }
        else if (localizedStringsByLanguage.TryGetValue(_FallbackLanguage, out localizedString))
        {
          return localizedString;
        }
      }

      return _UnknownString;
    }

    private static readonly Dictionary<string, Dictionary<SystemLanguage, string>> _LocalizedStringsByName =
      new Dictionary<string, Dictionary<SystemLanguage, string>>
      {
        { nameof(WelcomeTitle), new Dictionary<SystemLanguage, string>
          {
            { SystemLanguage.English, "Welcome to Beat Labs!" },
            { SystemLanguage.Polish, "Witaj w Beat Labs!" },
            { SystemLanguage.ChineseSimplified, "欢迎来到的节拍实验室 Beat Labs！" },
            { SystemLanguage.German, "Willkommen bei Beat Labs!" },
            { SystemLanguage.Russian, "Добро пожаловать в Beat Labs!" },
            { SystemLanguage.Korean, "번역하다" },
            { SystemLanguage.Japanese, "訳す" },
            { SystemLanguage.Spanish, "traducir" },
            { SystemLanguage.Romanian, "Bun venit la Beat Labs!" },
            { SystemLanguage.Finnish, "Tervetuloa Beat Labsiin!" },
            { SystemLanguage.Unknown, "mugh" },
          }
        },
        { nameof(WelcomeText_Part_1), new Dictionary<SystemLanguage, string>
          {
            { SystemLanguage.English, @"
Beat Labs is a free and open sandbox for fiddling with different gameplay mechanics and ideas for rhythm games in VR.

It currently focuses on the mechanics of Beat Saber but eventually there will be various modifications to the way the game feels and plays.

Find out more on the project website:
              ".Trim()
            },
            { SystemLanguage.Polish, @"
Beat Labs to darmowe i otwarte środowisko do eksperymentów z różnymi mechanikami i pomysłami na gry rytmiczne w VR.

Obecnie skupia się na mechanice gry Beat Saber, ale w przyszłości pojawią się modyfikacje, które zmienią zarówno wygląd, jak i sposób rozgrywki.

Dowiedz się więcej na stronie projektu:
              ".Trim()
            },
            { SystemLanguage.ChineseSimplified, @"
节拍实验室 Beat Labs 是一个免费的开放式沙盒，用于尝试虚拟现实 VR 中不同的游戏机制和韵律游戏的想法。

它目前专注于节奏光剑的机制，但接下来会对游戏感觉和玩法进行各种修改。

在项目网站上了解更多信息：
              ".Trim()
            },
            { SystemLanguage.German, @"
Beat Labs ist eine Sandbox zum Experimentieren mit verschiedenen Mechaniken und Ideen für Rhythmusspiele in VR.

Momentan fokussiert es sich auf die Spielmechanik von Beat Saber, aber langfristig wird es verschiedene Änderungen an der Art und Weise geben, wie sich das Spiel anfühlt und spielt.

Erfahre mehr auf der Webseite des Projekts:
              ".Trim()
            },
            { SystemLanguage.Russian, @"
Beat Labs - это бесплатная песочница, в которой можно поработать над различными игровыми механиками и идеями для ритм-игр в VR.

В настоящее время проект сосредоточен на механике Beat Saber, но со временем появятся различные изменения в механиках и ощущениях от игрового процесса.

Узнайте больше на сайте проекта:
              ".Trim()
            },
            { SystemLanguage.Korean, @"
번역하다

번역하다

번역하다
              ".Trim()
            },
            { SystemLanguage.Japanese, @"
訳す

訳す

訳す
              ".Trim()
            },
            { SystemLanguage.Spanish, @"
traducir

traducir

traducir
              ".Trim()
            },
            { SystemLanguage.Romanian, @"
Beat Labs este un sandbox gratis și deschis pentru a încerca diferite mecanici din joc și idei pentru jocuri de ritm în RV.

Deocamdată este concentrat pe mecanicile a jocului ”Beat Saber”, dar eventual vor fii modificări făcute la mecanicile din joc.

Aflați mai multe pe website-ul proiectului:
              ".Trim()
            },
            { SystemLanguage.Finnish, @"
Beat Labs on ilmainen ja avoin hiekkalaatikko erilaisten pelimekaniikkojen ja ideoiden kokeilemiseen VR-rytmipeleissä.

Tällä hetkellä se keskittyy Beat Saberin mekaniikkaan, mutta lopulta pelin tunnetta ja pelattavuutta muutetaan monin eri tavoin.

Lisätietoja projektin verkkosivustolta:
              ".Trim()
            },
            { SystemLanguage.Unknown, @"
mugh

mugh

mugh
              ".Trim()
            },
          }
        },
        { nameof(WelcomeText_Part_2), new Dictionary<SystemLanguage, string>
          {
            { SystemLanguage.English, "See you on Discord:" },
            { SystemLanguage.Polish, "Do zobaczenia na Discordzie:" },
            { SystemLanguage.ChineseSimplified, "希望可以在 Discord 上见到你：" },
            { SystemLanguage.German, "Wir sehen uns auf Discord:" },
            { SystemLanguage.Russian, "Приглашаем на наш Discord сервер" },
            { SystemLanguage.Korean, "번역하다" },
            { SystemLanguage.Japanese, "訳す" },
            { SystemLanguage.Spanish, "traducir" },
            { SystemLanguage.Romanian, "Ne vedem pe Discord:" },
            { SystemLanguage.Finnish, "Nähdään Discordissa:" },
            { SystemLanguage.Unknown, "mugh" },
          }
        },
        { nameof(WelcomeText_DisclaimerHeader), new Dictionary<SystemLanguage, string>
          {
            { SystemLanguage.English, "Disclaimer" },
            { SystemLanguage.Polish, "Oświadczenie" },
            { SystemLanguage.ChineseSimplified, "免责声明" },
            { SystemLanguage.German, "Haftungsausschluss" },
            { SystemLanguage.Russian, "Предисловие" },
            { SystemLanguage.Korean, "번역하다" },
            { SystemLanguage.Japanese, "訳す" },
            { SystemLanguage.Spanish, "traducir" },
            { SystemLanguage.Romanian, "Atenție" }, // no good translation for this. Attention would be the closest.
            { SystemLanguage.Finnish, "Huomio" }, // this meant Attention, Vastuuvapautuslauseke would be straight translation but it is way too formal and not really fitting
            { SystemLanguage.Unknown, "mugh" },
          }
        },
        { nameof(WelcomeText_Part_3), new Dictionary<SystemLanguage, string>
          {
            { SystemLanguage.English, @"
Beat Labs is not meant to replace, compete with or even replicate Beat Saber one-to-one. Rather, it's a learning and experimentation tool for the community

The songs/maps come from an external source which is not affiliated with Beat Labs. Kudos to them and all the beatmappers out there!
              ".Trim()
            },
            { SystemLanguage.Polish, @"
Beat Labs nie ma na celu zastąpienia, rywalizowania czy replikowania Beat Sabera jeden do jednego. Jest to raczej narzędzie do nauki i eksperymentów dla społeczności.

Utwory/mapy pochodzą z zewnętrznego źródła, które nie jest powiązane z Beat Labs. Podziękowania dla nich i dla wszystkich beatmapperów!
              ".Trim()
            },
            { SystemLanguage.ChineseSimplified, @"
节拍实验室 Beat Labs 不是为了取代、竞争甚至是完全复制节奏光剑。相反，它是一种社区学习和实验的工具。

歌曲/地图来自外部来源它与 节拍实验室 Beat Labs 无关。感谢他们和所有的节拍制图员!
              ".Trim()
            },
            { SystemLanguage.German, @"
Beat Labs versucht nicht, Beat Saber eins-zu-eins zu kopieren, damit zu konkurrieren oder es gar zu ersetzen. Eher ist es ein Lern- und Experimentiertool für die Community.

Die Songs/Maps stammen aus einer externen Quelle, mit der Beat Labs in keinerlei Beziehung steht. Danke an Beatsaver und alle Beatmapper da draußen!
              ".Trim()
            },
            { SystemLanguage.Russian, @"
Beat Labs не призван заменить, соперничать или же копировать Beat Saber. Это скорее инструмент обучения и экспериментов для сообщества.

Песни/карты взяты из внешних источников, не связанных с Beat Labs. Благодарим их и всех битмапперов!
              ".Trim()
            },
            { SystemLanguage.Korean, @"
번역하다

번역하다
              ".Trim()
            },
            { SystemLanguage.Japanese, @"
訳す

訳す
              ".Trim()
            },
            { SystemLanguage.Spanish, @"
traducir

traducir
              ".Trim()
            },
            { SystemLanguage.Romanian, @"
Beat Labs nu intenționeaza să înlocuiască sau să concureze cu sau să replice jocul ”Beat Saber”. Defapt, este un instrument experimental de învățare pentru comunitate.

Hărțiile/cântecele vin dintr-o sursă externă care nu este afiliat cu Beat Labs. Le mulțumim lor și toți creatorii de hărți.
              ".Trim()
            },
            { SystemLanguage.Finnish, @"
Beat Labsia ei ole tarkoitettu korvaamaan tai kopioimaan Beat Saberia, eikä kilpailemaan sen kanssa. Sen sijaan se on oppimis- ja kokeilutyökalu yhteisölle.

Kappaleet tulevat ulkoisesta lähteestä, joka ei ole yhteydessä Beat Labsiin. Kiitos heille ja kaikille beatmappereille!
              ".Trim()
            },
            { SystemLanguage.Unknown, @"
mugh

mugh
              ".Trim()
            },
          }
        },
        { nameof(LetsGoButtonText), new Dictionary<SystemLanguage, string>
          {
            { SystemLanguage.English, "Let's Go!" },
            { SystemLanguage.Polish, "Zaczynajmy!" },
            { SystemLanguage.ChineseSimplified, "开始吧！" },
            { SystemLanguage.German, "Los geht's!" },
            { SystemLanguage.Russian, "Начнем!" },
            { SystemLanguage.Korean, "번역하다" },
            { SystemLanguage.Japanese, "訳す" },
            { SystemLanguage.Spanish, "traducir" },
            { SystemLanguage.Romanian, "Hai să mergem!" },
            { SystemLanguage.Finnish, "Eikun Menoksi!" },
            { SystemLanguage.Unknown, "mugh" },
          }
        },
        { nameof(BuildInfoTextTemplate), new Dictionary<SystemLanguage, string>
          {
            { SystemLanguage.English, "${Platform} | v${AppVersion}" },
            { SystemLanguage.Polish, "${Platform} | v${AppVersion}" },
            { SystemLanguage.ChineseSimplified, "${Platform} | v${AppVersion}" },
            { SystemLanguage.German, "${Platform} | v${AppVersion}" },
            { SystemLanguage.Russian, "${Platform} | v${AppVersion}" },
            { SystemLanguage.Korean, "${Platform} | v${AppVersion}" },
            { SystemLanguage.Japanese, "${Platform} | v${AppVersion}" },
            { SystemLanguage.Spanish, "${Platform} | v${AppVersion}" },
            { SystemLanguage.Romanian, "${Platform} | v${AppVersion}" },
            { SystemLanguage.Finnish, "${Platform} | v${AppVersion}" },
            { SystemLanguage.Unknown, "${Platform} | v${AppVersion}" },
          }
        },
        { nameof(SearchHeaderText), new Dictionary<SystemLanguage, string>
          {
            { SystemLanguage.English, "Search" },
            { SystemLanguage.Polish, "Wyszukaj" },
            { SystemLanguage.ChineseSimplified, "搜索" },
            { SystemLanguage.German, "Suche" },
            { SystemLanguage.Russian, "Поиск" },
            { SystemLanguage.Korean, "번역하다" },
            { SystemLanguage.Japanese, "訳す" },
            { SystemLanguage.Spanish, "traducir" },
            { SystemLanguage.Romanian, "Căutare" },
            { SystemLanguage.Finnish, "Haku" },
            { SystemLanguage.Unknown, "mugh" },
          }
        },
        { nameof(SearchPlaceholderText), new Dictionary<SystemLanguage, string>
          {
            { SystemLanguage.English, "Enter text ..." },
            { SystemLanguage.Polish, "Wprowadź tekst ..." },
            { SystemLanguage.ChineseSimplified, "输入文字 ..." },
            { SystemLanguage.German, "Text eingeben ..." },
            { SystemLanguage.Russian, "Введите текст ..." },
            { SystemLanguage.Korean, "번역하다" },
            { SystemLanguage.Japanese, "訳す" },
            { SystemLanguage.Spanish, "traducir" },
            { SystemLanguage.Romanian, "Introduceți textul ..." },
            { SystemLanguage.Finnish, "Syötä Teksti ..." },
            { SystemLanguage.Unknown, "mugh" },
          }
        },
        { nameof(SearchSongsHeaderText), new Dictionary<SystemLanguage, string>
          {
            { SystemLanguage.English, "Search Songs" },
            { SystemLanguage.Polish, "Wyszukaj utwór" },
            { SystemLanguage.ChineseSimplified, "搜索歌曲" },
            { SystemLanguage.German, "Lieder suchen" },
            { SystemLanguage.Russian, "Поиск Песен" },
            { SystemLanguage.Korean, "번역하다" },
            { SystemLanguage.Japanese, "訳す" },
            { SystemLanguage.Spanish, "traducir" },
            { SystemLanguage.Romanian, "Căutați melodii" },
            { SystemLanguage.Finnish, "Hae Kappaleita" },
            { SystemLanguage.Unknown, "mugh" },
          }
        },
        { nameof(SearchPlaylistsHeaderText), new Dictionary<SystemLanguage, string>
          {
            { SystemLanguage.English, "Search Playlists" },
            { SystemLanguage.Polish, "Wyszukaj playlistę" },
            { SystemLanguage.ChineseSimplified, "搜索歌单" },
            { SystemLanguage.German, "Playlists suchen" },
            { SystemLanguage.Russian, "Поиск Плейлистов" },
            { SystemLanguage.Korean, "번역하다" },
            { SystemLanguage.Japanese, "訳す" },
            { SystemLanguage.Spanish, "traducir" },
            { SystemLanguage.Romanian, "Căutați playlisturi" },
            { SystemLanguage.Finnish, "Hae Soittolistoja" },
            { SystemLanguage.Unknown, "mugh" },
          }
        },
        { nameof(SearchButtonText), new Dictionary<SystemLanguage, string>
          {
            { SystemLanguage.English, "Search" },
            { SystemLanguage.Polish, "Szukaj" },
            { SystemLanguage.ChineseSimplified, "搜索" },
            { SystemLanguage.German, "Suchen" },
            { SystemLanguage.Russian, "Поиск" },
            { SystemLanguage.Korean, "번역하다" },
            { SystemLanguage.Japanese, "訳す" },
            { SystemLanguage.Spanish, "traducir" },
            { SystemLanguage.Romanian, "Caută" },
            { SystemLanguage.Finnish, "Hae" },
            { SystemLanguage.Unknown, "mugh" },
          }
        },
        { nameof(ClearButtonText), new Dictionary<SystemLanguage, string>
          {
            { SystemLanguage.English, "Clear" },
            { SystemLanguage.Polish, "Wyczyść" },
            { SystemLanguage.ChineseSimplified, "清除" },
            { SystemLanguage.German, "Löschen" },
            { SystemLanguage.Russian, "Очистить" },
            { SystemLanguage.Korean, "번역하다" },
            { SystemLanguage.Japanese, "訳す" },
            { SystemLanguage.Spanish, "traducir" },
            { SystemLanguage.Romanian, "Ștergeți" },
            { SystemLanguage.Finnish, "Tyhjennä" },
            { SystemLanguage.Unknown, "mugh" },
          }
        },
        { nameof(SelectListItemButtonText), new Dictionary<SystemLanguage, string>
          {
            { SystemLanguage.English, "Select" },
            { SystemLanguage.Polish, "Wybierz" },
            { SystemLanguage.ChineseSimplified, "选择" },
            { SystemLanguage.German, "Auswählen" },
            { SystemLanguage.Russian, "Выбрать" },
            { SystemLanguage.Korean, "번역하다" },
            { SystemLanguage.Japanese, "訳す" },
            { SystemLanguage.Spanish, "traducir" },
            { SystemLanguage.Romanian, "Selectați" },
            { SystemLanguage.Finnish, "Valitse" },
            { SystemLanguage.Unknown, "mugh" },
          }
        },
        { nameof(EmptySongName), new Dictionary<SystemLanguage, string>
          {
            { SystemLanguage.English, "No Song Selected" },
            { SystemLanguage.Polish, "Nic nie wybrano" },
            { SystemLanguage.ChineseSimplified, "未选择歌曲" },
            { SystemLanguage.German, "Kein Lied ausgewählt" },
            { SystemLanguage.Russian, "Песеня не выбрана" },
            { SystemLanguage.Korean, "번역하다" },
            { SystemLanguage.Japanese, "訳す" },
            { SystemLanguage.Spanish, "traducir" },
            { SystemLanguage.Romanian, "Nici-o melodie selectată" },
            { SystemLanguage.Finnish, "Ei Kappaletta Valittuna" },
            { SystemLanguage.Unknown, "mugh" },
          }
        },
        { nameof(EmptyPlaylistName), new Dictionary<SystemLanguage, string>
          {
            { SystemLanguage.English, "No Playlist Selected" },
            { SystemLanguage.Polish, "Nic nie wybrano" },
            { SystemLanguage.ChineseSimplified, "未选择歌单" },
            { SystemLanguage.German, "Keine Playlist ausgewählt" },
            { SystemLanguage.Russian, "Плейлист не выбран" },
            { SystemLanguage.Korean, "번역하다" },
            { SystemLanguage.Japanese, "訳す" },
            { SystemLanguage.Spanish, "traducir" },
            { SystemLanguage.Romanian, "Nici-un playlist selectat" },
            { SystemLanguage.Finnish, "Ei Soittolistaa Valittuna" },
            { SystemLanguage.Unknown, "mugh" },
          }
        },
        { nameof(EmptySongItemLine1), new Dictionary<SystemLanguage, string>
          {
            { SystemLanguage.English, "No Song" },
            { SystemLanguage.Polish, "Brak utworu" },
            { SystemLanguage.ChineseSimplified, "没有歌" },
            { SystemLanguage.German, "Kein Lied" },
            { SystemLanguage.Russian, "Нет песни" },
            { SystemLanguage.Korean, "번역하다" },
            { SystemLanguage.Japanese, "訳す" },
            { SystemLanguage.Spanish, "traducir" },
            { SystemLanguage.Romanian, "Nici-o melodie" },
            { SystemLanguage.Finnish, "Ei Kappaletta" },
            { SystemLanguage.Unknown, "mugh" },
          }
        },
        { nameof(EmptyPlaylistItemLine1), new Dictionary<SystemLanguage, string>
          {
            { SystemLanguage.English, "No Playlist" },
            { SystemLanguage.Polish, "Brak playlisty" },
            { SystemLanguage.ChineseSimplified, "没有歌单" },
            { SystemLanguage.German, "Keine Playlist" },
            { SystemLanguage.Russian, "Нет плейлиста" },
            { SystemLanguage.Korean, "번역하다" },
            { SystemLanguage.Japanese, "訳す" },
            { SystemLanguage.Spanish, "traducir" },
            { SystemLanguage.Romanian, "Nici-un playlist" },
            { SystemLanguage.Finnish, "Ei Soittolistaa" },
            { SystemLanguage.Unknown, "mugh" },
          }
        },
        { nameof(BusyText_Searching), new Dictionary<SystemLanguage, string>
          {
            { SystemLanguage.English, "Searching ..." },
            { SystemLanguage.Polish, "Szukam ..." },
            { SystemLanguage.ChineseSimplified, "搜索中 ..." },
            { SystemLanguage.German, "Suchen ..." },
            { SystemLanguage.Russian, "Идет поиск ..." },
            { SystemLanguage.Korean, "번역하다" },
            { SystemLanguage.Japanese, "訳す" },
            { SystemLanguage.Spanish, "traducir" },
            { SystemLanguage.Romanian, "Se caută ..." },
            { SystemLanguage.Finnish, "Hakee ..." },
            { SystemLanguage.Unknown, "mugh" },
          }
        },
        { nameof(BusyText_Downloading), new Dictionary<SystemLanguage, string>
          {
            { SystemLanguage.English, "Downloading ..." },
            { SystemLanguage.Polish, "Pobieram ..." },
            { SystemLanguage.ChineseSimplified, "下载中 ..." },
            { SystemLanguage.German, "Herunterladen ..." },
            { SystemLanguage.Russian, "Скачивание ..." },
            { SystemLanguage.Korean, "번역하다" },
            { SystemLanguage.Japanese, "訳す" },
            { SystemLanguage.Spanish, "traducir" },
            { SystemLanguage.Romanian, "Se descarcă ..." },
            { SystemLanguage.Finnish, "Lataa ..." },
            { SystemLanguage.Unknown, "mugh" },
          }
        },
        { nameof(BusyText_Loading), new Dictionary<SystemLanguage, string>
          {
            { SystemLanguage.English, "Loading ..." },
            { SystemLanguage.Polish, "Wczytuję ..." },
            { SystemLanguage.ChineseSimplified, "加载中 ..." },
            { SystemLanguage.German, "Laden ..." },
            { SystemLanguage.Russian, "Загрузка ..." },
            { SystemLanguage.Korean, "번역하다" },
            { SystemLanguage.Japanese, "訳す" },
            { SystemLanguage.Spanish, "traducir" },
            { SystemLanguage.Romanian, "Se încarcă ..." },
            { SystemLanguage.Finnish, "Lataa ..." },
            { SystemLanguage.Unknown, "mugh" },
          }
        },
        { nameof(FoundItemsTitle_FoundSongs), new Dictionary<SystemLanguage, string>
          {
            { SystemLanguage.English, "Found Songs" },
            { SystemLanguage.Polish, "Znalezione utwory" },
            { SystemLanguage.ChineseSimplified, "找到的歌曲" },
            { SystemLanguage.German, "Gefundene Lieder" },
            { SystemLanguage.Russian, "Результаты поиска песен" },
            { SystemLanguage.Korean, "번역하다" },
            { SystemLanguage.Japanese, "訳す" },
            { SystemLanguage.Spanish, "traducir" },
            { SystemLanguage.Romanian, "Melodii găsite" },
            { SystemLanguage.Finnish, "Löytyneet Kappaleet" },
            { SystemLanguage.Unknown, "mugh" },
          }
        },
        { nameof(FoundItemsTitle_FoundPlaylists), new Dictionary<SystemLanguage, string>
          {
            { SystemLanguage.English, "Found Playlists" },
            { SystemLanguage.Polish, "Znalezione playlisty" },
            { SystemLanguage.ChineseSimplified, "找到的歌单" },
            { SystemLanguage.German, "Gefundene Playlists" },
            { SystemLanguage.Russian, "Результаты поиска плейлистов" },
            { SystemLanguage.Korean, "번역하다" },
            { SystemLanguage.Japanese, "訳す" },
            { SystemLanguage.Spanish, "traducir" },
            { SystemLanguage.Romanian, "Playlisturi găsite" },
            { SystemLanguage.Finnish, "Löytyneet Soittolistat" },
            { SystemLanguage.Unknown, "mugh" },
          }
        },
        { nameof(TabButtonText_SearchSongs), new Dictionary<SystemLanguage, string>
          {
            { SystemLanguage.English, "SONGS" },
            { SystemLanguage.Polish, "UTWO\nRY" },
            { SystemLanguage.ChineseSimplified, "歌曲" },
            { SystemLanguage.German, "LIEDER" },
            { SystemLanguage.Russian, "ПЕСНИ" },
            { SystemLanguage.Korean, "번역하다" },
            { SystemLanguage.Japanese, "訳す" },
            { SystemLanguage.Spanish, "traducir" },
            { SystemLanguage.Romanian, "MELODII" },
            { SystemLanguage.Finnish, "KAPPA\nLEET" },
            { SystemLanguage.Unknown, "mugh" },
          }
        },
        { nameof(TabButtonText_SearchPlaylists), new Dictionary<SystemLanguage, string>
          {
            { SystemLanguage.English, "LISTS" },
            { SystemLanguage.Polish, "PLAY\nLISTY" },
            { SystemLanguage.ChineseSimplified, "歌单" },
            { SystemLanguage.German, "LISTEN" },
            { SystemLanguage.Russian, "ПЛЕЙ\nЛИСТЫ" },
            { SystemLanguage.Korean, "번역하다" },
            { SystemLanguage.Japanese, "訳す" },
            { SystemLanguage.Spanish, "traducir" },
            { SystemLanguage.Romanian, "PLAYLIST\nURI" },
            { SystemLanguage.Finnish, "SOITTO\nLISTAT" },
            { SystemLanguage.Unknown, "mugh" },
          }
        },
        { nameof(DifficultyHeaderText), new Dictionary<SystemLanguage, string>
          {
            { SystemLanguage.English, "Difficulty" },
            { SystemLanguage.Polish, "Poziom trudności" },
            { SystemLanguage.ChineseSimplified, "难度" },
            { SystemLanguage.German, "Schwierigkeitsgrad" },
            { SystemLanguage.Russian, "Сложность" },
            { SystemLanguage.Korean, "번역하다" },
            { SystemLanguage.Japanese, "訳す" },
            { SystemLanguage.Spanish, "traducir" },
            { SystemLanguage.Romanian, "Dificultate" },
            { SystemLanguage.Finnish, "Vaikeustaso" },
            { SystemLanguage.Unknown, "mugh" },
          }
        },
        { nameof(GameModeHeaderText), new Dictionary<SystemLanguage, string>
          {
            { SystemLanguage.English, "Game Mode" },
            { SystemLanguage.Polish, "Tryb gry" },
            { SystemLanguage.ChineseSimplified, "游戏模式" },
            { SystemLanguage.German, "Spielmodus" },
            { SystemLanguage.Russian, "Режим игры" },
            { SystemLanguage.Korean, "번역하다" },
            { SystemLanguage.Japanese, "訳す" },
            { SystemLanguage.Spanish, "traducir" },
            { SystemLanguage.Romanian, "Modul de joc" },
            { SystemLanguage.Finnish, "Peli Moodi" },
            { SystemLanguage.Unknown, "mugh" },
          }
        },
        { nameof(PlayButtonText), new Dictionary<SystemLanguage, string>
          {
            { SystemLanguage.English, "Play" },
            { SystemLanguage.Polish, "Zagraj" },
            { SystemLanguage.ChineseSimplified, "玩" },
            { SystemLanguage.German, "Spielen" },
            { SystemLanguage.Russian, "Играть" },
            { SystemLanguage.Korean, "번역하다" },
            { SystemLanguage.Japanese, "訳す" },
            { SystemLanguage.Spanish, "traducir" },
            { SystemLanguage.Romanian, "Jucați" },
            { SystemLanguage.Finnish, "Pelaa" },
            { SystemLanguage.Unknown, "mugh" },
          }
        },
        { nameof(Difficulty_ExpertPlus), new Dictionary<SystemLanguage, string>
          {
            { SystemLanguage.English, "Expert+" },
            { SystemLanguage.Polish, "Ekspert+" },
            { SystemLanguage.ChineseSimplified, "专家+" },
            { SystemLanguage.German, "Experte+" },
            { SystemLanguage.Russian, "Эксперт+" },
            { SystemLanguage.Korean, "번역하다" },
            { SystemLanguage.Japanese, "訳す" },
            { SystemLanguage.Spanish, "traducir" },
            { SystemLanguage.Romanian, "Expert+" },
            { SystemLanguage.Finnish, "Expertti+" },
            { SystemLanguage.Unknown, "mugh" },
          }
        },
        { nameof(Difficulty_Expert), new Dictionary<SystemLanguage, string>
          {
            { SystemLanguage.English, "Expert" },
            { SystemLanguage.Polish, "Ekspert" },
            { SystemLanguage.ChineseSimplified, "专家" },
            { SystemLanguage.German, "Experte" },
            { SystemLanguage.Russian, "Эксперт" },
            { SystemLanguage.Korean, "번역하다" },
            { SystemLanguage.Japanese, "訳す" },
            { SystemLanguage.Spanish, "traducir" },
            { SystemLanguage.Romanian, "Expert" },
            { SystemLanguage.Finnish, "Expertti" },
            { SystemLanguage.Unknown, "mugh" },
          }
        },
        { nameof(Difficulty_Hard), new Dictionary<SystemLanguage, string>
          {
            { SystemLanguage.English, "Hard" },
            { SystemLanguage.Polish, "Trudny" },
            { SystemLanguage.ChineseSimplified, "困难" },
            { SystemLanguage.German, "Schwer" },
            { SystemLanguage.Russian, "Сложно" },
            { SystemLanguage.Korean, "번역하다" },
            { SystemLanguage.Japanese, "訳す" },
            { SystemLanguage.Spanish, "traducir" },
            { SystemLanguage.Romanian, "Greu" },
            { SystemLanguage.Finnish, "Vaikea" },
            { SystemLanguage.Unknown, "mugh" },
          }
        },
        { nameof(Difficulty_Normal), new Dictionary<SystemLanguage, string>
          {
            { SystemLanguage.English, "Normal" },
            { SystemLanguage.Polish, "Normalny" },
            { SystemLanguage.ChineseSimplified, "普通" },
            { SystemLanguage.German, "Normal" },
            { SystemLanguage.Russian, "Нормально" },
            { SystemLanguage.Korean, "번역하다" },
            { SystemLanguage.Japanese, "訳す" },
            { SystemLanguage.Spanish, "traducir" },
            { SystemLanguage.Romanian, "Normal" },
            { SystemLanguage.Finnish, "Normaali" },
            { SystemLanguage.Unknown, "mugh" },
          }
        },
        { nameof(Difficulty_Easy), new Dictionary<SystemLanguage, string>
          {
            { SystemLanguage.English, "Easy" },
            { SystemLanguage.Polish, "Łatwy" },
            { SystemLanguage.ChineseSimplified, "简单" },
            { SystemLanguage.German, "Einfach" },
            { SystemLanguage.Russian, "Легко" },
            { SystemLanguage.Korean, "번역하다" },
            { SystemLanguage.Japanese, "訳す" },
            { SystemLanguage.Spanish, "traducir" },
            { SystemLanguage.Romanian, "Ușor" },
            { SystemLanguage.Finnish, "Helppo" },
            { SystemLanguage.Unknown, "mugh" },
          }
        },
        { nameof(GameModeDisplayName_Classic), new Dictionary<SystemLanguage, string>
          {
            { SystemLanguage.English, "Classic" },
            { SystemLanguage.Polish, "Klasyczny" },
            { SystemLanguage.ChineseSimplified, "经典" },
            { SystemLanguage.German, "Klassisch" },
            { SystemLanguage.Russian, "Классический" },
            { SystemLanguage.Korean, "번역하다" },
            { SystemLanguage.Japanese, "訳す" },
            { SystemLanguage.Spanish, "traducir" },
            { SystemLanguage.Romanian, "Clasic" },
            { SystemLanguage.Finnish, "Klassinen" },
            { SystemLanguage.Unknown, "mugh" },
          }
        },
        { nameof(GameModeDisplayName_ChillPill), new Dictionary<SystemLanguage, string>
          {
            { SystemLanguage.English, "Chill Pill" },
            { SystemLanguage.Polish, "Luz-blues" },
            { SystemLanguage.ChineseSimplified, "放松" },
            { SystemLanguage.German, "Zen" }, // like 'meditation'
            { SystemLanguage.Russian, "Релакс" },
            { SystemLanguage.Korean, "번역하다" },
            { SystemLanguage.Japanese, "訳す" },
            { SystemLanguage.Spanish, "traducir" },
            { SystemLanguage.Romanian, "Relaxare" }, //like relaxing
            { SystemLanguage.Finnish, "Rento Pilleri" },
            { SystemLanguage.Unknown, "mugh" },
          }
        },
        { nameof(GameModeDisplayName_TimingAttack), new Dictionary<SystemLanguage, string>
          {
            { SystemLanguage.English, "Timing Attack" },
            { SystemLanguage.Polish, "Wyczucie czasu" },
            { SystemLanguage.ChineseSimplified, "定时攻击" },
            { SystemLanguage.German, "Taktrausch" }, // like 'timing rush'
            { SystemLanguage.Russian, "Тайминг ударов" }, // there is no good translation for this(
            { SystemLanguage.Korean, "번역하다" },
            { SystemLanguage.Japanese, "訳す" },
            { SystemLanguage.Spanish, "traducir" },
            { SystemLanguage.Romanian, "Atac de timp" },
            { SystemLanguage.Finnish, "Ajoitus Hyökkäys" },
            { SystemLanguage.Unknown, "mugh" },
          }
        },
        { nameof(GamePausedHeaderText), new Dictionary<SystemLanguage, string>
          {
            { SystemLanguage.English, "Pause" },
            { SystemLanguage.Polish, "Pauza" },
            { SystemLanguage.ChineseSimplified, "暂停" },
            { SystemLanguage.German, "Pause" },
            { SystemLanguage.Russian, "Пауза" },
            { SystemLanguage.Korean, "번역하다" },
            { SystemLanguage.Japanese, "訳す" },
            { SystemLanguage.Spanish, "traducir" },
            { SystemLanguage.Romanian, "Pauză" },
            { SystemLanguage.Finnish, "Tauko" },
            { SystemLanguage.Unknown, "mugh" },
          }
        },
        { nameof(LevelClearedHeaderText), new Dictionary<SystemLanguage, string>
          {
            { SystemLanguage.English, "Level Cleared" },
            { SystemLanguage.Polish, "Poziom ukończony" },
            { SystemLanguage.ChineseSimplified, "关卡已通过" },
            { SystemLanguage.German, "Level abgeschlossen" },
            { SystemLanguage.Russian, "Уровень пройден" },
            { SystemLanguage.Korean, "번역하다" },
            { SystemLanguage.Japanese, "訳す" },
            { SystemLanguage.Spanish, "traducir" },
            { SystemLanguage.Romanian, "Nivel terminat" },
            { SystemLanguage.Finnish, "Kappale Läpi!" }, // means "song cleared", makes more sense
            { SystemLanguage.Unknown, "mugh" },
          }
        },
        { nameof(MenuButtonText), new Dictionary<SystemLanguage, string>
          {
            { SystemLanguage.English, "Menu" },
            { SystemLanguage.Polish, "Menu" },
            { SystemLanguage.ChineseSimplified, "菜单" },
            { SystemLanguage.German, "Menü" },
            { SystemLanguage.Russian, "Меню" },
            { SystemLanguage.Korean, "번역하다" },
            { SystemLanguage.Japanese, "訳す" },
            { SystemLanguage.Spanish, "traducir" },
            { SystemLanguage.Romanian, "Meniu" },
            { SystemLanguage.Finnish, "Valikko" },
            { SystemLanguage.Unknown, "mugh" },
          }
        },
        { nameof(ResumeButtonText), new Dictionary<SystemLanguage, string>
          {
            { SystemLanguage.English, "Resume" },
            { SystemLanguage.Polish, "Wznów" },
            { SystemLanguage.ChineseSimplified, "恢复" },
            { SystemLanguage.German, "Fortsetzen" },
            { SystemLanguage.Russian, "Продолжить" },
            { SystemLanguage.Korean, "번역하다" },
            { SystemLanguage.Japanese, "訳す" },
            { SystemLanguage.Spanish, "traducir" },
            { SystemLanguage.Romanian, "Reluă" },
            { SystemLanguage.Finnish, "Jatka" },
            { SystemLanguage.Unknown, "mugh" },
          }
        },
        { nameof(PlaylistStats_MapsCount), new Dictionary<SystemLanguage, string>
          {
            { SystemLanguage.English, "Number of songs" },
            { SystemLanguage.Polish, "Liczba utworów" },
            { SystemLanguage.ChineseSimplified, "歌曲数量" },
            { SystemLanguage.German, "Anzahl der Lieder" },
            { SystemLanguage.Russian, "Кол-во песен" },
            { SystemLanguage.Korean, "번역하다" },
            { SystemLanguage.Japanese, "訳す" },
            { SystemLanguage.Spanish, "traducir" },
            { SystemLanguage.Romanian, "Număr de melodii" },
            { SystemLanguage.Finnish, "Kappaleita" },
            { SystemLanguage.Unknown, "mugh" },
          }
        },
        { nameof(PlaylistStats_AverageScore), new Dictionary<SystemLanguage, string>
          {
            { SystemLanguage.English, "Average score" },
            { SystemLanguage.Polish, "Średnia ocena" },
            { SystemLanguage.ChineseSimplified, "平均分" },
            { SystemLanguage.German, "durchschn. Punktzahl" },
            { SystemLanguage.Russian, "Средний счет" },
            { SystemLanguage.Korean, "번역하다" },
            { SystemLanguage.Japanese, "訳す" },
            { SystemLanguage.Spanish, "traducir" },
            { SystemLanguage.Romanian, "Scorul mediu" }, //no good translation for average.. mediu in romanian means medie which is like a total of your notes at school.
            { SystemLanguage.Finnish, "Keskiarvo pisteet" },
            { SystemLanguage.Unknown, "mugh" },
          }
        },
        { nameof(ViewPlaylistSongsButtonText), new Dictionary<SystemLanguage, string>
          {
            { SystemLanguage.English, "View Songs" },
            { SystemLanguage.Polish, "Zobacz utwory" },
            { SystemLanguage.ChineseSimplified, "查看歌曲" },
            { SystemLanguage.German, "Lieder anzeigen" },
            { SystemLanguage.Russian, "Просмотреть песни" },
            { SystemLanguage.Korean, "번역하다" },
            { SystemLanguage.Japanese, "訳す" },
            { SystemLanguage.Spanish, "traducir" },
            { SystemLanguage.Romanian, "Vedeți melodiile" },
            { SystemLanguage.Finnish, "Näytä Kappaleet" },
            { SystemLanguage.Unknown, "mugh" },
          }
        },
        { nameof(PlaylistLabel), new Dictionary<SystemLanguage, string>
          {
            { SystemLanguage.English, "Playlist" },
            { SystemLanguage.Polish, "Playlista" },
            { SystemLanguage.ChineseSimplified, "歌单" },
            { SystemLanguage.German, "Playlist" },
            { SystemLanguage.Russian, "Плейлист" },
            { SystemLanguage.Korean, "번역하다" },
            { SystemLanguage.Japanese, "訳す" },
            { SystemLanguage.Spanish, "traducir" },
            { SystemLanguage.Romanian, "Playlist" },
            { SystemLanguage.Finnish, "Soittolista" },
            { SystemLanguage.Unknown, "mugh" },
          }
        },
        { nameof(GameScoreLabel_Accuracy), new Dictionary<SystemLanguage, string>
          {
            { SystemLanguage.English, "A" }, // as in 'Accuracy'
            { SystemLanguage.Polish, "C" }, // as in 'Celność'
            { SystemLanguage.ChineseSimplified, "A" }, // as in 'Accuracy'
            { SystemLanguage.German, "G" }, // as in 'Genauigkeit'
            { SystemLanguage.Russian, "Т" }, // as in 'Точность'
            { SystemLanguage.Korean, "번역하다" },
            { SystemLanguage.Japanese, "訳す" },
            { SystemLanguage.Spanish, "traducir" },
            { SystemLanguage.Romanian, "P" }, //As in „precizie”
            { SystemLanguage.Finnish, "T" }, // as in 'Tarkkuus'
            { SystemLanguage.Unknown, "mugh" },
          }
        },
        { nameof(GameScoreLabel_BlocksHitCount), new Dictionary<SystemLanguage, string>
          {
            { SystemLanguage.English, "H" }, // as in 'Hits'
            { SystemLanguage.Polish, "T" }, // as in 'Trafienia'
            { SystemLanguage.ChineseSimplified, "H" }, // as in 'Hits'
            { SystemLanguage.German, "T" }, // as in 'Treffer'
            { SystemLanguage.Russian, "У" }, // as in 'Удары'
            { SystemLanguage.Korean, "번역하다" },
            { SystemLanguage.Japanese, "訳す" },
            { SystemLanguage.Spanish, "traducir" },
            { SystemLanguage.Romanian, "L" }, // as in „Lovituri”
            { SystemLanguage.Finnish, "O" }, // as in 'Osumia'
            { SystemLanguage.Unknown, "mugh" },
          }
        },
        { nameof(GameScoreLabel_BlocksMissedCount), new Dictionary<SystemLanguage, string>
          {
            { SystemLanguage.English, "M" }, // as in 'Misses'
            { SystemLanguage.Polish, "N" }, // as in 'Nietrafienia'
            { SystemLanguage.ChineseSimplified, "M" }, // as in 'Misses'
            { SystemLanguage.German, "F" }, // as in 'Fehltreffer'
            { SystemLanguage.Russian, "П" }, // as in 'Промахи'
            { SystemLanguage.Korean, "번역하다" },
            { SystemLanguage.Japanese, "訳す" },
            { SystemLanguage.Spanish, "traducir" },
            { SystemLanguage.Romanian, "R" }, // as in „Ratări”
            { SystemLanguage.Finnish, "H" }, // as in 'Huteja'
            { SystemLanguage.Unknown, "mugh" },
          }
        },
      };
  }
}
