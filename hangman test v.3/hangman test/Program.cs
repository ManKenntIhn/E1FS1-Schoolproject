    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Collections;
    using System.Threading.Tasks;
    

namespace HangMan.cs.hange_man
    {
    class HangManGame
    {
        //Wörter für das spiel
        static string[] WortData = { "Hund", "Katze", "Maus", "Huan", "Schule" };
        // neue ArrayList erstellen und mit Wörtern aus dem Array WortData initialisieren
        static ArrayList wortListe = new ArrayList(WortData);



        static void Main()
        {
            Console.Title = ("HangMan");
            // Geheimwort
            Random random = new Random((int)DateTime.Now.Ticks);


            string Geheim = wortListe[random.Next(0, wortListe.Count)].ToString();
            string Geheimwort = Geheim.ToUpper();
            List<string> BuchstabeErraten = new List<string>();


            int Leben = 5;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Galgenmännchen");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("errate ein Wort mit {0} Buchstaben ", Geheimwort.Length);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Du hast {0} Leben", Leben);
            Isletter(Geheimwort, BuchstabeErraten);
            // solange die leben höher als 0 sind
            while (Leben > 0)
            {
                // Consoleneingabe + Anpassung der Form
                Console.ForegroundColor = ConsoleColor.Yellow;
                string EingabeEgal = Console.ReadLine();
                string Eingabe = EingabeEgal.ToUpper();

                // Falls BuchstabeErraten die Eingabe enthält



                if (BuchstabeErraten.Contains(Eingabe))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("du hast [{0}] bereits versucht", Eingabe);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Verscuhe ein anderes");
                    GetAlphabet(Eingabe);
                    continue;
                }


                // falls das Wort errate wird
                BuchstabeErraten.Add(Eingabe);
                if (IsWort(Geheimwort, BuchstabeErraten))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(Geheimwort);
                    Console.WriteLine("Glückwunsch, du hast das wort erraten");
                    break;
                }
                // falls ein Buchstabe erraten wird
                else if (Geheimwort.Contains(Eingabe))
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("Ja, dieser Buchstabe ist in meinem Wort enthalten");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    string Buchstaben = Isletter(Geheimwort, BuchstabeErraten);
                    Console.Write(Buchstaben);

                }
                // falls ein falscher buchstabe eingegeben wird, wird 1 leben abgezogen.
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Nein, dieser buchstabe ist nicht in meinem Wort enthalten");
                    Leben -= 1;
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine("Du hast noch {0} Leben", Leben);
                }
                Console.WriteLine();
                // Gameover falls alle leben aufgebraucht werden.
                if (Leben == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Game over \ndas Wort war [ {0} ]", Geheimwort);
                    break;
                }

            }

            Console.WriteLine("drücke eine taste um fortzufahren");
            Console.ReadKey();

        }

        // das Alphabet
        static void GetAlphabet(string Buchstaben)
        {
            List<string> alphabet = new List<string>();

            for (int i = 1; i <= 26; i++)
            {
                char alpha = Convert.ToChar(i + 96);
                alphabet.Add(Convert.ToString(alpha));
            }

            // für die regulierung, die übrigen (noch nicht geratenen) buchstaben.
            int num = 49;
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Folgende buchstaben sind noch übrig :");

            for (int i = 0; i < num; i++)
            {
                if (Buchstaben.Contains(Buchstaben))
                {
                    alphabet.Remove(Buchstaben);
                    num -= 1;
                }
                Console.Write("[" + alphabet[i] + "] ");
            }

            Console.WriteLine();

        }



        // check des Wortes
        static bool IsWort(string Geheim_wort, List<string> BuchstabeErraten)
        {

            bool Wort = false;
            // durchschleifen des Gesuchten Wortes
            for (int i = 0; i < Geheim_wort.Length; i++)
            {
                // initialisiere c mit dem index von Geheimwort[i]
                string c = Convert.ToString(Geheim_wort[i]);
                // check ob c in der liste der erratenen Buchstaben steht
                if (BuchstabeErraten.Contains(c))
                {
                    Wort = true;
                }
                /*if Wenn c nicht in den erratenen Buchstaben enthalten ist, haben wir nicht das vollständige Wort*/
                else
                {
                    // ändere den Wert von Wort in false und gebe false zurück
                    return Wort = false;

                }

            }
            return Wort;
        }



        // auf einzelnen Buchstaben prüfen
        static string Isletter(string Geheimwort, List<string> BuchstabeErraten)
        {
            // Erratenes Wort als leere Zeichenfolge festlegen
            string RichtigeBuchstaben = "";
            // durchschleifen des Gesuchten Wortes
            for (int i = 0; i < Geheimwort.Length; i++)
            {
                /* Inizialisiere c mit dem wert des index i
                 * wenn i = 0
                 * c = Geheimwort[0] ist der erste Index des wortes
                 * c = Geheimwort[1] ist der zweite Index des wortes und so´weiter mit [3], [4], ...*/
                string c = Convert.ToString(Geheimwort[i]);

                // Falls c in der Liste der geratenenen buchstaben ist 
                if (BuchstabeErraten.Contains(c))
                {
                    // füge c in "RichtigeBuchstaben" hinzu
                    RichtigeBuchstaben += c;
                }
                else
                {
                    // anonst gebe (__) aus um zu zeigen das der buchstabe kein teil des wortes ist
                    RichtigeBuchstaben += "_ ";
                }

            }
            // danach werden alle korrekten buchstaben ausgegeben.
            return RichtigeBuchstaben;

        }   
        
        }
    }