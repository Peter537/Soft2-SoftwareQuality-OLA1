# Reflektions og Diskussions dokument

I udviklingen af To Do Liste applikationen bidrager hver testtype til softwarekvaliteten på unikke måder:

- Unit-tests sikrer isoleret logik og tidlig fejlfinding, hvilket øger vedligeholdelsesvenligheden ved at fange fejl hurtigt.
- Integrationstests verificerer komponentinteraktioner for at forhindre grænseflade og dataflow problemer, der kunne kompromittere systemets stabilitet.
- Specifikationsbaserede tests validerer mod krav for at garantere brugerværdi.

Mutationstestingens analyse viste en score på 72%. Vi synes det er et godt værktøj til man kan bruge for at sikre i fremtiden hvis der skulle ske kodeændringer så vil man allerede være sikret med sine tests at hvis der sker en fejl i koden, så har testene allerede fundet den fejl inden det bliver offentliggjort.

Ifølge Plutora-artiklen er verifikation processen med at tjekke specifikations overholdelse under udvikling gennem inspektioner, mens validering sikrer, at det færdige produkt møder stakeholder-forventninger via test. I vores strategi dækker unit- og integrationstests verifikation ved at bygge rigtigt, mens specifikationsbaserede tests håndterer validering for at bygge det rigtige produkt.

Peter Morlions artikel understreger softwarekvalitet som intern (kodearkitektur) og ekstern (brugertilfredshed), målt via metrics som cyclomatic complexity og code coverage. Vores teststrategi evalueres positivt, da den reducerer teknisk gæld gennem høj dækning, men hvis vi yderligere skulle udvikle test strategien, så ville CI-integration kunne forbedre kvaliteten og performance-tests for at måle ekstern kvalitet bedre og automatisere metrics i pipelines for løbende forbedring.

For at sikre højere kvalitet kunne vi udvide med flere edge-cases og indføre load-testing.

Martin Fowlers testkategorier, som i testpyramiden, anbefaler mange unit-tests i bunden for hurtig feedback, færre integrationstests i midten for interaktionscheck og få end-to-end-tests øverst. Vores implementerede tests klassificeres som solitære unit-tests (med mocks) i bunden og sociable integrationstests i midten, hvilket følger pyramiden for balanceret portefølje.
