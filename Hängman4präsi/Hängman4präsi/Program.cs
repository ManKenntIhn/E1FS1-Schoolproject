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
        static ArrayList Wörtersee()
        {
            //Wörter für das Spiel
            string[] WortData = { "Hauptbahnhof", "Katze", "Jackett", "Hund", "Einfaltspinsel", "Baumhaus", "Puderzucker", "Weißnackenkranich", "Rythmus", "Erdnussbutter", "Code", "Vollmond", "Mondfinsternis", "Terroranschlag", "Marmelade",
                                "Grundstücksverkehrsgenehmigungszuständigkeitsübertragungsverordnung", "Bubatz", "Sneaker", "Tierpark", "Olympia", "Lastkraftwagenfahrer", "Spätzle", "Brechreizbeschleuniger", "Karaoke", "Antrag", "Geld",
                                "Teilchenbeschleuniger", "Prozessor", "Grafikkarte", "Arbeitsspeicher", "Monitor", "Server", "Festplatte", "Donaudampfschiffahrtsgesellschaftskapitän", "Desoxyribonukleinsäure", "aale", "aal", "schmock" };

            // neue Array List erstellen und mit Wörtern aus dem Array WortData initialisieren
            ArrayList wortListe = new ArrayList(WortData);
            return wortListe;
        }

        static List<string> alphabet = new List<string>();


        static void Main()
        {

            Console.Title = ("Galgenmänchen");

            for (int i = 1; i <= 26; i++)
            {
                char alpha = Convert.ToChar(i + 64);
                alphabet.Add(Convert.ToString(alpha));
            }

            alphabet.Add("Ä");
            alphabet.Add("Ö");
            alphabet.Add("Ü");
            alphabet.Add("ß");

            // Geheimwort
            Random random = new Random((int)DateTime.Now.Ticks);

            ArrayList wortListe = Wörtersee();

            string Geheim = wortListe[random.Next(0, wortListe.Count)].ToString();
            string Geheimwort = Geheim.ToUpper();
            List<string> BuchstabeErraten = new List<string>();

            // Leben je nach Länge des zu erratenen Wortes
            int Leben = 0;
            if (Geheimwort.Length < 10)
            {
                Leben = 10;
            }
            else if (Geheimwort.Length > 5 | Geheimwort.Length <= 10)
            {
                Leben = 7;
            }
            else if (Geheimwort.Length >= 20)
            {
                Leben = 5;
            }


            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Galgenmännchen");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("errate ein Wort mit {0} Buchstaben ", Geheimwort.Length);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Du hast {0} Leben", Leben);
            IstBuchstabe(Geheimwort, BuchstabeErraten);
            // solange die leben höher als 0 sind
            while (Leben > 0)
            {
                // Consoleneingabe + Anpassung der groß/klein Schreibung
                Console.ForegroundColor = ConsoleColor.Yellow;
                string EingabeEgal = Console.ReadLine();
                string Eingabe = EingabeEgal.ToUpper();


                // Falls BuchstabeErraten die Eingabe enthält
                if (BuchstabeErraten.Contains(Eingabe))
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("du hast [{0}] bereits versucht", Eingabe);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Bitte versuche ein einen anderen Buchstaben");
                    GetAlphabet(Eingabe);
                    Console.WriteLine("noch {0} Leben", Leben);
                    string Buchstaben = IstBuchstabe(Geheimwort, BuchstabeErraten);
                    Console.Write(Buchstaben);
                    continue;
                }


                // falls das Wort errate, wird
                BuchstabeErraten.Add(Eingabe);
                if (IstWort(Geheimwort, BuchstabeErraten))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(Geheimwort);
                    Console.WriteLine("Glückwunsch, du hast das wort erraten");
                    break;
                }

                // falls ein Buchstabe erraten wird
                else if (Geheimwort.Contains(Eingabe))
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Ja, dieser Buchstabe ist in meinem Wort enthalten");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    string Buchstaben = IstBuchstabe(Geheimwort, BuchstabeErraten);
                    Console.WriteLine("Errate einen weiteren Buchstaben. Unbenutze Buchstaben:"); GetAlphabet(Eingabe);
                    Console.Write(Buchstaben);


                }

                // falls ein falscher Buchstabe eingegeben wird, wird 1 Leben abgezogen.
                else
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Nein, dieser Buchstabe ist nicht in meinem Wort enthalten");
                    Console.WriteLine("Versuche einen anderen Buchstaben. Unbenutze Buchstaben:"); GetAlphabet(Eingabe);
                    Leben -= 1;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("noch {0} Leben", Leben);
                    string Buchstaben = IstBuchstabe(Geheimwort, BuchstabeErraten);
                    Console.Write(Buchstaben);
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

            alphabet.Remove(Buchstaben);
            foreach (string s in alphabet)
            {
                Console.Write("[{0}]", s);
            }
            Console.WriteLine();

        }



        // Check des Wortes
        static bool IstWort(string Geheim_wort, List<string> BuchstabeErraten)
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
                /*Wenn c nicht in den erratenen Buchstaben enthalten ist, haben wir nicht das vollständige Wort*/
                else
                {
                    // ändere den Wert von Wort in false und gebe false zurück
                    return Wort = false;

                }

            }
            return Wort;
        }



        // auf einzelnen Buchstaben prüfen
        static string IstBuchstabe(string Geheimwort, List<string> BuchstabeErraten)
        {
            // Erratenes Wort als leere Zeichenfolge festlegen
            string RichtigeBuchstaben = "";
            // durchschleifen des gesuchten Wortes
            for (int i = 0; i < Geheimwort.Length; i++)
            {
                /* Initialisieren c mit dem wert des Index i
                 * wenn i = 0
                 * c = Geheimwort[0] ist der erste Index des Wortes
                 * c = Geheimwort[1] ist der zweite Index des Wortes und so weiter mit [3], [4], ...*/
                string c = Convert.ToString(Geheimwort[i]);

                // Falls c in der Liste der geratenen Buchstaben ist 
                if (BuchstabeErraten.Contains(c))
                {
                    // füge c zu den richtigen Buchstaben hinzu
                    RichtigeBuchstaben += c;
                }
                else
                {
                    // ansonst gebe "__" aus um zu zeigen, dass der Buchstabe kein teil des Wortes ist
                    RichtigeBuchstaben += " _ ";
                }

            }
            // danach werden alle korrekten Buchstaben ausgegeben.
            return RichtigeBuchstaben;

        }

    }
}