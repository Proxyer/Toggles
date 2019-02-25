# Toggles
A mod for RimWorld.


#### Allows for disabling or hiding things in RimWorld:
* Incidents
* Alerts
* Letters
* Buttons
* Links
* Readouts
* and more...

_Toxic Fallout_ bumming you out? Game too easy with all the wanderers joining the colony? Or maybe you're just tired of constantly getting notified about it? 
Or how about those menu buttons: do you really need the _Tutorial_ anymore? And how often do you watch the _Credits_? Did you ever wish you could hide those and just see what you actually use?

This mod allows you to do that. Just go into the mod settings and choose your toggles, and keep playing.

## Instructions
Most of the settings are straightforward: Toggle things on or off in the mod settings. However, some settings, especially _Letters_, require some explaining.

## GUI
Toggling graphical elements works by either blocking the game from drawing them at all, or replacing their textures with completely transparent ones, depending on how they're set up.

## Incidents
These are events that occur during gameplay that alter game conditions, like _Volcanic Winter_ or _Infestation_. Whenever the game attempts to execute these events, _Toggles_ intercepts them and checks whether the specific incident is disabled in the settings. If it is, the incident is blocked.

## Alerts
An _alert_, just like a _letter_, is a notification. It informs the player of things he or she may want to know about, like _Need Colonist Beds_ or _Need Research Project_. However, in contrast with letters, alerts are visible on the screen until the thing has been fixed. Whenever the game attempts to execute an alert, _Toggles_ checks if that specific alert is disabled in the settings, and blocks it if it is. As soon as the setting is reactivated, all the blocked alerts reappear on the screen. Do note, this does not block the underlying cause of the alert, just the notification itself.

## Letters
There are a few things to explain about letters in RimWorld and how this mod goes about toggling them. This is a bit in-depth, for those curious minds out there. 

### tl;dr
_Toggles_ can block letters by category or specific letter labels.

### In-depth
As mentioned, letters are like alerts, but only notifies the player once - when an incident occurs. RimWorld categorizes letters by severity and type, like _Positive Event_ and _Small Threat_. Whenever a letter is about to appear on the screen, _Toggles_ checks the category of the letter against the settings, and blocks or allows accordingly. Simple. 

However, this mod also allows for disabling specific letters, and this is where it becomes a bit more tricky. Why? Because individual letters are currently only recognized through their text labels, and RimWorld allows for letter labels to include variables. 

Here's an example: there's an incident where a wild animal becomes self-tamed and joins the colony. The letter label for this incident is "_{animal} self-tamed_", like "_Rat self-tamed_" or "_Squirrel self-tamed_". So there is actually a unique _Self-Tamed_-letter for each unique animal, making for a very big number of letters for the same incident. 

There are no simple, yet automatic and maintainable solutions to this yet. I'm thinking about it. Until then this is unfortunately a drawback players have to deal with. It is worth noting that most letters don't contain variables.

For the same reason, there is no complete, fixed database of specific letters to put into the mod at startup. Instead, _Toggles_ taps into RimWorld's _History_ tab, making received letters available as toggles in the mod settings. In the mod settings' _Letters_ page, players can choose which specific letters to turn into toggles. These cannot be individually dropped as toggles at this time, if for some reason the player wants to prioritize letter categories instead. However, using the _Reset_-button resets all settings, including the specific letters. They can then be readded individually in the _Letters_ page.

Another thing about letters that might be worth keeping in mind, that I have already touched upon, is that the settings of specific letters always trump categories. For instance, if a player has the letter _Ambrosia sprout_ enabled, but the category _Positive Events_ disabled, the player will still get a letter. This is because _Ambrosia sprout_, being a specific letter, always gets priority over categories.

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
