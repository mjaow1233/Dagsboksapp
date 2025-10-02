Summering av appen;
C# .NET consolapp. Körs med .NET runtime library 9.0.
Dagboksapp som sparar inlägg till .txt fil. 
Menuchoice.cs -> Enum med menyval
Diary.cs -> Logik med dictionary för att handskas med dagboksfunktioner.

Programmet använder en fil som heter dagbok.txt för att spara/ladda inlägg.

Reflektion;
Jag började projektet med att använda min tidigare todo-app som bas. 
Jag valde att använda en .txt fil för det var det jag visste hur man gjorde.
Försökte använda LINQ men svårt, reverterade och byggde om sökfunktionen.

Under majoriteten av tiden användes list till allting men implementerade till sist Dictionary.
Tror inte det är risk för att det är mycket data att läsa igenom i denna app, men dictionary är definitivt effektivare
eftersom loopen behöver inte gå igenom alla inlägg varje gång.

Under processens gång kämpade jag en hel del med formatering, både vid skriv och inläsning.
Programmet hade också en tendens att göra dubletter vid load/save och detta tror jag att kunde undvikit
ifall jag hade listat ut hur man ger en typ av "tag" till varje inlägg istället för att räkna dem när man läser in filen.
