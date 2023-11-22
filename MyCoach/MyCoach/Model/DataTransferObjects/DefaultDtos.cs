using MyCoach.Model.Defines;
using System;
using System.Collections.ObjectModel;

namespace MyCoach.Model.DataTransferObjects
{
    /// <summary>
    ///     Stellt Listen aller Dtos mit Standardwerten für jede Datanstruktur zur Verfügung.
    /// </summary>
    public static class DefaultDtos
    {
        public static ObservableCollection<Category> Categories
        {
            get => new ObservableCollection<Category>()
            {
                new Category { ID = ExerciseCategory.WarmUp, Name = "Aufwärmübungen", Count = 3, Active = true, Type = ExerciseType.WarmUp },
                new Category { ID = ExerciseCategory.Category1, Name = "Arme", Count = 0, Active = true, Type = ExerciseType.Training },
                new Category { ID = ExerciseCategory.Category2, Name = "Bauch", Count = 0, Active = true, Type = ExerciseType.Training },
                new Category { ID = ExerciseCategory.Category3, Name = "Seiten", Count = 0, Active = true, Type = ExerciseType.Training },
                new Category { ID = ExerciseCategory.Category4, Name = "Rücken", Count = 0, Active = true, Type = ExerciseType.Training },
                new Category { ID = ExerciseCategory.Category5, Name = "Beine", Count = 0, Active = true, Type = ExerciseType.Training },
                new Category { ID = ExerciseCategory.Category6, Name = "", Count = 0, Active = false, Type = ExerciseType.Training },
                new Category { ID = ExerciseCategory.Category7, Name = "", Count = 0, Active = false, Type = ExerciseType.Training },
                new Category { ID = ExerciseCategory.Category8, Name = "", Count = 0, Active = false, Type = ExerciseType.Training },
                new Category { ID = ExerciseCategory.CoolDown, Name = "Dehnübungen", Count = 3, Active = true, Type = ExerciseType.CoolDown }
            };
        }

