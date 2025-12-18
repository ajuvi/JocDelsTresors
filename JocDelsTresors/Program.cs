namespace JocDelsTresors
{
    internal class Program
    {
        const ConsoleColor COLOR_PLAYER = ConsoleColor.Blue;
        const ConsoleColor COLOR_TRESOR = ConsoleColor.Yellow;
        const ConsoleColor COLOR_FONS = ConsoleColor.Green;
        const int PUNTS_GUANYADOR = 5;
        const int AMPLADA = 50;
        const int ALCADA = 25;

        static void Main(string[] args)
        {
            Console.ResetColor();

            Console.SetWindowSize(AMPLADA, ALCADA);
            Console.SetBufferSize(AMPLADA, ALCADA);
            Console.CursorVisible = false;

            //int xPlayer=5, yPlayer=5;
            int[] cPlayer = [5, 5];
            Random rand = new Random();
            int[] cTresor = [rand.Next(1, AMPLADA - 1), rand.Next(1, ALCADA - 1)];
            int punts = 0;
            bool finalJoc = false;

            PintarFonsPantalla();
            DrawBox(0, 0, AMPLADA, ALCADA, $"AGAFA EL TRESOR | PUNTS: {punts}");
            PintarAvatar(cTresor, COLOR_TRESOR);
            PintarAvatar(cPlayer, COLOR_PLAYER);

            while (!finalJoc)
            {
                // entrada de teclat
                ConsoleKey tecla = Console.ReadKey(true).Key;

                // logica del joc
                MourePlayer(tecla, cPlayer);

                if (Colissio(cTresor, cPlayer))
                {
                    punts++;
                    cTresor = [rand.Next(1, AMPLADA - 1), rand.Next(1, ALCADA - 1)];
                    Console.Beep(440, 300);
                }

                // pintar l'escenari
                PintarFonsPantalla();
                DrawBox(0, 0, AMPLADA, ALCADA, $"AGAFA EL TRESOR | PUNTS: {punts}");
                PintarAvatar(cTresor, COLOR_TRESOR);
                PintarAvatar(cPlayer, COLOR_PLAYER);

                // valorar el final de joc
                finalJoc = punts >= PUNTS_GUANYADOR || tecla == ConsoleKey.Escape;
            }

            PintarFonsPantalla();
            Console.SetCursorPosition(AMPLADA / 3, ALCADA / 2);
            if (punts == PUNTS_GUANYADOR)
                Console.WriteLine("HAS GUANYAT!");
            else
                Console.WriteLine("HAS MARXAT?");

            Console.ReadKey();
        }

        static void MourePlayer(ConsoleKey tecla, int[] cPlayer)
        {
            if (tecla == ConsoleKey.DownArrow)
                cPlayer[1]++;
            else if (tecla == ConsoleKey.UpArrow)
                cPlayer[1]--;
            else if (tecla == ConsoleKey.LeftArrow)
                cPlayer[0]--;
            else if (tecla == ConsoleKey.RightArrow)
                cPlayer[0]++;

            if (cPlayer[0] < 0) cPlayer[0] = 0;
            if (cPlayer[0] >= AMPLADA) cPlayer[0] = AMPLADA - 1;
            if (cPlayer[1] < 0) cPlayer[1] = 0;
            if (cPlayer[1] >= ALCADA) cPlayer[1] = ALCADA - 1;

        }

        static bool Colissio(int[] c1, int[] c2)
        { return c1[0] == c2[0] && c1[1] == c2[1]; }

        static void PintarFonsPantalla()
        {
            ConsoleColor colorAnterior = Console.BackgroundColor;
            Console.BackgroundColor = COLOR_FONS;
            Console.Clear();
            Console.BackgroundColor = colorAnterior;
        }
        static void PintarAvatar(int[] pos, ConsoleColor color)
        {
            ConsoleColor colorAnterior = Console.BackgroundColor;
            Console.BackgroundColor = color;
            Console.SetCursorPosition(pos[0], pos[1]);
            Console.Write(' ');
            Console.BackgroundColor = colorAnterior;
        }

        public static void DrawBox(int x, int y, int width, int height, string title)
        {
            // Desa el color original per restaurar-lo després
            var originalColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.White;

            // Dibuixa la línia superior
            Console.SetCursorPosition(x, y);
            Console.Write("╔" + new string('═', width - 2) + "╗");

            // Dibuixa les línies laterals
            for (int i = 1; i < height - 1; i++)
            {
                Console.SetCursorPosition(x, y + i);
                Console.Write("║");
                Console.SetCursorPosition(x + width - 1, y + i);
                Console.Write("║");
            }

            // Dibuixa la línia inferior
            Console.SetCursorPosition(x, y + height - 1);
            Console.Write("╚" + new string('═', width - 2) + "╝");

            // Col·loca el títol centrat a la vora superior
            Console.SetCursorPosition(x + 2, y);
            Console.Write(title);

            // Restaura el color original
            Console.ForegroundColor = originalColor;
        }

    }
}
