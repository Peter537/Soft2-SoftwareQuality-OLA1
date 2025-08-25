# Teststrategi

## Overordnet teststrategi og mål:
Denne teststrategien er for at sikre høj softwarekvalitet, reducere risici og øge vedligeholdelsesvenlighed på tværs af alle udviklingsprojekter, herunder dem demonstreret i vores GitHub-repository. Vores filosofi er at prioritere tidlig fejlfinding, robusthed og skalerbarhed gennem en struktureret testtilgang, der balancerer forskellige testniveauer og -typer for at opnå omfattende dækning og tillid til softwarens pålidelighed. Målet er at levere produkter, der opfylder bruger- og forretningskrav, overholder tekniske standarder og minimerer teknisk gæld, samtidig med at vi understøtter løbende forbedring gennem standardiserede værktøjer, metoder og målinger.


## Testniveauer og typer:
Strategien dækker flere testniveauer for at adressere forskellige kvalitetsaspekter. Unit-tests validerer individuelle kodeenheder for at sikre korrekt funktionalitet i isolation. Vi definer unit til at være en metode/function. Integrationstests verificerer samspillet mellem moduler for at opdage fejl i grænseflader og dataflow.
Specifikationsbaserede tests er afledt direkte fra applikationens krav, som at kategorisere opgaver, og implementeres som en del af unit-testsuiten.


## Testværktøjer og standarder:
Vi bruger MSTest som primær testramme i C#-projekter, da det passer godt sammen med Visual Studio og giver mulighed for detaljerede annotationer, som styrker dokumentation og samarbejde i teamet. Moq-biblioteket bruges til at lave testdoubles, så vi kan lave isolerede og stabile unit-tests. Mutationstesting med værktøjer som Stryker.NET bruges til at vurdere testens styrke og finde svagheder i dækningen. Vores standarder kræver, at tests er overskuelige, godt beskrevet og fokuserer på klare mål. Det sikrer, at de er nemme at læse, gentage og vedligeholde, og at kvaliteten holdes ens på tværs af projekter.


## Roller og ansvar:
Udviklerne har ansvaret for at skrive og vedligeholde tests til deres egen kode. De skal samarbejde om at sikre, at testdækningen er tilstrækkelig, og at tests er effektive og relevante.


## Risikostyring:
Strategien håndterer risici som manglende testdækning, integrationsfejl og manglende opfyldelse af krav ved at bruge en kombination af forskellige testtyper og løbende evaluering. Områder med høj risiko, fx kritiske forretningsfunktioner og komplekse afhængigheder, prioriteres gennem grundige unit- og integrationstests. Testdoubles bruges til at mindske risikoen ved eksterne afhængigheder, og mutationstesting hjælper med at opdage svage tests. Løbende målinger som code coverage gør det nemmere at spotte og reducere fejl.


## Anvendelse af testdoubles:
I tråd med Martin Fowlers principper om testdoubles bruger vi testdoubles til at erstatte produktionsafhængigheder og gøre tests hurtigere og mere stabile. Vi bruger mocks via Moq til at simulere grænseflader, så vi kan teste korrekt opførsel uden eksterne systemer. Mocks bruges til at tjekke interaktioner, mens stubs leverer faste svar for at simulere data, og dummies bruges til irrelevante parametre, når det er nødvendigt. Det mindsker afhængigheder og sideeffekter i tests, og annotationer i koden forklarer brugen, så det er tydeligt og nemt for udviklere at gentage.


## Mutationstesting, verifikation og validering:
Mutationstesting gøres med Stryker.NET for at vurdere, hvor stærk testpakken er, ved at ændre små dele af koden (mutanter) og se, hvor mange der bliver fanget af tests. Resultaterne bruges til at forbedre dækningen ved at tilføje edge-cases og dermed gøre testen mere robust. Verifikation sikrer, at softwaren bygges korrekt i forhold til specifikationerne gennem unit- og integrationstests, som køres tidligt i udviklingen. Validering sker ved at bekræfte, at produktet opfylder brugerbehov efter færdiggørelse og sikres gennem specifikationsbaserede tests og mutation, som validerer end-to-end-kvalitet og reducerer omkostninger ved tidlig fejlfinding.
