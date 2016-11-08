# Whitelist Plugin for KEKCON Tool (BattlEye Client)
**2016 (c) by Scripticus - Regnum4Games**

[![N|Solid](https://www.regnum4games.de/upload/r4g_logo.png)](https://www.regnum4games.de)


### Erklärung
> Wie man es von den BattlEye-Extended-Controls her kennt, gibt es die Möglichkeit Spieler auf einem Arma3-Server zu whitelisten. Mit diesem Plugin wird dieses Feature auch in Kekcon implementiert.



### Download
[*** zum Download ***](https://github.com/Scriqticus/Kekcon-Whitelist/releases)


### Features
- Die Whitelist lässt sich inGame per Global-Chat aktivieren, deaktivieren und neu laden.
- Angabe von Whitelist-GUID's in der Config.
- Angabe von Admin-GUID's in der Config.
- Angabe eines Kick-Grunds in der Config.

### Konfiguration

Konfig-Pfad: `plugins/config/whitelist.cfg`

**Alle Parameter müssen in der Config enthalten sein, sonst kommt ein Fehler beim Laden des Plugins in KEKCON !**


***activated***
- *true:* Whitelist aktiviert
- *false:* Whitelist deaktiviert
```sh
activated = false
oder
activated = true
```

***kickreason***
```sh
kickreason = Der Server wird momentan gewartet
oder
kickreason = Bitte lasse dich in unserem Forum whitelisten: www.forum.de
```

***whitelist***
```sh
whitelist = guid1,guid2,guid3,...
```

***admins***
```sh
admins = guid1,guid2,guid3,...
```

### Lizenz
Die Nutzung dieses Plugins ist komplett kostenlos. Mit der Nutzung des Plugins erklärt sich der Nutzer automatisch einverstanden, dieses Plugin und den zugehörigen Quellcode nicht als eigenes Werk anzupreisen. Die kommerzielle Weiterverbreitung des Plugins ist untersagt. Bei Missachtung dieser Punkte, können notgedrungen auch rechtliche Schritte eingeleitet werden.
Dieses Plugin wurde für die ArmA 3 Community und deren Gameserver entwickelt und soll dazu beitragen, Kekcon künftig alternativ zu den veralteten BEC (BattlEye Extended Controls) zu verwenden.

### Spenden
Dieses Plugin wurde ursprünglich für Regnum4Games entwickelt, dort wird ebenfalls ein Gameserver betrieben. Falls Euch das Plugin gefällt und Ihr unseren Server unterstützen wollt, kommt doch einfach mal vorbei. Wir freuen uns auch über jede kleine Spende, die zum Erhalt und der Weiterentwicklung unseres Servers beiträgt. Außerdem tragt Ihr damit auch zur Weiterentwicklung unserer Plugins bei.

### Fragen, Probleme, Vorschläge
Habt ihr schwierigkeiten beim Installieren des Plugins, gibt es sonstige Probleme oder ist euch ein Feature eingefallen, welches ihr gerne im Plugin sehen wollt, dann könnt ihr mich einfach über unser Forum oder unseren Teamspeak³-Server kontaktieren.

**Homepage:** [www.regnum4games.de](https://www.regnum4games.de) 

**Forum:** [www.regnum4games.de/forum/](https://www.regnum4games.de/forum/)

**TeamSpeak³:** [ts.regnum4games.de](http://ts.regnum4games.de)

[![paypal](https://www.paypalobjects.com/de_DE/DE/i/btn/btn_donateCC_LG.gif)](https://www.paypal.com/cgi-bin/webscr?cmd=_donations&business=regnum4games%40web%2ede&lc=DE&item_name=Regnum4Games-Whitelist-Plugin-Spende&no_note=0&currency_code=EUR&bn=PP%2dDonationsBF%3abtn_donateCC_LG%2egif%3aNonHostedGuest)