        public static ObservableCollection<Exercise> Exercises
        {
            get => new ObservableCollection<Exercise>()
            {
                // Warm Up

                new Exercise
                {
                    ID = 0,
                    Category = ExerciseCategory.WarmUp,
                    Count = 60,
                    Name = "Hampelmann",
                    Unit = "Sekunden",
                    Scores = 0,
                    Info = "Stell dich gerade hin. Hüpfe leicht und spreize dabei die Beine, bis du mehr als schulterbreit stehst. Hebe dabei die Arme gleichzeitig seitlich nach oben und strecke sie, so dass sich die Hände über dem Kopf berühren. Spring dann wieder in die Ausgangsstellung zurück.",
                    Active = true
                },

                new Exercise
                {
                    ID = 1,
                    Category = ExerciseCategory.WarmUp,
                    Count = 60,
                    Name = "Kettenfauststöße",
                    Unit = "Sekunden",
                    Scores = 0,
                    Info = "Boxe abwechselnd mit der linken und rechten Faust auf ein unsichtbares Ziel vor dir. Versuche die Schläge möglichst schnell folgen zu lassen.",
                    Active = true
                },

                new Exercise
                {
                    ID = 2,
                    Category = ExerciseCategory.WarmUp,
                    Count = 60,
                    Name = "Radfahren",
                    Unit = "Sekunden",
                    Scores = 0,
                    Info = "Lege dich auf den Rücken und ziehe ein Bein zur Brust, das andere streckst du in der Luft aus. Wechsle nun möglichst schnell die Position der Beine, während der gesamten Übungszeit berührt kein Bein den Boden.",
                    Active = true
                },

                new Exercise
                {
                    ID = 3,
                    Category = ExerciseCategory.WarmUp,
                    Count = 15,
                    Name = "Hockstrecksprünge",
                    Unit = "Wiederholungen",
                    Scores = 0,
                    Info = "Hocke dich auf den Boden und führe aus dieser Position gerade Sprünge nach oben aus, nach denen du wieder in die Hocke gehst. Strecke bei jedem Sprung deine Arme so weit wie möglich nach oben aus.",
                    Active = true
                },

                // Category 1

                new Exercise
                {
                    ID = 4,
                    Category = ExerciseCategory.Category1,
                    Count = 20,
                    Name = "Hantelheben 5 kg",
                    Unit = "Wiederholungen je Seite",
                    Scores = 10,
                    Info = "Nimm dir eine Hantel in jede Hand und lasse die Arme ausgestreckt an deiner Seite herunterhängen. Beuge nun abwechselnd je Seite (oder wenn du magst auch gleichzeitig) deine Arme und hebe die Hanteln an.",
                    Active = true
                },

                new Exercise
                {
                    ID = 5,
                    Category = ExerciseCategory.Category1,
                    Count = 20,
                    Name = "Hantelheben Trizeps 5 kg",
                    Unit = "Wiederholungen",
                    Scores = 10,
                    Info = "Greife mit beiden Händen eine Hantel und lege sie hinter deinem Kopf in den Nacken. Strecke aus dieser Position heraus deine Arme aus und hebe die Hantel so weit wie möglich über deinen Kopf.",
                    Active = true
                },

                new Exercise
                {
                    ID = 6,
                    Category = ExerciseCategory.Category1,
                    Count = 15,
                    Name = "Liegestütze",
                    Unit = "Wiederholungen",
                    Scores = 10,
                    Info = "Lege dich mit dem Bauch auf den Boden. Setze die Handflächen neben deinen Schultern auf und strecke die Arme, sodass dein Oberkörper abhebt. Jetzt bist du in der Ausgangshaltung. Beuge nun deine Arme und bewege dein Gesicht bis fast zum Boden. Strecke dann deine Arme wieder aus, um deinen Oberkörper wieder nach oben zu bringen. Achte darauf, dass dein Rücken immer gerade ist. Wenn dir die Übung zu schwer fällt, kannst du alternativ auch während der Ausführung die Knie auf dem Boden aufsetzen.",
                    Active = true
                },

                new Exercise
                {
                    ID = 7,
                    Category = ExerciseCategory.Category1,
                    Count = 20,
                    Name = "Dips",
                    Unit = "Wiederholungen",
                    Scores = 10,
                    Info = "Für Dips brauchst du eine stabile, ca. kniehohe Kante, zum Beispiel die einer Bank. Stell dich mit dem Rücken zur Kante und setze deine Handballen auf der Kante auf, sodass die Finger zum Boden zeigen. Die Füße sind von der Kante weggestreckt und der Po hängt nach unten. Führe aus dieser Position deinen Po bis fast zum Boden und strecke danach die Arme wieder aus, um dich wieder nach oben zu bewegen.",
                    Active = true
                },

                // Category 2

                new Exercise
                {
                    ID = 8,
                    Category = ExerciseCategory.Category2,
                    Count = 30,
                    Name = "Situps",
                    Unit = "Wiederholungen",
                    Scores = 10,
                    Info = "Lege dich auf den Rücken. Winkel deine Beine an und halte deine Hände neben den Schläfen. Bewege nun deinen Oberkörper nach oben, bis Ellenbogen die Knie berühren. Um dich während der Übung zu stabilisieren, kannst du deine Füße auch fixieren, indem du sie zum Beispiel unter eine Sofakante schiebst oder jemanden bittest, deine Füße festzuhalten.",
                    Active = true
                },

                new Exercise
                {
                    ID = 9,
                    Category = ExerciseCategory.Category2,
                    Count = 90,
                    Name = "Planke",
                    Unit = "Sekunden",
                    Scores = 10,
                    Info = "Begib dich in eine Liegestützposition, bei der deine Ellenbogen auf dem Boden aufliegen. Die Hände können sich dabei berühren. Halte diese Position und achte dabei darauf, dass dein Rücken gerade bleibt.",
                    Active = true
                },

                new Exercise
                {
                    ID = 10,
                    Category = ExerciseCategory.Category2,
                    Count = 20,
                    Name = "Beinheben",
                    Unit = "Wiederholungen",
                    Scores = 10,
                    Info = "Lege dich auf den Rücken und strecke deine Beine aus. Die Arme liegen ausgestreckt eng am Körper. Bewege dann deine Beine bis zu einem Winkel von 90° nach oben und lege sie danach wieder ab. Achte bei der Übung darauf, dass die Beine möglichst gestreckt bleiben.",
                    Active = true
                },

                new Exercise
                {
                    ID = 11,
                    Category = ExerciseCategory.Category2,
                    Count = 20,
                    Name = "Russian Twists",
                    Unit = "Wiederholungen je Seite",
                    Scores = 10,
                    Info = "Setze dich auf den Boden und hebe mit angewinkelten Knien deine Füße vom Boden ab. Lege deine Hände mit den Fingerspritzen vor deiner Brust aneinander. Drehe deinen Oberkörper nun abwechselnd zur linken und zur rechten Seite, soweit wie möglich. Dabei berührt nur dein Po den Boden, deine Beine balancieren dich aus. Am Punkt der maximalen Drehung tippst du mit den Fingerspitzen kurz auf den Boden. Du kannst diese Übung auch mit einer leichten Hantel, die du in beiden Händen hältst, durchführen, um den Schwierigkeitsgrad zu erhöhen.",
                    Active = true
                },

                // Category 3

                new Exercise
                {
                    ID = 12,
                    Category = ExerciseCategory.Category3,
                    Count = 10,
                    Name = "Seitstütze",
                    Unit = "Wiederholungen je Seite",
                    Scores = 10,
                    Info = "Um in die Ausgangsposition für diese Übung zu kommen, gehe in den Liegestütz und öffne diesen nun zu einer Seite, sodass du dich nur noch mit einem Arm auf dem Boden aufstützt und zur Seite schaust. Hebe nun den Arm, der nicht den Boden berührt, gerade nach oben und spreize gleichzeitig das obere Bein so weit wie möglich vom Körper ab. Komme danach wieder in die Ausgangsposition zurück. Während der gesamten Übung bleibt der Rücken gerade.",
                    Active = true
                },

                new Exercise
                {
                    ID = 13,
                    Category = ExerciseCategory.Category3,
                    Count = 25,
                    Name = "Windmühle",
                    Unit = "Wiederholungen je Seite",
                    Scores = 10,
                    Info = "Stelle dich mit leicht gespreizten Beinen gerade hin. Strecke beide Arme seitlich vom Körper horizontal aus. Beuge dich vor und berühre mit einem Arm den gegenüberliegenden Fuß, während der andere Arm gerade nach oben gestreckt wird. Richte dich danach wieder auf und komme in die Ausgangsposition zurück. Wiederhole die Übung nun zur anderen Seite.",
                    Active = true
                },

                new Exercise
                {
                    ID = 14,
                    Category = ExerciseCategory.Category3,
                    Count = 30,
                    Name = "Seitliches Oberkörperbeugen",
                    Unit = "Wiederholungen je Seite",
                    Scores = 10,
                    Info = "Im Stehen beugst du einen Oberkörper zur Seite, sodass sich die Hand in Richtung deines Knies bewegt. Führe währenddessen die andere Hand über deinen Kopf. Falls dir diese Übung zu leicht ist, kannst du in beiden Händen jeweils ein leichtes Gewicht halten.",
                    Active = true
                },

                new Exercise
                {
                    ID = 15,
                    Category = ExerciseCategory.Category3,
                    Count = 25,
                    Name = "Hüftrollen",
                    Unit = "Wiederholungen je Seite",
                    Scores = 10,
                    Info = "Die Ausgangsposition ist auf dem Rücken liegend mit angewinkelten Beinen in der Luft. Die Arme sind zu beiden Seiten vom Körper weggestreckt. Drehe nun abwechselnd die Hüften zu beiden Seiten, bis die Beine fast auf dem Boden aufliegen. Die Schulterblätter bleiben dabei beide auf dem Boden und heben nicht ab.",
                    Active = true
                },

                // Category 4

                new Exercise
                {
                    ID = 16,
                    Category = ExerciseCategory.Category4,
                    Count = 4,
                    Name = "Supermann",
                    Unit = "Sets mit je 5 Wdh. pro Seite",
                    Scores = 10,
                    Info = "Begib dich in den Vierfüßlerstand. Achte darauf, dass deine Knie unter den Hüften und deine Handgelenke unter den Schultern stehen. Hebe nun das linke Bein und den rechten Arm vom Boden ab und strecke sie gleichzeitig aus. Halte die Position kurz und wiederhole die Bewegung mit dem anderen Bein und Arm.",
                    Active = true
                },

                new Exercise
                {
                    ID = 17,
                    Category = ExerciseCategory.Category4,
                    Count = 20,
                    Name = "Reverse Situps",
                    Unit = "Wiederholungen",
                    Scores = 10,
                    Info = "Lege dich auf den Bauch und führe die Hände zu den Schläfen. Hebe nun Oberkörper und Zehenspitzen so weit wie möglich vom Boden ab und lass sie danach wieder zu Boden sinken.",
                    Active = true
                },

                new Exercise
                {
                    ID = 18,
                    Category = ExerciseCategory.Category4,
                    Count = 20,
                    Name = "Rudern in Bauchlage",
                    Unit = "Wiederholungen",
                    Scores = 10,
                    Info = "Für diese Übung brauchst du einen Schal oder Gürtel. Lege dich auf den Bauch, strecke deine Arme über deinen Kopf von deinem Körper weg und halte den Schal mit deinen Händen gespannt, sodass die Hände etwas mehr als schulterbreit auseinander sind. Führe nun den Schal hinter deinen Kopf in Richtung deiner Schulterblätter und versuche dabei möglichst deinen Kopf nicht mit dem Schal zu berühren. Strecke danach deine Arme wieder aus.",
                    Active = true
                },

                new Exercise
                {
                    ID = 19,
                    Category = ExerciseCategory.Category4,
                    Count = 20,
                    Name = "Hantel Rudern 5kg",
                    Unit = "Wiederholungen je Seite",
                    Scores = 10,
                    Info = "Stell dich seitlich zu einer ca. kniehohen Kante. Lege ein Knie auf der Kante ab, das andere Bein bleibt gestreckt. Beuge dich nach vorn und stütze dich mit dem zur Kante zeigenden Arm auf dieser ab. Dein Rücken sollte nun in etwa waagerecht zum Boden sein, das abstützende Knie in etwa unter deiner Hüfte und der abstützende Arm in etwa unter deiner Schulter. Nimm mit der freien Hand eine Hantel und ziehe sie zur Hüfte. Der Ellenbogen bleibt dabei möglichst nahe am Körper. Strecke danach den Arm wieder nach unten aus. Halte während der gesamten Übung deinen Rücken gerade und nutze den Bewegungsradius deiner Schulter möglichst voll aus.",
                    Active = true
                },

                // Category 5

                new Exercise
                {
                    ID = 20,
                    Category = ExerciseCategory.Category5,
                    Count = 15,
                    Name = "Ausfallschritte",
                    Unit = "Wiederholungen je Seite",
                    Scores = 10,
                    Info = "Stelle dich gerade hin. Um in die Ausgangsposition der Übung zu kommen, ziehe ein Knie zur Brust und halte es mit beiden Händen fest. Mache nun mit dem angewinkeltem Bein einen großen Ausfallschritt nach vorn. Das Knie des hinteren Beins geht runter bis fast auf den Boden. Stoße dich danach mit dem nach vorn gestreckten Bein wieder nach hinten ab, sodass du wieder aufrecht in der Ausgansposition stehst.",
                    Active = true
                },

                new Exercise
                {
                    ID = 21,
                    Category = ExerciseCategory.Category5,
                    Count = 8,
                    Name = "Kniebeuge einbeinig",
                    Unit = "Wiederholungen je Seite",
                    Scores = 10,
                    Info = "Lege einen Fuß hinter dir auf einer Kante ab. Mit dem anderen Bein machst du Kniebeuge. Halte dabei den Oberkörper möglichst gerade und den Blick nach vorn.",
                    Active = true
                },

                new Exercise
                {
                    ID = 22,
                    Category = ExerciseCategory.Category5,
                    Count = 50,
                    Name = "Wadenheben",
                    Unit = "Wiederholungen",
                    Scores = 10,
                    Info = "Stelle dich mit beiden Fußballen auf eine Kante. Bewege nun die Fersen abwechselnd nach oben und unten und versuche dabei so viel vom Bewegungsradius wie möglich auszunutzen. Gegebenenfalls kannst du dich mit einem Arm an einer Wand oder einem Gegenstand in der Nähe festhalten, um die Balance nicht zu verlieren.",
                    Active = true
                },

                new Exercise
                {
                    ID = 23,
                    Category = ExerciseCategory.Category5,
                    Count = 15,
                    Name = "Hüftheben",
                    Unit = "Wiederholungen je Seite",
                    Scores = 10,
                    Info = "Lege dich auf den Rücken. Winkel ein Bein an, der Fuß bleibt auf dem Boden. Das andere Bein bleibt durchgestreckt. Hebe nun aus der Kraft des angewinkelten Beins dein Becken und das ausgestreckte Bein an. Dein Rücken und das durchgestreckte Bein bilden eine gerade Linie. Führe nun Becken und Bein wieder nach unten, lege beide jedoch zwischen den einzelnen Wiederholungen nicht ab.",
                    Active = true
                },

                // Cool Down

                new Exercise
                {
                    ID = 24,
                    Category = ExerciseCategory.CoolDown,
                    Count = 2,
                    Name = "Vorbeugen",
                    Unit = "Sets aus 20 Sekunden",
                    Scores = 0,
                    Info = "Stelle dich gerade hin, die Füße stehen nahe beieinander. Beuge dich mit geradem Rücken nach vorn und strecke die Fingerspritzen zu den Zehen.",
                    Active = true
                },

                new Exercise
                {
                    ID = 25,
                    Category = ExerciseCategory.CoolDown,
                    Count = 2,
                    Name = "Spagat",
                    Unit = "Sets aus 20 Sekunden",
                    Scores = 0,
                    Info = "Stelle dich gerade hin. Führe nun die Füße so weit wie möglich zu beiden Seiten von deinem Körper weg. Die Fußsohlen bleiben dabei auf dem Boden und die Füße nach vorn gerichtet.",
                    Active = true
                },

                new Exercise
                {
                    ID = 26,
                    Category = ExerciseCategory.CoolDown,
                    Count = 2,
                    Name = "Hände hinterrücks verschränken",
                    Unit = "Sets aus 20 Sekunden",
                    Scores = 0,
                    Info = "Führe eine Hand über deine Schulter hinweg zu deinem oberen Rücken. Die Hand auf der anderen Seite greift unter dem Schulterblatt ebenfalls in Richtung des oberen Rückens. Versuche in dieser Position beide Hände aneinander zu legen und mit den Fingerspitzen zu verschränken.",
                    Active = true
                },

                new Exercise
                {
                    ID = 27,
                    Category = ExerciseCategory.CoolDown,
                    Count = 2,
                    Name = "Schultern dehnen",
                    Unit = "Sets aus 20 Sekunden",
                    Scores = 0,
                    Info = "Führe einen Arm gerade gestreckt zur anderen Körperseite, sodass er sich auf der Höhe deiner Schulter befindet. Ziehe den Arm mit deiner anderen Hand so nahe wie möglich an die Schulter heran.",
                    Active = true
                },

                new Exercise
                {
                    ID = 28,
                    Category = ExerciseCategory.CoolDown,
                    Count = 2,
                    Name = "Brustmuskel dehnen",
                    Unit = "Sets aus 20 Sekunden",
                    Scores = 0,
                    Info = "Stelle dich mit der Schulter seitlich an eine Wand. Strecke den Arm, der zur Wand zeigt, nach hinten aus und lege die Hand auf Schulterhöhe flach auf der Wand ab. Drücke nun deinen Oberkörper an die Wand heran, um eine maximale Dehnung zu erreichen.",
                    Active = true
                },

                new Exercise
                {
                    ID = 29,
                    Category = ExerciseCategory.CoolDown,
                    Count = 2,
                    Name = "Oberschenkel dehnen",
                    Unit = "Sets aus 20 Sekunden",
                    Scores = 0,
                    Info = "Ziehe im Stehen eine Ferse mit beiden Händen zu deinem Po, um deinen Oberschenkel zu dehnen.",
                    Active = true
                },

                new Exercise
                {
                    ID = 30,
                    Category = ExerciseCategory.CoolDown,
                    Count = 2,
                    Name = "Spagat im Sitzen",
                    Unit = "Sets aus 20 Sekunden",
                    Scores = 0,
                    Info = "Setze dich auf den Boden und spreize deine Beine so weit wie möglich. Bewege nun deinen Oberkörper nach vorn, so weit wie du kannst. Halte dabei deinen Rücken möglichst gerade und deine Beine gestreckt. Aus dieser Position heraus kannst du auch versuchen im Wechsel deine Fußspitzen mit den Fingern zu erreichen.",
                    Active = true
                },
            };
        }

