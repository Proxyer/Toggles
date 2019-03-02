# Toggles
A mod for RimWorld.

#### Hide and disable things in RimWorld:
* Incidents
* Alerts
* Letters
* Readouts
* Weather Overlays
* Buttons
* Links
* and more...

_Toxic Fallout_ bumming you out? Game too easy with all the wanderers joining the colony? Or maybe you're just tired of constantly getting notified about it? 
This mod allows you to do that. Just go into the mod settings and choose your toggles. 

## News
 - _Hotkeys!_ Players can now assign individual or groups of toggles to key bindings, and toggle them instantly while playing.
 - Added toggles for hiding _Weather Overlays_.
 - Fixed bug where Caravan Trading window would be blank or yield errors.

## Instructions
Most of the settings are straightforward: Toggle things on or off in the mod settings, or assign them to hotkeys and toggle during gameplay. That's all you need to know.
However, here is a bit more in-depth on this mods' features and mechanics:

### GUI
This mod can toggle graphical elements. It does this by either blocking the game from drawing them at all, or replacing their textures with completely transparent ones, depending on how they're set up.
E.g. buttons and icons.

## Events
Incidents, alerts and letters are categorized as events. Instead of me manually telling the mod what to toggle, like buttons and links, these are automatically generated. The mod searches all mods in the load order for incidents, alerts and letters, and adds them as toggles.

### Incidents
These are events that occur during gameplay that alter game conditions, like _Volcanic Winter_ or _Infestation_. Whenever the game attempts to execute these events, _Toggles_ intercepts them and checks whether the specific incident is disabled in the settings. If it is, the incident is blocked.

### Alerts
An _alert_ is a notification. It informs the player of things he or she may want to know about, like _Need Colonist Beds_ or _Need Research Project_. Alerts remain visible on the screen until the thing has been fixed. Whenever the game attempts to execute an alert, _Toggles_ checks if that specific alert is disabled in the settings, and blocks it if it is. As soon as the setting is reactivated, all the blocked alerts reappear on the screen. Do note, this does not block the underlying cause of the alert, just the notification itself.

### Letters
Letters are like alerts, but only notifies the player once - when an incident occurs. Toggles handles letters in two different ways:

RimWorld categorizes letters by severity and type, like _Positive Event_ and _Small Threat_. Whenever a letter is about to appear on the screen, _Toggles_ checks the category of the letter against the settings, and blocks or allows accordingly. Simple. 

However, this mod also allows for disabling specific letters, and this is where it becomes a bit more tricky. Why? Because individual letters are currently only recognized through their text labels, and RimWorld allows for letter labels to include variables, making for an unknown number of existing letters. 

Here's an example: there's an incident where a wild animal becomes self-tamed and joins the colony. The letter label for this incident is "_{animal} self-tamed_", like "_Rat self-tamed_" or "_Squirrel self-tamed_". So there is actually a unique _Self-Tamed_-letter for each unique animal, making for a very large number of letters for the same incident. 

There are no simple, automatic and maintainable solutions to this yet. I'm thinking about it. Until then this is unfortunately a drawback players have to deal with. It is worth noting that most letters **don't** contain variables.

For the same reason, there is no complete, fixed database of specific letters to put into the mod at startup. Instead, _Toggles_ taps into RimWorld's _History_ tab, exposing previously received letters for toggling identical ones in the future. Individual letters that have been claimed in the Mod settings' _Letters_ page cannot currently be removed one by one, but will be unclaimed if the mod is reset with the reset button.
Individual letters always get priority over categorized letters.

## Dependencies
[HugsLib](https://github.com/UnlimitedHugs/RimworldHugsLib)

## Compatibility
* Developed and tested mainly for vanilla RimWorld. However, should play nice with most mods.
* Should be completely safe to add and remove from save games.

## Usage
You can use this mod in any modpack, or fork it. Please message me first if you do. I'd love to know where the mod is being used. However, please don't re-upload a translated version of the mod. If you wish to translate the mod into another language, contact me. Also feel free to message me about any suggestions or improvements you think of. I'd love to hear it.

## How to install:
- Subscribe at the [Steam Workshop](https://steamcommunity.com/sharedfiles/filedetails/?id=1666097824)

or

- Download the latest zip-file from [Latest release.](https://github.com/krafs/Toggles/releases)
- Unzip the contents and place them in your RimWorld/Mods folder.
- Activate the mod in the mod menu in the game.
