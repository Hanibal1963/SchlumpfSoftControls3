# WizardControl

Ein Control zum Erstellen eines Assistenten.

Dieses Steuerelement wurde von mir in Anlehnung an den [Wizard](https://marketplace.visualstudio.com/items?itemName=vs-publisher-106990.RuWizard) von [Klaus Rutkowski](https://marketplace.visualstudio.com/publishers/vs-publisher-106990) entwickelt.

Sinn dieses Projekts ist für mich der Lerneffekt sowie eventuelle Anpassungen vornehmen zu können.

## Eigenschaften von Wizard

- **VisibleHelp** - Ruft die Sichtbarkeit Status der Hilfeschaltfläche ab oder legt diesen fest.
- **Pages** - Ruft die Auflistung der Assistentenseiten in diesem Registerkartensteuerelement ab.
- **ImageHeader** - Ruft das in der Kopfzeile der Standardseiten angezeigte Bild ab oder legt dieses fest.
- **ImageWelcome** - Ruft das auf den Begrüßungs- und Abschlussseiten angezeigte Bild ab oder legt es fest.
- **Dock** - Ruft ab oder legt fest, an welcher Kante des übergeordneten Containers ein Steuerelement angedockt ist. +
- **SelectedPage** - Ruft die aktuell ausgewählte Seite ab oder legt diese fest.
- **HeaderFont** - Ruft die Schriftart ab, die zum Anzeigen der Beschreibung einer Standardseite verwendet wird, oder legt diese fest.
- **HeaderTitleFont** - Ruft die Schriftart ab, die zum Anzeigen des Titels einer Standardseite verwendet wird, oder legt diese fest.
- **WelcomeFont** - Ruft die Schriftart ab, die zum Anzeigen der Beschreibung einer Begrüßungs- oder Abschlussseite verwendet wird, oder legt diese fest.
- **WelcomeTitleFont** - Ruft die Schriftart ab, die zum Anzeigen des Titels einer Begrüßungs- oder Abschlussseite verwendet wird, oder legt diese fest.
- **NextEnabled** - Ruft den Status der Schaltfläche "weiter" ab oder legt diesen fest.
- **BackEnabled** - Ruft den Status der Schaltfläche "zurück" ab oder legt diesen fest.
- **CancelText** - Ruft den Text der Schaltfläche "Abbrechen" ab oder legt diesen fest.
- **HelpText** - Ruft den Text der Schaltfläche "Hilfe" ab oder legt diesen fest.

## Eigenschaften von WizardPages

- **Style** - Ruft den Stil der Assistentenseite ab oder legt diesen fest.
- **Title** - Ruft den Titel der Assistentenseite ab oder legt diesen fest.
- **Description** - Ruft die Beschreibung der Assistentenseite ab oder legt diese fest.

## Methoden von Wizard

- **Next** - Entspricht einem Klick auf die Schaltfläche "weiter".
- **Back** - Entspricht einem Klick auf die Schaltfläche "zurück".

## Ereignisse von Wizard

- **BeforeSwitchPages**  - Tritt auf, bevor die Seiten des Assistenten gewechselt werden, um dem Benutzer die Möglichkeit zur Validierung zu geben.
- **AfterSwitchPages**  -  Tritt auf, nachdem die Seiten des Assistenten gewechselt wurden, und gibt dem Benutzer die Möglichkeit, die neue Seite einzurichten.
- **Cancel** - Tritt auf wenn der Benutzer auf Abbrechen geklickt hat.
- **Finish** - Tritt auf, wenn der Assistent abgeschlossen ist, und gibt dem Benutzer die Möglichkeit, zusätzliche Aufgaben zu erledigen.
- **Help** - Tritt auf, wenn der Benutzer auf die Hilfeschaltfläche klickt.