        public static ObservableCollection<Settings> Settings
        {
            get
            {
                var settings = new ObservableCollection<Settings>
                {
                    new Settings
                    {
                        Permission = ExerciseSchedulingRepetitionPermission.NotPreferred,
                        RepeatsRound1 = 100,
                        RepeatsRound2 = 75,
                        RepeatsRound3 = 60,
                        RepeatsRound4 = 50,
                        ScoresRound1 = 100,
                        ScoresRound2 = 100,
                        ScoresRound3 = 100,
                        ScoresRound4 = 100,
                        RepeatsAndScoresMultiplier = 100
                    }
                };

                settings[0].Units.Add("Wiederholungen");
                settings[0].Units.Add("Wiederholungen je Seite");
                settings[0].Units.Add("Minuten");
                settings[0].Units.Add("Sekunden");
                return settings;
            }
        }

        public static ObservableCollection<TrainingSchedule> TrainingSchedules
        {
            get => new ObservableCollection<TrainingSchedule>()
            {
                new TrainingSchedule
                {
                    ScheduleType = ScheduleType.Generic,
                    StartMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1),
                    Duration = 1
                }
            };
        }

        public static ObservableCollection<Month> Months
        {
            get => new ObservableCollection<Month>();
        }

        public static DtoCollection Collection
        {
            get
            {
                return new DtoCollection
                {
                    Categories = Categories,
                    Exercises = Exercises,
                    Settings = Settings,
                    TrainingSchedules = TrainingSchedules,
                    Months = Months
                };
            }
        }
    }
}
